using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
//using System.ServiceModel.Channels;

namespace SocketSever
{
    internal class AsyncUserToken
    {
        private Socket _socket = null;
        private IPEndPoint _endPort = null;
        private DateTime _connectTime = default(DateTime);       // 连接时间

        public Socket Socket
        {
            get { return _socket; }
            set { _socket = value; }
        }

        public DateTime ConnectTime
        {
            get { return _connectTime; }
            set { _connectTime = value; }
        }

        public IPEndPoint EndPort
        {
            get { return _endPort; }
            set { _endPort = value; }
        }
    }
}