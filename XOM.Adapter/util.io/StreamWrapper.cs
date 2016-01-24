using System.IO;
using java.io;

namespace nu.util.io
{
    public class StreamWrapper : Stream
    {
        private InputStream m_InputStream;

        public override bool CanRead
        {
            get { return this.m_InputStream != null; }
        }

        public StreamWrapper(InputStream inputStream)
        {
            this.m_InputStream = inputStream;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            var readBytes = this.m_InputStream.read(buffer, offset, count);
            if (readBytes == -1) return 0;
            return readBytes;
        }

        public override void Flush()
        {
            //do nothing
        }

        public override void Close()
        {
            this.m_InputStream.close();
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override long Length
        {
            get { throw new System.NotImplementedException(); }
        }

        public override bool CanSeek
        {
            get { return false; }
        }

        public override long Position {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); } 
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new System.NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new System.NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new System.NotImplementedException();
        }
    }
}
