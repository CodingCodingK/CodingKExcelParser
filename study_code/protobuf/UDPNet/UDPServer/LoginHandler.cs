using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using NetProtocol;

namespace UDPServer
{
    public class LoginHandler
    {
        public void ReqLogin(Body reqBody, IPEndPoint pt)
        {
            ReqLogicLogin req = reqBody.reqLogicLogin;
            Console.WriteLine("Client:{0} ReqLogin,Acct:{1} Pass:{2}", pt.ToString(), req.acct, req.pass);

            Body body = new Body
            {
                rspLogicLogin = new RspLogicLogin
                {
                    userData = new UserData
                    {
                        uid = 7,
                        name = "CodingK",
                        level = 17,
                        exp = 27
                    }
                }
            };

            ServerRoot.Instance.SendMsg(CMD.LogicLogin, body, pt);
        }
    }

    public class Proto_LoginHandler
    {
        public void ReqPBLogin(LogicProtocol.Body reqBody, IPEndPoint pt)
        {
            LogicProtocol.ReqLogicLogin req = reqBody.reqLogicLogin;
            Console.WriteLine("Client:{0} ReqLogin,Acct:{1} Pass:{2}", pt.ToString(), req.Acct, req.Pass);

            LogicProtocol.Body body = new LogicProtocol.Body
            {
                rspLogicLogin = new LogicProtocol.RspLogicLogin
                {
                    userData = new LogicProtocol.UserData
                    {
                        Uid = 7,
                        Name = "CodingK",
                        Level = 17,
                        Exp = 27
                    }
                }
            };

            Proto_ServerRoot.Instance.SendMsg(LogicProtocol.Cmd.LogicLogin, body, pt);
        }
    }
}
