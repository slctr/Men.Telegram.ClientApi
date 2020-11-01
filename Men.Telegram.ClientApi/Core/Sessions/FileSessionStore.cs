using System;
using System.IO;
using Men.Telegram.ClientApi.Core.Sessions;

namespace TLSharp.Core
{
    public class FileSessionStore : ISessionStore
    {
        private readonly DirectoryInfo basePath;

        public FileSessionStore(DirectoryInfo basePath = null)
        {
            if (basePath != null && !basePath.Exists)
            {
                throw new ArgumentException("basePath doesn't exist", nameof(basePath));
            }
            this.basePath = basePath;
        }

        public void Save(TelegramSession session)
        {
            string sessionFileName = $"{session.SessionUserId}.dat";
            string sessionPath = this.basePath == null ? sessionFileName :
                Path.Combine(this.basePath.FullName, sessionFileName);

            using (FileStream stream = new FileStream(sessionPath, FileMode.OpenOrCreate))
            {
                byte[] result = session.ToBytes();
                stream.Write(result, 0, result.Length);
            }
        }

        public TelegramSession Load(string sessionUserId)
        {
            string sessionFileName = $"{sessionUserId}.dat";
            string sessionPath = this.basePath == null ? sessionFileName :
                Path.Combine(this.basePath.FullName, sessionFileName);

            if (!File.Exists(sessionPath))
            {
                return null;
            }

            using (FileStream stream = new FileStream(sessionPath, FileMode.Open))
            {
                byte[] buffer = new byte[2048];
                stream.Read(buffer, 0, 2048);

                return TelegramSession.FromBytes(buffer, this, sessionUserId);
            }
        }
    }
}
