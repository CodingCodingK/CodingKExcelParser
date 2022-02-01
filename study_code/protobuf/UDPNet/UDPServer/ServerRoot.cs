using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using NetProtocol;

namespace UDPServer
{
    internal class ServerRoot
    {
        LoginHandler loginHandler;
        BagHandler bagHandler;

        private static ServerRoot instance;
        UdpClient udp;

        public static ServerRoot Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ServerRoot();
                }
                return instance;
            }
        }

        public void Init()
        {
            loginHandler = new LoginHandler();
            bagHandler = new BagHandler();

            udp = new UdpClient(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 17666));

            Task.Run(ServerRecive);
        }

        async void ServerRecive()
        {
            UdpReceiveResult result;
            while (true)
            {
                result = await udp.ReceiveAsync();
                byte[] data = result.Buffer;

                //客户端IP
                IPEndPoint remotePoint = result.RemoteEndPoint;
                Console.WriteLine("Rcv Client Data From:" + remotePoint.ToString());

                Pkg pkg = ProtocolTool.DeSerialize(data);
                switch (pkg.head.cmd)
                {
                    case CMD.LogicLogin:
                        loginHandler.ReqLogin(pkg.body, remotePoint);
                        break;
                    case CMD.BagInfo:
                        bagHandler.ReqBagInfo(pkg.body, remotePoint);
                        break;
                    case CMD.None:
                    default:
                        break;
                }
            }
        }

        public void SendMsg(CMD cmd, Body body, IPEndPoint pt)
        {
            Pkg pkg = new Pkg
            {
                head = new Head
                {
                    cmd = cmd,
                    seq = 6,// 这里随便取一个
                    error = 0
                },
                body = body
            };

            byte[] bytes = ProtocolTool.Serialize(pkg);
            udp.Send(bytes, bytes.Length, pt);
        }
    }
}
