using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using TLSharp.Core.MTProto.Crypto;

namespace TLSharp.Core.Network
{
    public delegate TcpClient TcpClientConnectionHandler(string address, int port);

    public class TcpTransport : IDisposable
    {
        private readonly TcpClient tcpClient;
        private readonly NetworkStream stream;
        private int sendCounter = 0;

        public TcpTransport(string address, int port, TcpClientConnectionHandler handler = null)
        {
            if (handler == null)
            {
                IPAddress ipAddress = IPAddress.Parse(address);
                IPEndPoint endpoint = new IPEndPoint(ipAddress, port);

                this.tcpClient = new TcpClient(ipAddress.AddressFamily);

                try {
                    this.tcpClient.Connect (endpoint);
                } catch (Exception ex) {
                    throw new Exception ($"Problem when trying to connect to {endpoint}; either there's no internet connection or the IP address version is not compatible (if the latter, consider using DataCenterIPVersion enum)",
                                         ex);
                }
            }
            else
            {
                this.tcpClient = handler(address, port);
            }

            if (this.tcpClient.Connected)
            {
                this.stream = this.tcpClient.GetStream();
            }
        }

        public async Task Send(byte[] packet, CancellationToken token = default(CancellationToken))
        {
            if (!this.tcpClient.Connected)
            {
                throw new InvalidOperationException("Client not connected to server.");
            }

            TcpMessage tcpMessage = new TcpMessage(this.sendCounter, packet);

            await this.stream.WriteAsync(tcpMessage.Encode(), 0, tcpMessage.Encode().Length, token).ConfigureAwait(false);
            this.sendCounter++;
        }

        public async Task<TcpMessage> Receive(CancellationToken token = default(CancellationToken))
        {
            byte[] packetLengthBytes = new byte[4];
            if (await this.stream.ReadAsync(packetLengthBytes, 0, 4, token).ConfigureAwait(false) != 4)
            {
                throw new InvalidOperationException("Couldn't read the packet length");
            }

            int packetLength = BitConverter.ToInt32(packetLengthBytes, 0);

            byte[] seqBytes = new byte[4];
            if (await this.stream.ReadAsync(seqBytes, 0, 4, token).ConfigureAwait(false) != 4)
            {
                throw new InvalidOperationException("Couldn't read the sequence");
            }

            int seq = BitConverter.ToInt32(seqBytes, 0);

            int readBytes = 0;
            byte[] body = new byte[packetLength - 12];
            int neededToRead = packetLength - 12;

            do
            {
                byte[] bodyByte = new byte[packetLength - 12];
                int availableBytes = await this.stream.ReadAsync(bodyByte, 0, neededToRead, token).ConfigureAwait(false);
                neededToRead -= availableBytes;
                Buffer.BlockCopy(bodyByte, 0, body, readBytes, availableBytes);
                readBytes += availableBytes;
            }
            while (readBytes != packetLength - 12);

            byte[] crcBytes = new byte[4];
            if (await this.stream.ReadAsync(crcBytes, 0, 4, token).ConfigureAwait(false) != 4)
            {
                throw new InvalidOperationException("Couldn't read the crc");
            }

            byte[] rv = new byte[packetLengthBytes.Length + seqBytes.Length + body.Length];

            Buffer.BlockCopy(packetLengthBytes, 0, rv, 0, packetLengthBytes.Length);
            Buffer.BlockCopy(seqBytes, 0, rv, packetLengthBytes.Length, seqBytes.Length);
            Buffer.BlockCopy(body, 0, rv, packetLengthBytes.Length + seqBytes.Length, body.Length);
            Crc32 crc32 = new Crc32();
            System.Collections.Generic.IEnumerable<byte> computedChecksum = crc32.ComputeHash(rv).Reverse();

            if (!crcBytes.SequenceEqual(computedChecksum))
            {
                throw new InvalidOperationException("invalid checksum! skip");
            }

            return new TcpMessage(seq, body);
        }

        public bool IsConnected
        {
            get
            {
                return this.tcpClient.Connected;
            }
        }


        public void Dispose()
        {
            if (this.tcpClient.Connected)
            {
                this.stream.Close();
                this.tcpClient.Close();
            }
        }
    }
}
