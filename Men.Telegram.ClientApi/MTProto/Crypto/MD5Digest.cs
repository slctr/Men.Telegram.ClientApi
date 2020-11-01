using System;
using System.Text;

namespace TLSharp.Core.MTProto.Crypto
{
    public interface IDigest
    {
        /**
* return the algorithm name
*
* @return the algorithm name
*/
        string AlgorithmName { get; }

        /**
* return the size, in bytes, of the digest produced by this message digest.
*
* @return the size, in bytes, of the digest produced by this message digest.
*/
        int GetDigestSize();

        /**
* return the size, in bytes, of the internal buffer used by this digest.
*
* @return the size, in bytes, of the internal buffer used by this digest.
*/
        int GetByteLength();

        /**
* update the message digest with a single byte.
*
* @param inByte the input byte to be entered.
*/
        void Update(byte input);

        /**
* update the message digest with a block of bytes.
*
* @param input the byte array containing the data.
* @param inOff the offset into the byte array where the data starts.
* @param len the length of the data.
*/
        void BlockUpdate(byte[] input, int inOff, int length);

        /**
* Close the digest, producing the final digest value. The doFinal
* call leaves the digest reset.
*
* @param output the array the digest is to be copied into.
* @param outOff the offset into the out array the digest is to start at.
*/
        int DoFinal(byte[] output, int outOff);

        /**
* reset the digest back to it's initial state.
*/
        void Reset();
    }

    public class MD5
    {

        public static string GetMd5String(string data)
        {
            return BitConverter.ToString(GetMd5Bytes(Encoding.UTF8.GetBytes(data))).Replace("-", "").ToLower();
        }

        public static byte[] GetMd5Bytes(byte[] data)
        {
            MD5Digest digest = new MD5Digest();
            digest.BlockUpdate(data, 0, data.Length);
            byte[] hash = new byte[16];
            digest.DoFinal(hash, 0);

            return hash;
        }

        private MD5Digest digest = new MD5Digest();

        public void Update(byte[] chunk)
        {
            this.digest.BlockUpdate(chunk, 0, chunk.Length);
        }

        public void Update(byte[] chunk, int offset, int limit)
        {
            this.digest.BlockUpdate(chunk, offset, limit);
        }

        public string FinalString()
        {
            byte[] hash = new byte[16];
            this.digest.DoFinal(hash, 0);
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }

    public abstract class GeneralDigest
        : IDigest
    {
        private const int BYTE_LENGTH = 64;

        private readonly byte[] xBuf;

        private long byteCount;
        private int xBufOff;

        internal GeneralDigest()
        {
            this.xBuf = new byte[4];
        }

        internal GeneralDigest(GeneralDigest t)
        {
            this.xBuf = new byte[t.xBuf.Length];
            Array.Copy(t.xBuf, 0, this.xBuf, 0, t.xBuf.Length);

            this.xBufOff = t.xBufOff;
            this.byteCount = t.byteCount;
        }

        public void Update(byte input)
        {
            this.xBuf[this.xBufOff++] = input;

            if (this.xBufOff == this.xBuf.Length)
            {
                this.ProcessWord(this.xBuf, 0);
                this.xBufOff = 0;
            }

            this.byteCount++;
        }

        public void BlockUpdate(
            byte[] input,
            int inOff,
            int length)
        {
            //
            // fill the current word
            //
            while ((this.xBufOff != 0) && (length > 0))
            {
                this.Update(input[inOff]);
                inOff++;
                length--;
            }

            //
            // process whole words.
            //
            while (length > this.xBuf.Length)
            {
                this.ProcessWord(input, inOff);

                inOff += this.xBuf.Length;
                length -= this.xBuf.Length;
                this.byteCount += this.xBuf.Length;
            }

            //
            // load in the remainder.
            //
            while (length > 0)
            {
                this.Update(input[inOff]);

                inOff++;
                length--;
            }
        }

        public virtual void Reset()
        {
            this.byteCount = 0;
            this.xBufOff = 0;
            Array.Clear(this.xBuf, 0, this.xBuf.Length);
        }

        public int GetByteLength()
        {
            return BYTE_LENGTH;
        }

        public abstract string AlgorithmName { get; }
        public abstract int GetDigestSize();
        public abstract int DoFinal(byte[] output, int outOff);

        public void Finish()
        {
            long bitLength = (this.byteCount << 3);

            //
            // add the pad bytes.
            //
            this.Update(128);

            while (this.xBufOff != 0)
            {
                this.Update(0);
            }

            this.ProcessLength(bitLength);
            this.ProcessBlock();
        }

        internal abstract void ProcessWord(byte[] input, int inOff);
        internal abstract void ProcessLength(long bitLength);
        internal abstract void ProcessBlock();
    }

