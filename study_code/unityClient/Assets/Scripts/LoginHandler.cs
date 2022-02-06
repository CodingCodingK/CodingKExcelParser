using System;
using UnityEngine;

namespace UDPClient
{
    public class Proto_LoginHandler
    {
        public void HandlePBLoginData(LogicProtocol.RspLogicLogin rsp)
        {
            Debug.Log("Uid:" + rsp.userData.Uid);
            Debug.Log("Name:" + rsp.userData.Name);
            Debug.Log("Level:" + rsp.userData.Level);
            Debug.Log("Exp:" + rsp.userData.Exp);
        }
    }
}
