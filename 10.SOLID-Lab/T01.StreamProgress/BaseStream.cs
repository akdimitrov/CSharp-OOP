using T01.StreamProgress.Contracts;

namespace T01.StreamProgress
{
    public abstract class BaseStream : IStreamable
    {
        protected BaseStream(int length, int bytesSent)
        {
            Length = length;
            BytesSent = bytesSent;
        }

        public int Length { get; private set; }

        public int BytesSent { get; private set; }
    }
}