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
}
