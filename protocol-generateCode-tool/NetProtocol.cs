// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: NetProtocol.proto

#pragma warning disable CS1591, CS0612, CS3021, IDE1006
namespace LogicProtocol
{

    [global::ProtoBuf.ProtoContract()]
    public partial class ReqLogicLogin : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, Name = @"acct", IsRequired = true)]
        public string Acct { get; set; }

        [global::ProtoBuf.ProtoMember(2, Name = @"pass", IsRequired = true)]
        public string Pass { get; set; }

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class RspLogicLogin : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, IsRequired = true)]
        public UserData userData { get; set; }

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class UserData : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, Name = @"uid", IsRequired = true)]
        public int Uid { get; set; }

        [global::ProtoBuf.ProtoMember(2, Name = @"name", IsRequired = true)]
        public string Name { get; set; }

        [global::ProtoBuf.ProtoMember(3, Name = @"level", IsRequired = true)]
        public uint Level { get; set; }

        [global::ProtoBuf.ProtoMember(4, Name = @"exp", IsRequired = true)]
        public int Exp { get; set; }

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class ReqBagInfo : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class RspBagInfo : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, Name = @"itemLst")]
        public global::System.Collections.Generic.List<BagItem> itemLsts { get; } = new global::System.Collections.Generic.List<BagItem>();

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class BagItem : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, Name = @"id", IsRequired = true)]
        public int Id { get; set; }

        [global::ProtoBuf.ProtoMember(2, Name = @"type", IsRequired = true)]
        public int Type { get; set; }

        [global::ProtoBuf.ProtoMember(3, Name = @"des", IsRequired = true)]
        public string Des { get; set; }

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class Pkg : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, Name = @"head", IsRequired = true)]
        public Head Head { get; set; }

        [global::ProtoBuf.ProtoMember(2, Name = @"body")]
        public Body Body { get; set; }

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class Head : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, Name = @"cmd", IsRequired = true)]
        public Cmd Cmd { get; set; } = Cmd.LogicLogin;

        [global::ProtoBuf.ProtoMember(2, Name = @"seq", IsRequired = true)]
        public int Seq { get; set; }

        [global::ProtoBuf.ProtoMember(3, Name = @"error", IsRequired = true)]
        public int Error { get; set; }

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class Body : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public ReqLogicLogin reqLogicLogin { get; set; }

        [global::ProtoBuf.ProtoMember(2)]
        public RspLogicLogin rspLogicLogin { get; set; }

        [global::ProtoBuf.ProtoMember(3)]
        public ReqBagInfo reqBagInfo { get; set; }

        [global::ProtoBuf.ProtoMember(4)]
        public RspBagInfo rspBagInfo { get; set; }

        [global::ProtoBuf.ProtoMember(5, Name = @"xo")]
        public Xxxooo Xo { get; set; }

        [global::ProtoBuf.ProtoMember(6, Name = @"xo1")]
        public Xxxooo1 Xo1 { get; set; }

        [global::ProtoBuf.ProtoMember(7, Name = @"xo2")]
        public Xxxooo2 Xo2 { get; set; }

    }

    [global::ProtoBuf.ProtoContract(Name = @"XXXOOO")]
    public partial class Xxxooo : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, Name = @"name", IsRequired = true)]
        public string Name { get; set; }

    }

    [global::ProtoBuf.ProtoContract(Name = @"XXXOOO1")]
    public partial class Xxxooo1 : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, Name = @"id", IsRequired = true)]
        public int Id { get; set; }

    }

    [global::ProtoBuf.ProtoContract(Name = @"XXXOOO2")]
    public partial class Xxxooo2 : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, Name = @"name", IsRequired = true)]
        public string Name { get; set; }

    }

    [global::ProtoBuf.ProtoContract()]
    public enum Error
    {
        [global::ProtoBuf.ProtoEnum(Name = @"db_data_error")]
        DbDataError = 1,
        [global::ProtoBuf.ProtoEnum(Name = @"acct_data_error")]
        AcctDataError = 2001,
        [global::ProtoBuf.ProtoEnum(Name = @"team_wait_enter")]
        TeamWaitEnter = 2002,
        [global::ProtoBuf.ProtoEnum(Name = @"bag_data_error")]
        BagDataError = 2003,
    }

    [global::ProtoBuf.ProtoContract(Name = @"CMD")]
    public enum Cmd
    {
        LogicLogin = 1,
        BagInfo = 2,
    }

}

#pragma warning restore CS1591, CS0612, CS3021, IDE1006
