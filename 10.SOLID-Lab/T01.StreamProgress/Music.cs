namespace T01.StreamProgress
{
    public class Music : BaseStream
    {
        private readonly string artist;
        private readonly string album;

        public Music(string artist, string album, int length, int bytesSent) : base(length, bytesSent)
        {
            this.artist = artist;
            this.album = album;
        }
    }
}
