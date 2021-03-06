﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TLSharp.Core.MTProto;
using TLSharp.Core.MTProto.Crypto;

namespace TLSharp.Core.Auth
{
    public class Step1_Response
    {
        public byte[] Nonce { get; set; }
        public byte[] ServerNonce { get; set; }
        public BigInteger Pq { get; set; }
        public List<byte[]> Fingerprints { get; set; }
    }

    public class Step1_PQRequest
    {
        private byte[] nonce;

        public Step1_PQRequest()
        {
            this.nonce = new byte[16];
        }

        public byte[] ToBytes()
        {
            new Random().NextBytes(this.nonce);
            const int constructorNumber = 0x60469778;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
                {
                    binaryWriter.Write(constructorNumber);
                    binaryWriter.Write(this.nonce);

                    return memoryStream.ToArray();
                }
            }
        }

        public Step1_Response FromBytes(byte[] bytes)
        {
            List<byte[]> fingerprints = new List<byte[]>();

            using (MemoryStream memoryStream = new MemoryStream(bytes, false))
            {
                using (BinaryReader binaryReader = new BinaryReader(memoryStream))
                {
                    const int responseConstructorNumber = 0x05162463;
                    int responseCode = binaryReader.ReadInt32();
                    if (responseCode != responseConstructorNumber)
                    {
                        throw new InvalidOperationException($"invalid response code: {responseCode}");
                    }

                    byte[] nonceFromServer = binaryReader.ReadBytes(16);

                    if (!nonceFromServer.SequenceEqual(this.nonce))
                    {
                        throw new InvalidOperationException("invalid nonce from server");
                    }

                    byte[] serverNonce = binaryReader.ReadBytes(16);

                    byte[] pqbytes = Serializers.Bytes.Read(binaryReader);
                    BigInteger pq = new BigInteger(1, pqbytes);

                    int vectorId = binaryReader.ReadInt32();
                    const int vectorConstructorNumber = 0x1cb5c415;
                    if (vectorId != vectorConstructorNumber)
                    {
                        throw new InvalidOperationException($"Invalid vector constructor number {vectorId}");
                    }

                    int fingerprintCount = binaryReader.ReadInt32();
                    for (int i = 0; i < fingerprintCount; i++)
                    {
                        byte[] fingerprint = binaryReader.ReadBytes(8);
                        fingerprints.Add(fingerprint);
                    }

                    return new Step1_Response
                    {
                        Fingerprints = fingerprints,
                        Nonce = nonce,
                        Pq = pq,
                        ServerNonce = serverNonce
                    };
                }
            }
        }
    }
}
