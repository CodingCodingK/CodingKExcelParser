using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using NetProtocol;

namespace UDPServer
{
    public class BagHandler
    {
        public void ReqBagInfo(Body reqBody, IPEndPoint pt)
        {
            Console.WriteLine("Client:{0} ReqBagInfo.", pt.ToString());

            Body body = new Body
            {
                rspBagInfo = new RspBagInfo
                {
                    itemLst = new List<BagItem>()
                }
            };
            body.rspBagInfo.itemLst.Add(new BagItem { id = 1, type = 0, des = "精灵球" });
            body.rspBagInfo.itemLst.Add(new BagItem { id = 2, type = 0, des = "强力精灵球" });
            body.rspBagInfo.itemLst.Add(new BagItem { id = 3, type = 1, des = "奇异糖果" });

            ServerRoot.Instance.SendMsg(CMD.BagInfo, body, pt);
        }
    }
}
