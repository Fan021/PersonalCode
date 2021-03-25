using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;
using System.Reflection;

namespace SocketSever
{
    public class SocketManager
    {
        public event EventHandler<SocketAsyncEventArgs> OnReceiveCompleted;     // 接收到数据
        public event EventHandler<SocketAsyncEventArgs> OnSendCompleted;        // 数据发送完成
        public event EventHandler<SocketAsyncEventArgs> OnAccept;               // 客户端连接通知
        public event EventHandler<SocketAsyncEventArgs> OnConnectionBreak;      // 客户端下线通知

        private readonly object _lockHelper = new object();
        private bool _isRunning = false;                                       // TCP服务器是否正在运行
        private int _numConnections = 1;                                       // 同时处理的最大连接数
        private int _bufferSize = 0;                                           // 用于每个Socket I/O 操作的缓冲区大小
        private BufferManager _bufferManager = null;                           // 表示用于所有套接字操作的大量可重用的缓冲区
        private Socket _listenSocket = null;                                   // 用于监听传入的连接请求的套接字
        private SocketAsyncEventArgsPool _readWritePool = null;                // 可重用SocketAsyncEventArgs对象池，用于写入，读取和接受套接字操作
        private int _totalBytesRead = 0;                                       // 服务器接收的总共＃个字节的计数器
        private int _numConnectedSockets = 0;                                  // 当前连接的tcp客户端数量
        private Semaphore _maxNumberAcceptedClients = null;                    // 控制tcp客户端连接数量的信号量
        private List<SocketAsyncEventArgs> _connectedPool = null;              // 用于socket发送数据的SocketAsyncEventArgs集合
        private string _ip = "";
        private int _port = 0;

        /// <summary>
        /// 创建服务端实例
        /// </summary>
        /// <param name="numConnections">允许连接到tcp服务器的tcp客户端数量</param>
        /// <param name="bufferSize">用于socket发送和接收的缓存区大小</param>
        public SocketManager(int numConnections, int bufferSize)
        {
            _numConnections = numConnections;
            _bufferSize = bufferSize;
            _bufferManager = new BufferManager(bufferSize * numConnections, bufferSize);
            _readWritePool = new SocketAsyncEventArgsPool(numConnections);
            _connectedPool = new List<SocketAsyncEventArgs>(numConnections);
        }

        /// <summary>
        /// 启动服务器，侦听客户端连接请求
        /// </summary>
        /// <param name="localEndPoint"></param>
        public void Start(string ip, int port)
        {
            if (_isRunning) return;
            if (string.IsNullOrEmpty(ip)) throw new ArgumentNullException("ip cannot be null");
            if (port < 1 || port > 65535) throw new ArgumentOutOfRangeException("port is out of range");

            _ip = ip;
            _port = port;

            try
            {
                Init();

                _maxNumberAcceptedClients = new Semaphore(_numConnections, _numConnections);

                // 创建 Socket 监听连接请求
                _listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress address = IPAddress.Parse(ip);
                IPEndPoint endpoint = new IPEndPoint(address, port);
                _listenSocket.Bind(endpoint);
                _listenSocket.Listen(int.MaxValue);   // Listen 参数表示最多可容纳的等待接受的连接数

                // 接受客户端连接请求
                StartAccept(null);

                _isRunning = true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                throw ex;
            }
        }

        /// <summary>
        /// 在客户端连接前，先分配 SocketAsyncEventArg 对象池
        /// </summary>
        private void Init()
        {
            _totalBytesRead = 0;
            _numConnectedSockets = 0;

            // 分配一个大字节缓冲区，所有 I/O 操作都使用该缓冲区。
            _bufferManager.InitBuffer();
            SocketAsyncEventArgs socketAsyncEventArgs;
            for (int i = 0; i < _numConnections; i++)
            {
                // 分配可重用的 SocketAsyncEventArgs 对象
                socketAsyncEventArgs = new SocketAsyncEventArgs();
                socketAsyncEventArgs.Completed += IO_Completed;
                //socketAsyncEventArgs.UserToken = new AsyncUserToken();

                // 将缓冲池中的字节缓冲区分配给 SocketAsyncEventArg 对象
                _bufferManager.SetBuffer(socketAsyncEventArgs);

                // 放入对象池
                _readWritePool.Push(socketAsyncEventArgs);
            }
        }

