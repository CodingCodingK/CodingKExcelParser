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
                        name = "Plane",
                        level = 17,
                        exp = 27
                    }
                }
            };

            ServerRoot.Instance.SendMsg(CMD.LogicLogin, body, pt);
        }
    }
}
