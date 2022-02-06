using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetProtocol;
using PEUtils;

namespace UDPClient
{
    internal class BagHandler
    {
        public void HandleBagData(RspBagInfo rsp)
        {
            List<BagItem> itemLst = rsp.itemLst;
            for (int i = 0; i < itemLst.Count; i++)
            {
                Console.WriteLine("id:" + itemLst[i].id);
                Console.WriteLine("type:" + itemLst[i].type);
                Console.WriteLine("des:" + itemLst[i].des);
                Console.WriteLine("---------------------");
            }
        }
    }

    public class Proto_BagHandler
    {
        public void HandlePBBagData(LogicProtocol.RspBagInfo rsp)
        {
            List<LogicProtocol.BagItem> itemLst = rsp.itemLsts;
            for (int i = 0; i < itemLst.Count; i++)
            {
                Console.WriteLine("id:" + itemLst[i].Id);
                Console.WriteLine("type:" + itemLst[i].Type);
                Console.WriteLine("des:" + itemLst[i].Des);
                Console.WriteLine("-----------------");
            }
        }
    }
}