        public void Stop()
        {
            if (_isRunning)
            {
                _isRunning = false;

                if (_listenSocket == null)
                    return;

                try
                {
                    foreach (var e in _connectedPool)
                    {
                        if (OnConnectionBreak != null)
                            OnConnectionBreak(this, e);
                        e.AcceptSocket.Shutdown(SocketShutdown.Both);
                        e.AcceptSocket.Close();
                        e.AcceptSocket = null;
                        e.Dispose();
                    }

                    _listenSocket.LingerState = new LingerOption(true, 0);
                    _listenSocket.Close();
                    _listenSocket = null;
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.Message);
                    throw ex;
                }
                finally
                {
                    _connectedPool.Clear();
                    _readWritePool.Clear();
                    _bufferManager.FreeAllBuffer();
                    GC.Collect();
                }
            }
        }

        public void Restart()
        {
            Stop();
            Start(_ip, _port);
        }

        /// <summary>
        /// 开始接受来自客户端的连接请求
        /// </summary>
        /// <param name="acceptEventArg"></param>
        private void StartAccept(SocketAsyncEventArgs acceptEventArg)
        {
            if (acceptEventArg == null)
            {
                acceptEventArg = new SocketAsyncEventArgs();
                acceptEventArg.Completed += AcceptEventArg_Completed;
            }
            else
            {
                acceptEventArg.AcceptSocket = null;
            }
            _maxNumberAcceptedClients.WaitOne();
            if (_listenSocket == null)
            {
                return;
            }
            if (!_listenSocket.AcceptAsync(acceptEventArg))
            {
                ProcessAccept(acceptEventArg);
            }
        }

        /// <summary>
        /// Socket.AcceptAsync完成回调函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AcceptEventArg_Completed(object sender, SocketAsyncEventArgs e)
        {
            ProcessAccept(e);
        }

        /// <summary>
        /// 接受到tcp客户端连接，进行处理
        /// </summary>
        /// <param name="e"></param>
        private void ProcessAccept(SocketAsyncEventArgs e)
        {
            if (_isRunning)
            {
                if (e.SocketError == SocketError.Success)
                {
                    Interlocked.Increment(ref _numConnectedSockets);

                    if (OnAccept != null)
                        OnAccept(this, e);

                    // 获取接受的客户端连接的套接字
                    SocketAsyncEventArgs socketAsyncEventArgs = _readWritePool.Pop();
                    socketAsyncEventArgs.AcceptSocket = e.AcceptSocket;

                    lock (_lockHelper)
                    {
                        _connectedPool.Add(socketAsyncEventArgs);
                    }

                    // tcp服务器开始接受tcp客户端发送的数据
                    if (!e.AcceptSocket.ReceiveAsync(socketAsyncEventArgs))
                    {
                        ProcessReceive(socketAsyncEventArgs);
                    }

                    // 接受下一个连接请求
                    StartAccept(e);
                }
            }
        }

