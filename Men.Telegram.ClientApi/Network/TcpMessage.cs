using System;
using System.IO;
using System.Linq;
using TLSharp.Core.MTProto.Crypto;

namespace TLSharp.Core.Network
{
    public class TcpMessage
    {
        public int SequneceNumber { get; private set; }
        public byte[] Body { get; private set; }

        public TcpMessage(int seqNumber, byte[] body)
        {
            if (body == null)
            {
                throw new ArgumentNullException(nameof(body));
            }

            this.SequneceNumber = seqNumber;
            this.Body = body;
        }

        public byte[] Encode()
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
                {
                    // https://core.telegram.org/mtproto#tcp-transport
                    /*
                        4 length bytes are added at the front 
                        (to include the length, the sequence number, and CRC32; always divisible by 4)
                        and 4 bytes with the packet sequence number within this TCP connection 
                        (the first packet sent is numbered 0, the next one 1, etc.),
                        and 4 CRC32 bytes at the end (length, sequence number, and payload together).
                    */
                    binaryWriter.Write(this.Body.Length + 12);
                    binaryWriter.Write(this.SequneceNumber);
                    binaryWriter.Write(this.Body);
                    Crc32 crc32 = new Crc32();
                    byte[] checksum = crc32.ComputeHash(memoryStream.GetBuffer(), 0, 8 + this.Body.Length).Reverse().ToArray();
                    binaryWriter.Write(checksum);

                    byte[] transportPacket = memoryStream.ToArray();

                    //					Debug.WriteLine("Tcp packet #{0}\n{1}", SequneceNumber, BitConverter.ToString(transportPacket));

                    return transportPacket;
                }
            }
        }

        public static TcpMessage Decode(byte[] body)
        {
            if (body == null)
            {
                throw new ArgumentNullException(nameof(body));
            }

            if (body.Length < 12)
            {
                throw new InvalidOperationException("Ops, wrong size of input packet");
            }

            using (MemoryStream memoryStream = new MemoryStream(body))
            {
                using (BinaryReader binaryReader = new BinaryReader(memoryStream))
                {
                    int packetLength = binaryReader.ReadInt32();

                    if (packetLength < 12)
                    {
                        throw new InvalidOperationException(string.Format("invalid packet length: {0}", packetLength));
                    }

                    int seq = binaryReader.ReadInt32();
                    byte[] packet = binaryReader.ReadBytes(packetLength - 12);
                    byte[] checksum = binaryReader.ReadBytes(4);

                    Crc32 crc32 = new Crc32();
                    System.Collections.Generic.IEnumerable<byte> computedChecksum = crc32.ComputeHash(body, 0, packetLength - 4).Reverse();

                    if (!checksum.SequenceEqual(computedChecksum))
                    {
                        throw new InvalidOperationException("invalid checksum! skip");
                    }

                    return new TcpMessage(seq, packet);
                }
            }
        }
    }
}
