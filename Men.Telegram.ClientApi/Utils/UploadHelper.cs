using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Men.Telegram.ClientApi;
using TeleSharp.TL;
using TeleSharp.TL.Upload;

namespace TLSharp.Core.Utils
{
    public static class UploadHelper
    {
        private static string GetFileHash(byte[] data)
        {
            string md5CheckSum;
            using (MD5 md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(data);
                StringBuilder hashResult = new StringBuilder(hash.Length * 2);

                foreach (byte t in hash)
                {
                    hashResult.Append(t.ToString("x2"));
                }

                md5CheckSum = hashResult.ToString();
            }

            return md5CheckSum;
        }

        public static async Task<TLAbsInputFile> UploadFile(this TelegramClient client, string name, StreamReader reader, CancellationToken token = default(CancellationToken))
        {
            const long tenMb = 10 * 1024 * 1024;
            return await UploadFile(name, reader, client, reader.BaseStream.Length >= tenMb, token).ConfigureAwait(false);
        }

        private static byte[] GetFile(StreamReader reader)
        {
            byte[] file = new byte[reader.BaseStream.Length];

            using (reader)
            {
                reader.BaseStream.Read(file, 0, (int)reader.BaseStream.Length);
            }

            return file;
        }

        private static Queue<byte[]> GetFileParts(byte[] file)
        {
            Queue<byte[]> fileParts = new Queue<byte[]>();

            const int maxFilePart = 512 * 1024;

            using (MemoryStream stream = new MemoryStream(file))
            {
                while (stream.Position != stream.Length)
                {
                    if ((stream.Length - stream.Position) > maxFilePart)
                    {
                        byte[] temp = new byte[maxFilePart];
                        stream.Read(temp, 0, maxFilePart);
                        fileParts.Enqueue(temp);
                    }
                    else
                    {
                        long length = stream.Length - stream.Position;
                        byte[] temp = new byte[length];
                        stream.Read(temp, 0, (int)(length));
                        fileParts.Enqueue(temp);
                    }
                }
            }

            return fileParts;
        }

        private static async Task<TLAbsInputFile> UploadFile(string name, StreamReader reader,
            TelegramClient client, bool isBigFileUpload, CancellationToken token = default(CancellationToken))
        {
            token.ThrowIfCancellationRequested();

            byte[] file = GetFile(reader);
            Queue<byte[]> fileParts = GetFileParts(file);

            int partNumber = 0;
            int partsCount = fileParts.Count;
            long file_id = BitConverter.ToInt64(Helpers.GenerateRandomBytes(8), 0);
            while (fileParts.Count != 0)
            {
                byte[] part = fileParts.Dequeue();

                if (isBigFileUpload)
                {
                    await client.SendAuthenticatedRequestAsync<bool>(new TLRequestSaveBigFilePart
                    {
                        FileId = file_id,
                        FilePart = partNumber,
                        Bytes = part,
                        FileTotalParts = partsCount
                    }, token).ConfigureAwait(false);
                }
                else
                {
                    await client.SendAuthenticatedRequestAsync<bool>(new TLRequestSaveFilePart
                    {
                        FileId = file_id,
                        FilePart = partNumber,
                        Bytes = part
                    }, token).ConfigureAwait(false);
                }
                partNumber++;
            }

            if (isBigFileUpload)
            {
                return new TLInputFileBig
                {
                    Id = file_id,
                    Name = name,
                    Parts = partsCount
                };
            }
            else
            {
                return new TLInputFile
                {
                    Id = file_id,
                    Name = name,
                    Parts = partsCount,
                    Md5Checksum = GetFileHash(file)
                };
            }
        }
    }
}