    public class MD5Digest
        : GeneralDigest
    {
        private const int DigestLength = 16;

        //
        // round 1 left rotates
        //
        private static readonly int S11 = 7;
        private static readonly int S12 = 12;
        private static readonly int S13 = 17;
        private static readonly int S14 = 22;

        //
        // round 2 left rotates
        //
        private static readonly int S21 = 5;
        private static readonly int S22 = 9;
        private static readonly int S23 = 14;
        private static readonly int S24 = 20;

        //
        // round 3 left rotates
        //
        private static readonly int S31 = 4;
        private static readonly int S32 = 11;
        private static readonly int S33 = 16;
        private static readonly int S34 = 23;

        //
        // round 4 left rotates
        //
        private static readonly int S41 = 6;
        private static readonly int S42 = 10;
        private static readonly int S43 = 15;
        private static readonly int S44 = 21;
        private readonly int[] X = new int[16];
        private int H1, H2, H3, H4; // IV's
        private int xOff;

        public MD5Digest()
        {
            this.Reset();
        }

        /**
* Copy constructor. This will copy the state of the provided
* message digest.
*/

        public MD5Digest(MD5Digest t)
            : base(t)
        {
            this.H1 = t.H1;
            this.H2 = t.H2;
            this.H3 = t.H3;
            this.H4 = t.H4;

            Array.Copy(t.X, 0, this.X, 0, t.X.Length);
            this.xOff = t.xOff;
        }

        public override string AlgorithmName
        {
            get { return "MD5"; }
        }

        public override int GetDigestSize()
        {
            return DigestLength;
        }

        internal override void ProcessWord(
            byte[] input,
            int inOff)
        {
            this.X[this.xOff++] = (input[inOff] & 0xff) | ((input[inOff + 1] & 0xff) << 8)
                        | ((input[inOff + 2] & 0xff) << 16) | ((input[inOff + 3] & 0xff) << 24);

            if (this.xOff == 16)
            {
                this.ProcessBlock();
            }
        }

        internal override void ProcessLength(
            long bitLength)
        {
            if (this.xOff > 14)
            {
                this.ProcessBlock();
            }

            this.X[14] = (int)(bitLength & 0xffffffff);
            this.X[15] = (int)((ulong)bitLength >> 32);
        }

        private void UnpackWord(
            int word,
            byte[] outBytes,
            int outOff)
        {
            outBytes[outOff] = (byte)word;
            outBytes[outOff + 1] = (byte)((uint)word >> 8);
            outBytes[outOff + 2] = (byte)((uint)word >> 16);
            outBytes[outOff + 3] = (byte)((uint)word >> 24);
        }

        public override int DoFinal(
            byte[] output,
            int outOff)
        {
            this.Finish();

            this.UnpackWord(this.H1, output, outOff);
            this.UnpackWord(this.H2, output, outOff + 4);
            this.UnpackWord(this.H3, output, outOff + 8);
            this.UnpackWord(this.H4, output, outOff + 12);

            this.Reset();

            return DigestLength;
        }

        /**
* reset the chaining variables to the IV values.
*/

        public override void Reset()
        {
            base.Reset();

            this.H1 = unchecked(0x67452301);
            this.H2 = unchecked((int)0xefcdab89);
            this.H3 = unchecked((int)0x98badcfe);
            this.H4 = unchecked(0x10325476);

            this.xOff = 0;

            for (int i = 0; i != this.X.Length; i++)
            {
                this.X[i] = 0;
            }
        }

        /*
* rotate int x left n bits.
*/

        private int RotateLeft(
            int x,
            int n)
        {
            return (x << n) | (int)((uint)x >> (32 - n));
        }

        /*
* F, G, H and I are the basic MD5 functions.
*/

        private int F(
            int u,
            int v,
            int w)
        {
            return (u & v) | (~u & w);
        }

        private int G(
            int u,
            int v,
            int w)
        {
            return (u & w) | (v & ~w);
        }

        private int H(
            int u,
            int v,
            int w)
        {
            return u ^ v ^ w;
        }

        private int K(
            int u,
            int v,
            int w)
        {
            return v ^ (u | ~w);
        }

        internal override void ProcessBlock()
        {
            int a = this.H1;
            int b = this.H2;
            int c = this.H3;
            int d = this.H4;

            //
            // Round 1 - F cycle, 16 times.
            //
            a = this.RotateLeft((a + this.F(b, c, d) + this.X[0] + unchecked((int)0xd76aa478)), S11) + b;
            d = this.RotateLeft((d + this.F(a, b, c) + this.X[1] + unchecked((int)0xe8c7b756)), S12) + a;
            c = this.RotateLeft((c + this.F(d, a, b) + this.X[2] + unchecked(0x242070db)), S13) + d;
            b = this.RotateLeft((b + this.F(c, d, a) + this.X[3] + unchecked((int)0xc1bdceee)), S14) + c;
            a = this.RotateLeft((a + this.F(b, c, d) + this.X[4] + unchecked((int)0xf57c0faf)), S11) + b;
            d = this.RotateLeft((d + this.F(a, b, c) + this.X[5] + unchecked(0x4787c62a)), S12) + a;
            c = this.RotateLeft((c + this.F(d, a, b) + this.X[6] + unchecked((int)0xa8304613)), S13) + d;
            b = this.RotateLeft((b + this.F(c, d, a) + this.X[7] + unchecked((int)0xfd469501)), S14) + c;
            a = this.RotateLeft((a + this.F(b, c, d) + this.X[8] + unchecked(0x698098d8)), S11) + b;
            d = this.RotateLeft((d + this.F(a, b, c) + this.X[9] + unchecked((int)0x8b44f7af)), S12) + a;
            c = this.RotateLeft((c + this.F(d, a, b) + this.X[10] + unchecked((int)0xffff5bb1)), S13) + d;
            b = this.RotateLeft((b + this.F(c, d, a) + this.X[11] + unchecked((int)0x895cd7be)), S14) + c;
            a = this.RotateLeft((a + this.F(b, c, d) + this.X[12] + unchecked(0x6b901122)), S11) + b;
            d = this.RotateLeft((d + this.F(a, b, c) + this.X[13] + unchecked((int)0xfd987193)), S12) + a;
            c = this.RotateLeft((c + this.F(d, a, b) + this.X[14] + unchecked((int)0xa679438e)), S13) + d;
            b = this.RotateLeft((b + this.F(c, d, a) + this.X[15] + unchecked(0x49b40821)), S14) + c;

            //
            // Round 2 - G cycle, 16 times.
            //
            a = this.RotateLeft((a + this.G(b, c, d) + this.X[1] + unchecked((int)0xf61e2562)), S21) + b;
            d = this.RotateLeft((d + this.G(a, b, c) + this.X[6] + unchecked((int)0xc040b340)), S22) + a;
            c = this.RotateLeft((c + this.G(d, a, b) + this.X[11] + unchecked(0x265e5a51)), S23) + d;
            b = this.RotateLeft((b + this.G(c, d, a) + this.X[0] + unchecked((int)0xe9b6c7aa)), S24) + c;
            a = this.RotateLeft((a + this.G(b, c, d) + this.X[5] + unchecked((int)0xd62f105d)), S21) + b;
            d = this.RotateLeft((d + this.G(a, b, c) + this.X[10] + unchecked(0x02441453)), S22) + a;
            c = this.RotateLeft((c + this.G(d, a, b) + this.X[15] + unchecked((int)0xd8a1e681)), S23) + d;
            b = this.RotateLeft((b + this.G(c, d, a) + this.X[4] + unchecked((int)0xe7d3fbc8)), S24) + c;
            a = this.RotateLeft((a + this.G(b, c, d) + this.X[9] + unchecked(0x21e1cde6)), S21) + b;
            d = this.RotateLeft((d + this.G(a, b, c) + this.X[14] + unchecked((int)0xc33707d6)), S22) + a;
            c = this.RotateLeft((c + this.G(d, a, b) + this.X[3] + unchecked((int)0xf4d50d87)), S23) + d;
            b = this.RotateLeft((b + this.G(c, d, a) + this.X[8] + unchecked(0x455a14ed)), S24) + c;
            a = this.RotateLeft((a + this.G(b, c, d) + this.X[13] + unchecked((int)0xa9e3e905)), S21) + b;
            d = this.RotateLeft((d + this.G(a, b, c) + this.X[2] + unchecked((int)0xfcefa3f8)), S22) + a;
            c = this.RotateLeft((c + this.G(d, a, b) + this.X[7] + unchecked(0x676f02d9)), S23) + d;
            b = this.RotateLeft((b + this.G(c, d, a) + this.X[12] + unchecked((int)0x8d2a4c8a)), S24) + c;

            //
            // Round 3 - H cycle, 16 times.
            //
            a = this.RotateLeft((a + this.H(b, c, d) + this.X[5] + unchecked((int)0xfffa3942)), S31) + b;
            d = this.RotateLeft((d + this.H(a, b, c) + this.X[8] + unchecked((int)0x8771f681)), S32) + a;
            c = this.RotateLeft((c + this.H(d, a, b) + this.X[11] + unchecked(0x6d9d6122)), S33) + d;
            b = this.RotateLeft((b + this.H(c, d, a) + this.X[14] + unchecked((int)0xfde5380c)), S34) + c;
            a = this.RotateLeft((a + this.H(b, c, d) + this.X[1] + unchecked((int)0xa4beea44)), S31) + b;
            d = this.RotateLeft((d + this.H(a, b, c) + this.X[4] + unchecked(0x4bdecfa9)), S32) + a;
            c = this.RotateLeft((c + this.H(d, a, b) + this.X[7] + unchecked((int)0xf6bb4b60)), S33) + d;
            b = this.RotateLeft((b + this.H(c, d, a) + this.X[10] + unchecked((int)0xbebfbc70)), S34) + c;
            a = this.RotateLeft((a + this.H(b, c, d) + this.X[13] + unchecked(0x289b7ec6)), S31) + b;
            d = this.RotateLeft((d + this.H(a, b, c) + this.X[0] + unchecked((int)0xeaa127fa)), S32) + a;
            c = this.RotateLeft((c + this.H(d, a, b) + this.X[3] + unchecked((int)0xd4ef3085)), S33) + d;
            b = this.RotateLeft((b + this.H(c, d, a) + this.X[6] + unchecked(0x04881d05)), S34) + c;
            a = this.RotateLeft((a + this.H(b, c, d) + this.X[9] + unchecked((int)0xd9d4d039)), S31) + b;
            d = this.RotateLeft((d + this.H(a, b, c) + this.X[12] + unchecked((int)0xe6db99e5)), S32) + a;
            c = this.RotateLeft((c + this.H(d, a, b) + this.X[15] + unchecked(0x1fa27cf8)), S33) + d;
            b = this.RotateLeft((b + this.H(c, d, a) + this.X[2] + unchecked((int)0xc4ac5665)), S34) + c;

            //
            // Round 4 - K cycle, 16 times.
            //
            a = this.RotateLeft((a + this.K(b, c, d) + this.X[0] + unchecked((int)0xf4292244)), S41) + b;
            d = this.RotateLeft((d + this.K(a, b, c) + this.X[7] + unchecked(0x432aff97)), S42) + a;
            c = this.RotateLeft((c + this.K(d, a, b) + this.X[14] + unchecked((int)0xab9423a7)), S43) + d;
            b = this.RotateLeft((b + this.K(c, d, a) + this.X[5] + unchecked((int)0xfc93a039)), S44) + c;
            a = this.RotateLeft((a + this.K(b, c, d) + this.X[12] + unchecked(0x655b59c3)), S41) + b;
            d = this.RotateLeft((d + this.K(a, b, c) + this.X[3] + unchecked((int)0x8f0ccc92)), S42) + a;
            c = this.RotateLeft((c + this.K(d, a, b) + this.X[10] + unchecked((int)0xffeff47d)), S43) + d;
            b = this.RotateLeft((b + this.K(c, d, a) + this.X[1] + unchecked((int)0x85845dd1)), S44) + c;
            a = this.RotateLeft((a + this.K(b, c, d) + this.X[8] + unchecked(0x6fa87e4f)), S41) + b;
            d = this.RotateLeft((d + this.K(a, b, c) + this.X[15] + unchecked((int)0xfe2ce6e0)), S42) + a;
            c = this.RotateLeft((c + this.K(d, a, b) + this.X[6] + unchecked((int)0xa3014314)), S43) + d;
            b = this.RotateLeft((b + this.K(c, d, a) + this.X[13] + unchecked(0x4e0811a1)), S44) + c;
            a = this.RotateLeft((a + this.K(b, c, d) + this.X[4] + unchecked((int)0xf7537e82)), S41) + b;
            d = this.RotateLeft((d + this.K(a, b, c) + this.X[11] + unchecked((int)0xbd3af235)), S42) + a;
            c = this.RotateLeft((c + this.K(d, a, b) + this.X[2] + unchecked(0x2ad7d2bb)), S43) + d;
            b = this.RotateLeft((b + this.K(c, d, a) + this.X[9] + unchecked((int)0xeb86d391)), S44) + c;

            this.H1 += a;
            this.H2 += b;
            this.H3 += c;
            this.H4 += d;

            //
            // reset the offset and clean out the word buffer.
            //
            this.xOff = 0;
            for (int i = 0; i != this.X.Length; i++)
            {
                this.X[i] = 0;
            }
        }
    }
}