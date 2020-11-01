using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TeleSharp.TL;
namespace TeleSharp.TL
{
    public class TLObjectAttribute : Attribute
    {
        public int Constructor { get; private set; }
        
        public TLObjectAttribute(int Constructor)
        {
            this.Constructor = Constructor;
        }
    }

    public abstract class TLObject
    {
        public abstract int Constructor { get; }
        public abstract void SerializeBody(BinaryWriter bw);
        public abstract void DeserializeBody(BinaryReader br);
        public byte[] Serialize()
        {
            using (MemoryStream m = new MemoryStream())
            using (BinaryWriter bw = new BinaryWriter(m))
            {
                this.Serialize(bw);
                bw.Close();
                m.Close();
                return m.ToArray();
            }
        }
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(this.Constructor);
            this.SerializeBody(writer);
        }
        public void Deserialize(BinaryReader reader)
        {
            int constructorId = reader.ReadInt32();
            if (constructorId != this.Constructor)
            {
                throw new InvalidDataException("Constructor Invalid");
            }

            this.DeserializeBody(reader);
        }
    }
}
