using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using LogicProtocol;
using ProtoBuf;

namespace UDPClient
{
    internal class Proto_ClientRoot
    {
        UdpClient udp;

        Proto_LoginHandler loginHandler;
        Proto_BagHandler bagHandler;
        //服务器IP
        IPEndPoint remotePoint;

        private static Proto_ClientRoot instance;
        public static Proto_ClientRoot Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Proto_ClientRoot();
                }
                return instance;
            }
        }

        public void Init()
        {
            remotePoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 17666);
            loginHandler = new Proto_LoginHandler();
            bagHandler = new Proto_BagHandler();

            //设置为0，则客户端发送端口由操作系统分配
            udp = new UdpClient(0);

            Task.Run(ClientRecive);
        }

        async void ClientRecive()
        {
            UdpReceiveResult result;
            while (true)
            {
                result = await udp.ReceiveAsync();
                byte[] data = result.Buffer;

                //服务IP
                IPEndPoint srvPt = result.RemoteEndPoint;
                Console.WriteLine("Rcv  Server Data From:" + srvPt.ToString());

                MemoryStream st = new MemoryStream(data);
                Pkg pkg = Serializer.Deserialize<Pkg>(st);
                switch (pkg.Head.Cmd)
                {
                    case Cmd.LogicLogin:
                        loginHandler.HandlePBLoginData(pkg.Body.rspLogicLogin);
                        break;
                    case Cmd.BagInfo:
                        bagHandler.HandlePBBagData(pkg.Body.rspBagInfo);
                        break;
                    default:
                        break;
                }
            }
        }

        public void ReqPBLogin()
        {
            Pkg pkg = new Pkg
            {
                Head = new Head
                {
                    Cmd = Cmd.LogicLogin,
                    Seq = 1
                },
                Body = new Body
                {
                    reqLogicLogin = new ReqLogicLogin
                    {
                        Acct = "CodingCodingK",
                        Pass = "123456",
                    }
                }
            };

            MemoryStream ms = new MemoryStream();
            Serializer.Serialize(ms, pkg);
            //36
            byte[] bytes = new byte[ms.Length];
            Buffer.BlockCopy(ms.GetBuffer(), 0, bytes, 0, (int)ms.Length);

            udp.Send(bytes, bytes.Length, remotePoint);
        }

        public void ReqPBBag()
        {
            Pkg pkg = new Pkg
            {
                Head = new Head
                {
                    Cmd = Cmd.BagInfo,
                    Seq = 2
                }
            };
            MemoryStream ms = new MemoryStream();
            Serializer.Serialize(ms, pkg);
            byte[] bytes = new byte[ms.Length];
            Buffer.BlockCopy(ms.GetBuffer(), 0, bytes, 0, (int)ms.Length);

            udp.Send(bytes, bytes.Length, remotePoint);
        }
    }
}
