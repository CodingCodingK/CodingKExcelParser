using System;
using System.Collections.Generic;

// 网络通信数据协议结构
namespace NetProtocol
{
    [Serializable]
    public class Pkg
    {
        public Head head;
        public Body body;
    }
    [Serializable]
    public class Head
    {
        public CMD cmd;
        public int seq;
        public int error;
    }
    [Serializable]
    public class Body
    {
        public ReqLogicLogin reqLogicLogin;
        public RspLogicLogin rspLogicLogin;
        public ReqBagInfo reqBagInfo;
        public RspBagInfo rspBagInfo;
        public ReqXXXOOO reqXXXOOO;
        public RspXXXOOO rspXXXOOO;
    }
    [Serializable]
    public class ReqXXXOOO
    {
        public string xxoo;
    }
    [Serializable]
    public class RspXXXOOO
    {
        public int ID;
    }

    #region LogicLogin
    [Serializable]
    public class ReqLogicLogin
    {
        public string acct;
        public string pass;
    }
    [Serializable]
    public class RspLogicLogin
    {
        public UserData userData;
    }
    [Serializable]
    public class UserData
    {
        public int uid;//区服唯一ID;
        public string name;
        public int level;
        public int exp;
    }
    #endregion

    #region BagInfo
    [Serializable]
    public class ReqBagInfo
    {
        //TOADD
    }
    [Serializable]
    public class RspBagInfo
    {
        public List<BagItem> itemLst;
    }
    [Serializable]
    public class BagItem
    {
        public int id;//物品id
        public int type;//物品类型
        public string des;//物品描述
    }
    #endregion

    public enum Error
    {
        db_data_error = 1,

        acct_data_error = 2001,//贴数据异常
        team_wait_enter = 2002,//排队等待进入队伍

        bag_data_error = 2003,//背包数据异常
    }

    public enum CMD
    {
        None = 0,
        LogicLogin = 1,
        BagInfo = 2,
    }
}
