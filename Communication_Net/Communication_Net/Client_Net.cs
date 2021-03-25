using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace Communication_Net
{
    //客户端简单理解，连接上服务端的网口socket，对服务端的网口socket进行数据收发操作
    public class Client_Net
    {
        private Socket _socket;
        private IPEndPoint _ep;
        private bool _stop = true;
        private Thread receiveThread;
        public string Result { get; set; } = "";
        /// <summary>
        /// Client net shoud init serve net's ip address and its port(defined by owner)
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public void Init(string ip, int port)
        {
            try
            {
                IPAddress address = IPAddress.Parse(ip);
                //将IP地址和端口号绑定到网络节点point上
                _ep = new IPEndPoint(address, port);
                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                //客户端与服务端主要区别：客户端需要连接服务端
                _socket.Connect(_ep);

                receiveThread = new Thread(new ThreadStart(Receive));

            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// Send message to the serve
        /// </summary>
        /// <param name="msg"></param>
        public void Send(string msg)
        {
            try
            {
                Byte[] data = Encoding.ASCII.GetBytes(msg);
                _socket.Send(data);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Receive form serve, you can start a new thread or set a read interval.
        /// </summary>
        public void ReceiveThreadStart()
        {   
            receiveThread.Start();
        }
        public void ReceiveThreadStop()
        {
            _stop = false;
            receiveThread.Abort();
        }
        public void Receive()
        {
            try
            {
                while (_stop)
                {
                    Byte[] data = new byte[1024];
                    _socket.Receive(data);
                    string result = Encoding.ASCII.GetString(data);
                    if (result!="")
                    {
                        Result = result;
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
