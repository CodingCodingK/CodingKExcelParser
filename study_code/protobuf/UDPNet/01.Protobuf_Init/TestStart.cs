using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using CodingK_Extension;
using PEUtils;
using ProtoBuf;

namespace _01.Protobuf_Init
{
    /// <summary>
    /// 测试protobuf和c#普通序列化之间的区别
    /// </summary>
    internal class TestStart
    {
        static void Main(string[] args)
        {
            PELog.InitSettings();

            TestBasicNormal();
            PELog.ColorLog(LogColor.Red, "--------------------------------------------------------------");
            PELog.ColorLog(LogColor.Red, "--------------------------------------------------------------");
            PELog.ColorLog(LogColor.Red, "--------------------------------------------------------------");
            TestBasicProtoBuff();

            Console.ReadKey();
        }

        #region protobuff

        static void TestBasicProtoBuff()
        {
            var person = new PersonInfo
            {
                Id = 10,
                Name = "CodingK",
                Address = new AddressInfo
                {
                    City = "Shanghai",
                    Street = "Valhalla",
                }
            };

            // Serialize
            byte[] bytes = null;
            using (MemoryStream ms = new MemoryStream())
            {
                ProtoBuf.Serializer.Serialize(ms, person);
                bytes = new byte[ms.Length];
                Buffer.BlockCopy(ms.GetBuffer(), 0, bytes, 0, (int)ms.Length);
            }

            // DeSerialize
            PersonInfo newPerson = new PersonInfo();
            PersonInfo newPerson_file = new PersonInfo();
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                newPerson = ProtoBuf.Serializer.Deserialize<PersonInfo>(ms);
            }

            PELog.ColorLog(LogColor.Yellow, "------------------------------Serialize--------------------------------");
            PELog.ColorLog(LogColor.Green, "datas length:" + bytes.Length);
            PELog.ColorLog(LogColor.Green, bytes.BytesToString());
            PELog.ColorLog(LogColor.Yellow, "------------------------------DeSerialize--------------------------------");

            PELog.ColorLog(LogColor.Green, "Id :" + newPerson.Id);
            PELog.ColorLog(LogColor.Green, "Name :" + newPerson.Name);
            PELog.ColorLog(LogColor.Green, "Address :" + newPerson.Address);
            PELog.ColorLog(LogColor.Green, "Address.City :" + newPerson.Address.City);
            PELog.ColorLog(LogColor.Green, "Address.Street :" + newPerson.Address.Street);

            // 本地 存储读取
            using (FileStream file = File.Create("person.bytes"))
            {
                ProtoBuf.Serializer.Serialize(file, person);
            }

            using (FileStream file = File.OpenRead("person.bytes"))
            {
                newPerson_file = ProtoBuf.Serializer.Deserialize<PersonInfo>(file);
            }
        }

        #endregion

        #region normal

        static void TestBasicNormal()
        {
            var person = new PersonInfo
            {
                Id = 10,
                Name = "CodingK",
                Address = new AddressInfo
                {
                    City = "Shanghai",
                    Street = "Valhalla",
                }
            };

            byte[] data = Serialize(person);

            PELog.ColorLog(LogColor.Yellow, "------------------------------Serialize--------------------------------");
            PELog.ColorLog(LogColor.Green,"datas length:" + data.Length);
            PELog.ColorLog(LogColor.Green, data.BytesToString());
            PELog.ColorLog(LogColor.Yellow, "------------------------------DeSerialize--------------------------------");

            PersonInfo newPerson = DeSerialize(data);

            PELog.ColorLog(LogColor.Green, "Id :" + newPerson.Id);
            PELog.ColorLog(LogColor.Green, "Name :" + newPerson.Name);
            PELog.ColorLog(LogColor.Green, "Address :" + newPerson.Address);
            PELog.ColorLog(LogColor.Green, "Address.City :" + newPerson.Address.City);
            PELog.ColorLog(LogColor.Green, "Address.Street :" + newPerson.Address.Street);
        }

        static byte[] Serialize(PersonInfo msg)
        {
            byte[] bytes = null;
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();

            try
            {
                bf.Serialize(ms, msg);
                ms.Seek(0, SeekOrigin.Begin);
                bytes = ms.ToArray();
            }
            catch (Exception ex)
            {
                Console.Write("Serialize failed:" + ex.Message);
            }
            finally
            {
                ms.Close();
            }
            
            return bytes;
        }

        static PersonInfo DeSerialize(byte[] bytes)
        {
            PersonInfo msg = null;
            
            MemoryStream ms = new MemoryStream(bytes);
            BinaryFormatter bf = new BinaryFormatter();

            try
            {
                msg = (PersonInfo)bf.Deserialize(ms);
            }
            catch (Exception ex)
            {
                Console.Write("DeSerialize failed:" + ex.Message);
            }
            finally
            {
                ms.Close();
            }

            return msg;
        }

        #endregion
    }

    [ProtoContract]
    [Serializable]
    class PersonInfo
    {
        [ProtoMember(1)]
        public int Id { get; set; }

        [ProtoMember(2)]
        public string Name { get; set; }

        [ProtoMember(3)]
        public AddressInfo Address { get; set; }
    }

    [ProtoContract]
    [Serializable]
    class AddressInfo
    {
        [ProtoMember(1)]
        public string City { get; set; }

        [ProtoMember(2)]
        public string Street { get; set; }
    }

}
