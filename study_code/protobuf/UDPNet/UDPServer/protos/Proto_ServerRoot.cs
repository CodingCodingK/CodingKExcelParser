using LogicProtocol;
using ProtoBuf;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace UDPServer
{
    internal class Proto_ServerRoot
    {
        UdpClient udp;

        Proto_LoginHandler loginHandler;
        Proto_BagHandler bagHandler;

        private static Proto_ServerRoot instance;
        public static Proto_ServerRoot Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Proto_ServerRoot();
                }
                return instance;
            }
        }

        public void Init()
        {
            loginHandler = new Proto_LoginHandler();
            bagHandler = new Proto_BagHandler();

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

                MemoryStream st = new MemoryStream(data);
                Pkg pkg = Serializer.Deserialize<Pkg>(st);
                switch (pkg.Head.Cmd)
                {
                    case Cmd.LogicLogin:
                        loginHandler.ReqPBLogin(pkg.Body, remotePoint);
                        break;
                    case Cmd.BagInfo:
                        bagHandler.ReqPBBagInfo(pkg.Body, remotePoint);
                        break;
                    default:
                        break;
                }
            }
        }

        public void SendMsg(Cmd cmd, Body body, IPEndPoint pt)
        {
            Pkg pkg = new Pkg
            {
                Head = new Head
                {
                    Cmd = cmd,
                    Seq = 6,
                    Error = 0
                },
                Body = body
            };

            MemoryStream ms = new MemoryStream();
            Serializer.Serialize(ms, pkg);
            byte[] bytes = new byte[ms.Length];
            Buffer.BlockCopy(ms.GetBuffer(), 0, bytes, 0, (int)ms.Length);

            udp.Send(bytes, bytes.Length, pt);
        }
    }
}
