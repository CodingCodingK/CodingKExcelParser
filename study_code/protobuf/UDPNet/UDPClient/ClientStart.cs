using System;
using PEUtils;

namespace UDPClient
{
    internal class ClientStart
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Client Start...");
            PELog.InitSettings();
            ClientRoot.Instance.Init();
            //PBGameRoot.Instance.Init();

            while (true)
            {
                string ipt = Console.ReadLine();
                if (ipt == "login")
                {
                    ClientRoot.Instance.ReqLogin();
                    //PBGameRoot.Instance.ReqPBLogin();
                }
                else if (ipt == "bag")
                {
                    ClientRoot.Instance.ReqBag();
                    //PBGameRoot.Instance.ReqPBBag();
                }
                else
                {
                    Console.WriteLine("input command [login] or [bag] plz.");
                }
            }
        }
    }
}
