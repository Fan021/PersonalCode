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
    //服务端简单理解，Socket就是一个网口，服务端先要初始化自己的网口socket，侦听是否有客户端的网口socket连入，连入一个就创建一个socket，收发信息都要指定（地址）客户端的网口socket
    public class Serve_Net
    {
        private Socket _socket;
        private IPEndPoint _ep;
        private bool _stop = true;
        private Thread _listenThread;
        public ReceiveMessage _receiveMessage = new ReceiveMessage() { message = "", client = "",time="" };
        //use the dictionary to save clients
        public Dictionary<string, Socket> _clientItems = new Dictionary<string, Socket>();
        public void Init(string ip, int port)
        {
            try
            {
                IPAddress address = IPAddress.Parse(ip);
                _ep = new IPEndPoint(address, port);
                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                //客户端与服务端主要区别：服务端需要开启侦听
                _socket.Bind(_ep);//监听绑定的网络节点  
                _socket.Listen(10); //将套接字的监听队列长度限制为10  

                _listenThread = new Thread(new ThreadStart(ListenClient));
                //将窗体线程设置为与后台同步，随着主线程结束而结束  
                _listenThread.IsBackground = true;
                _listenThread.Start();
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// serve net should start listen thread to konw how many clients connect
        /// </summary>
        public void ListenClient()
        {
            Socket connection = null;
            //keep listeing 
            while (true)
            {
                //wait for the new connection
                try
                {
                    //thread will stop to wait until a new client connect
                    connection = _socket.Accept();
                    if (connection == null)
                    {
                        break;
                    }
                }
                catch (Exception)
                {
                    throw;
                }

                //infrom the client of connecting successfully
                IPAddress address = (connection.RemoteEndPoint as IPEndPoint).Address;
                int port = (connection.RemoteEndPoint as IPEndPoint).Port;
                string messageClient = "Success to connect Serve.\r\n" + "Local IP:" + address.ToString() + "\r\n" + "Local port:" + port.ToString();
                byte[] data = Encoding.ASCII.GetBytes(messageClient);
                connection.Send(data);

                //save the new connecetion in dictionary and start a new communication thread for it.
                if (!_clientItems.ContainsKey(address.ToString()))
                {
                    _clientItems.Add(address.ToString(), connection);

                    Thread clientReceive = new Thread(new ParameterizedThreadStart(Receive));
                    clientReceive.IsBackground = true;
                    clientReceive.Start(connection);
                }

            }
        }

        /// <summary>
        /// Receive thread to receive message from target client
        /// </summary>
        public void Receive(object para)
        {
            Socket client = para as Socket;
            Byte[] data = new byte[1024];
            while (true)
            {
                //thread will stop to wait until get a new message
                client.Receive(data);
                string result = Encoding.ASCII.GetString(data);
                result=result.Trim('\0');
                if (result != "")
                {
                    _receiveMessage.message = result;
                    _receiveMessage.client = (client.RemoteEndPoint as IPEndPoint).Address.ToString();
                    _receiveMessage.time = DateTime.Now.ToString();
                    result = "";
                }
            }
        }

        /// <summary>
        /// Store receive message
        /// </summary>
        public struct ReceiveMessage
        {
            public string message;
            public string client;
            public string time;
        }

        /// <summary>
        /// Send message to the target client
        /// </summary>
        /// <param name="clientName"></param>
        /// <param name="msg"></param>
        public void Send(string clientName,string msg)
        {
            try
            {
                Socket connection = null;
                //Search the target client socket
                foreach (var item in _clientItems)
                {
                    if (item.Key == clientName)
                    {
                        connection = item.Value;
                    }
                }
                
                if (connection == null)
                {
                    throw new Exception("No client connect!");
                }
                Byte[] data = Encoding.ASCII.GetBytes(msg);
                connection.Send(data);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }

}