        /// <summary>
        /// socket.sendAsync和socket.recvAsync的完成回调函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IO_Completed(object sender, SocketAsyncEventArgs e)
        {
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.Receive:
                    ProcessReceive(e);
                    break;
                case SocketAsyncOperation.Send:
                    ProcessSend(e);
                    break;
                default:
                    throw new ArgumentException("The last operation completed on the socket was not a receive or send");
            }
        }

        /// <summary>
        /// 处理接受到的tcp客户端数据
        /// </summary>
        /// <param name="e"></param>
        private void ProcessReceive(SocketAsyncEventArgs e)
        {
            if (_isRunning)
            {
                //AsyncUserToken asyncUserToken = (AsyncUserToken)e.UserToken;
                if (e.BytesTransferred > 0 && e.SocketError == SocketError.Success)
                {
                    Interlocked.Add(ref _totalBytesRead, e.BytesTransferred);
                    if (OnReceiveCompleted != null)
                        OnReceiveCompleted(this, e);


                    //Get received message
                    string strRecMsg = Encoding.UTF8.GetString(e.Buffer, e.Offset, e.BytesTransferred);


                    e.SetBuffer(e.Offset, _bufferSize);
                    if (!e.AcceptSocket.ReceiveAsync(e))
                    {
                        ProcessReceive(e);
                        return;
                    }
                }
                else
                {
                    CloseClientSocket(e);
                }
            }
        }

        /// <summary>
        /// 处理tcp服务器发送的结果
        /// </summary>
        /// <param name="e"></param>
        private void ProcessSend(SocketAsyncEventArgs e)
        {
            if (_isRunning)
            {
                if (e.SocketError == SocketError.Success)
                {
                    if (OnSendCompleted != null)
                        OnSendCompleted(this, e);
                }
                else
                {
                    CloseClientSocket(e);
                }
            }
        }

        /// <summary>
        /// 断开某一客户端
        /// </summary>
        /// <param name="e"></param>
        private void CloseClientSocket(SocketAsyncEventArgs e)
        {
            if (_isRunning)
            {
                if (OnConnectionBreak != null)
                    OnConnectionBreak(this, e);

                if (e != null && e.AcceptSocket != null)
                {
                    try
                    {
                        e.AcceptSocket.Shutdown(SocketShutdown.Both);
                        e.AcceptSocket.Disconnect(false);
                        e.AcceptSocket.Close();
                        e.AcceptSocket = null;
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine(ex.Message);
                    }
                    finally
                    {
                        Interlocked.Decrement(ref _numConnectedSockets);
                        _readWritePool.Push(e);
                        _maxNumberAcceptedClients.Release();

                        lock (_lockHelper)
                            _connectedPool.Remove(e);
                    }
                }
            }
        }

        /// <summary>
        /// 向指定客户端发送信息
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="message"></param>
        public void Send(EndPoint endpoint, string message)
        {
            byte[] buff = Encoding.UTF8.GetBytes(message);
            if (buff.Length > _bufferSize)
                throw new ArgumentOutOfRangeException("message is out off range");

            SocketAsyncEventArgs argSend = _connectedPool.Find((s) =>
            {
                return s.AcceptSocket.RemoteEndPoint.ToString() == endpoint.ToString();
            });

            if (argSend != null)
            {
                SocketAsyncEventArgs sendArg = new SocketAsyncEventArgs();
                sendArg.AcceptSocket = argSend.AcceptSocket;
                sendArg.SetBuffer(buff, 0, buff.Length);

                //获取绑定函数的名称,若为空重新绑定
                var eventField = sendArg.GetType().GetField("m_Completed", BindingFlags.Instance | BindingFlags.NonPublic);
                if (eventField != null)
                {
                    var handle = eventField.GetValue(sendArg) as Delegate;
                    if (handle == null || handle.Method.Name == "") sendArg.Completed += IO_Completed; //重新绑定
                }

                bool willRaiseEvent = argSend.AcceptSocket.SendAsync(sendArg);  //The same socket Instance can not be reused in one proccess.
                if (!willRaiseEvent)
                {
                    ProcessSend(argSend);
                }
            }
        }

        /// <summary>
        /// 向已连接所有客户端发送
        /// </summary>
        /// <param name="message"></param>
        public void SendToAllClient(string message)
        {
            if (string.IsNullOrEmpty(message))
                throw new ArgumentNullException("message cannot be null");

            foreach (var e in _connectedPool)
            {
                Send(e.AcceptSocket.RemoteEndPoint, message);
            }
        }

        #region "PrivateMethods"

        #endregion
    }
}
