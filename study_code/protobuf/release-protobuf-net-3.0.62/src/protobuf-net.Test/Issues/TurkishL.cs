using System.IO;
using System.Threading;
using Xunit;

namespace ProtoBuf.unittest.Attribs
{
    
    public class TurkishL
    {
        [Fact]
        public void FakeTupleTest()
        {
#if !COREFX
            //Turkish culture has unusual case-sensitivity rules:  http://blog.codinghorror.com/whats-wrong-with-turkey/
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("tr-TR");
#endif
            
            byte[] ser;
            using var ms = new MemoryStream();
            var t = new ForTupleSerializer(123, "abc");
            Serializer.Serialize(ms, t);
            ser = ms.ToArray();
        }

        public class ForTupleSerializer
        {
            public ForTupleSerializer(int id, string name)
            {
                Id = id;
                Name = name;
            }

            public int Id { get; private set; }
            public string Name { get; private set; }
        }
    }
}
