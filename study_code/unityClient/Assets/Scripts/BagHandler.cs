using System;
using System.Collections.Generic;
using UnityEngine;

namespace UDPClient
{
    public class Proto_BagHandler
    {
        public void HandlePBBagData(LogicProtocol.RspBagInfo rsp)
        {
            List<LogicProtocol.BagItem> itemLst = rsp.itemLsts;
            for (int i = 0; i < itemLst.Count; i++)
            {
                Debug.Log("id:" + itemLst[i].Id);
                Debug.Log("type:" + itemLst[i].Type);
                Debug.Log("des:" + itemLst[i].Des);
                Debug.Log("-----------------");
            }
        }
    }
}
