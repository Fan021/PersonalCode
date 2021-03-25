using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocketSever
{
    public class SocketServer
    {
        // 创建一个和客户端通信的套接字
        static Socket _socketListener = null;
        static Thread _threadListen = null;
        //定义一个集合，存储客户端信息
        static Dictionary<string, Socket> _clientConnectionItems = new Dictionary<string, Socket> { };

        private string _barcode;
        private string _message;
        private bool _stop;

        public delegate void MessageEvent(string message);
        public event MessageEvent MessageReceived;

        //For scanner
        private KeyenceScanner.KeyenceModuleLAN _scanner = new KeyenceScanner.KeyenceModuleLAN();

        public SocketServer(string ipAdress, int port)
        {
            try
            {
                //定义一个套接字用于监听客户端发来的消息，包含三个参数（IP4寻址协议，流式连接，Tcp协议）  
                _socketListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                //服务端发送信息需要一个IP地址和端口号  
                IPAddress address = IPAddress.Parse(ipAdress);

                //将IP地址和端口号绑定到网络节点point上  
                IPEndPoint point = new IPEndPoint(address, port);
                //此端口专门用来监听的  

                //监听绑定的网络节点  
                _socketListener.Bind(point);

                //将套接字的监听队列长度限制为10  
                _socketListener.Listen(10);

                //_socketListener.Blocking = false;

                //负责监听客户端的线程:创建一个监听线程  
                _threadListen = new Thread(ListenConnecting);

                //将窗体线程设置为与后台同步，随着主线程结束而结束  
                _threadListen.IsBackground = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool StartListen()
        {
            _stop = false;
            //启动线程     
            _threadListen.Start();

            return true;
        }

        //监听客户端发来的请求  
        private void ListenConnecting()
        {
            Socket connection = null;

            //持续不断监听客户端发来的请求     
            while (!_stop)
            {
                try
                {
                    connection = _socketListener.Accept();
                    //connection = _socketListener.AcceptAsync();
                    if (connection == null) { break; }
                }
                catch (Exception ex)
                {
                    //套接字监听异常    
                    break;
                }

                //获取客户端的IP和端口号  
                IPAddress clientIP = (connection.RemoteEndPoint as IPEndPoint).Address;
                int clientPort = (connection.RemoteEndPoint as IPEndPoint).Port;

                //让客户显示"连接成功的"的信息  
                string sendmsg = "连接服务端成功！\r\n" + "本地IP:" + clientIP + "，本地端口" + clientPort.ToString();
                _message = sendmsg;
                byte[] arrSendMsg = Encoding.UTF8.GetBytes(sendmsg);
                connection.Send(arrSendMsg);

                //客户端网络结点号  
                string remoteEndPoint = connection.RemoteEndPoint.ToString();

                //显示与客户端连接情况
                _message = "成功与" + remoteEndPoint + "客户端建立连接！";

                //添加客户端信息  
                if (!_clientConnectionItems.ContainsKey(remoteEndPoint))
                {
                    _clientConnectionItems.Add(remoteEndPoint, connection);

                    //IPEndPoint netpoint = connection.RemoteEndPoint as IPEndPoint;

                    //创建一个通信线程      
                    ParameterizedThreadStart pts = new ParameterizedThreadStart(ReceiveMsg);
                    Thread thread = new Thread(pts);

                    //设置为后台线程，随着主线程退出而退出 
                    thread.IsBackground = true;

                    //启动线程     
                    thread.Start(connection);

                }
            }
        }

        /// <summary>
        /// 接收客户端发来的信息，客户端套接字对象
        /// </summary>
        /// <param name="socketclientpara"></param>    
        private void ReceiveMsg(object socketclientpara)
        {
            Socket socketClient = socketclientpara as Socket;

            while (!_stop)
            {
                //创建一个内存缓冲区，其大小为1024*1024字节  即1M     
                byte[] serverRecMsg = new byte[1024 * 1024];

                //将接收到的信息存入到内存缓冲区，并返回其字节数组的长度    
                try
                {
                    int length = socketClient.Receive(serverRecMsg);

                    string strRecMsg = Encoding.UTF8.GetString(serverRecMsg, 0, length);

                    if (serverRecMsg[0] == 2 && serverRecMsg[length - 1] == 3)
                    {
                        strRecMsg = Encoding.UTF8.GetString(serverRecMsg, 1, length - 2);
                    }
                    //将机器接受到的字节数组转换为人可以读懂的字符串     


                    _message = "Recived:" + strRecMsg;

                    switch (strRecMsg)
                    {
                        case "INIT":

                            break;

                        case "LON":

                            _barcode = Scan(3000);
                            _message = _barcode;
                            socketClient.Send(Encoding.UTF8.GetBytes(_barcode));

                            break;

                        case "LOFF":

                            TriggerOff();

                            break;

                        case "QUIT":

                            _clientConnectionItems.Remove(socketClient.RemoteEndPoint.ToString());
                            socketClient.Close();

                            break;

                        default:

                            break;
                    }
                }
                catch (Exception ex)
                {
                    _message = ex.Message;
                    _clientConnectionItems.Remove(socketClient.RemoteEndPoint.ToString());
                    socketClient.Close();
                    break;
                }
                finally
                {
                    MessageReceived(_message);
                }
            }
        }

        public void StopListen()
        {
            _stop = true;
            Thread.Sleep(50);

            foreach (KeyValuePair<string, Socket> s in _clientConnectionItems)
            {
                s.Value.Disconnect(false);
                s.Value.Close();
                s.Value.Dispose();
            }

            _clientConnectionItems.Clear();
            _threadListen.Abort();
        }

        public void Quit()
        {
            try
            {
                _stop = true;
                Thread.Sleep(50);

                //Close scanner
                _scanner.Quit();

                //Close clients
                foreach (KeyValuePair<string, Socket> s in _clientConnectionItems)
                {
                    s.Value.Disconnect(false);
                    s.Value.Close();
                    s.Value.Dispose();
                }
                _clientConnectionItems.Clear();

                //Close sever
                _socketListener.Close();
                _socketListener.Dispose();

                _threadListen.Abort();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region "Scanner"

        public bool InitScanner(string ip, int port)
        {
            return _scanner.Init(ip, port);
        }

        private string Scan(int timeout)
        {
            return _scanner.Scan(timeout);
        }

        private bool TriggerOff()
        {
            return _scanner.TrigOFF();
        }

        #endregion

    }
}
