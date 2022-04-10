namespace T01.StreamProgress.Contracts
{
    public interface IStreamable
    {
        int Length { get; }

        int BytesSent { get; }
    }
}
