using System;
using NetProtocol;


namespace UDPClient
{
    public class LoginHandler
    {
        public void HandleLoginData(RspLogicLogin rsp)
        {
            Console.WriteLine("Uid:" + rsp.userData.uid);
            Console.WriteLine("Name:" + rsp.userData.name);
            Console.WriteLine("Level:" + rsp.userData.level);
            Console.WriteLine("Exp:" + rsp.userData.exp);
        }
    }

    public class Proto_LoginHandler
    {
        public void HandlePBLoginData(LogicProtocol.RspLogicLogin rsp)
        {
            Console.WriteLine("Uid:" + rsp.userData.Uid);
            Console.WriteLine("Name:" + rsp.userData.Name);
            Console.WriteLine("Level:" + rsp.userData.Level);
            Console.WriteLine("Exp:" + rsp.userData.Exp);
        }
    }
}
