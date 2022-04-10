namespace T01.StreamProgress
{
    public class File : BaseStream
    {
        private readonly string name;

        public File(string name, int length, int bytesSent) : base(length, bytesSent)
        {
            this.name = name;
        }
    }
}
