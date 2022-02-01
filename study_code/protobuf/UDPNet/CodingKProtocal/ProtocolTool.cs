using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace NetProtocol
{
    /// <summary>
    /// 常规序列化工具
    /// </summary>
    public static class ProtocolTool
    {
        public static byte[] Serialize(Pkg msg)
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

        public static Pkg DeSerialize(byte[] bytes)
        {
            Pkg msg = null;

            MemoryStream ms = new MemoryStream(bytes);
            BinaryFormatter bf = new BinaryFormatter();

            try
            {
                msg = (Pkg)bf.Deserialize(ms);
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
    }
}
