using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;
using UDPClient;
using UnityEngine;
using LogicProtocol;

public class Proto_ClientRoot : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("Client Start...");
        Init();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            //ClientRoot.Instance.ReqLogin();
            ReqPBLogin();
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            //ClientRoot.Instance.ReqBag();
            ReqPBBag();
        }
    }


    UdpClient udp;
     Proto_LoginHandler loginHandler;
        Proto_BagHandler bagHandler;
        //服务器IP
        IPEndPoint remotePoint;

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
                Debug.Log("Rcv  Server Data From:" + srvPt.ToString());

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
