﻿using System;

namespace UDPServer
{
    internal class ServerStart
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Server Starting...");

            ServerRoot.Instance.Init();

            Console.ReadKey();
        }
    }
}
