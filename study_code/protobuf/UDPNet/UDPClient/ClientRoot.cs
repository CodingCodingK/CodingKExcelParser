using NetProtocol;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace UDPClient
{
    public class ClientRoot
    {
        UdpClient udp;

        LoginHandler loginHandler;
        BagHandler bagHandler;
        //服务器IP
        IPEndPoint remotePoint;

        private static ClientRoot instance;
        public static ClientRoot Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ClientRoot();
                }
                return instance;
            }
        }

        public void Init()
        {
            remotePoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 17666);
            loginHandler = new LoginHandler();
            bagHandler = new BagHandler();

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

                Pkg pkg = ProtocolTool.DeSerialize(data);
                switch (pkg.head.cmd)
                {
                    case CMD.LogicLogin:
                        loginHandler.HandleLoginData(pkg.body.rspLogicLogin);
                        break;
                    case CMD.BagInfo:
                        bagHandler.HandleBagData(pkg.body.rspBagInfo);
                        break;
                    case CMD.None:
                    default:
                        break;
                }
            }
        }

        public void ReqLogin()
        {
            Pkg pkg = new Pkg
            {
                head = new Head
                {
                    cmd = CMD.LogicLogin,
                    seq = 1
                },
                body = new Body
                {
                    reqLogicLogin = new ReqLogicLogin
                    {
                        acct = "CodingCodingK",
                        pass = "123456"
                    }
                }
            };
            //592 ->668
            byte[] bytes = ProtocolTool.Serialize(pkg);
            udp.Send(bytes, bytes.Length, remotePoint);
        }

        public void ReqBag()
        {
            Pkg pkg = new Pkg
            {
                head = new Head
                {
                    cmd = CMD.BagInfo,
                    seq = 2
                }
            };

            byte[] bytes = ProtocolTool.Serialize(pkg);
            udp.Send(bytes, bytes.Length, remotePoint);
        }
    }
}
