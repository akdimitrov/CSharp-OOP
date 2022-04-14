namespace AuthorProblem
{
    [Author("Alex")]
    public class StartUp
    {
        [Author("Alex")]
        [Author("John")]
        static void Main(string[] args)
        {
            Tracker tracker = new Tracker();
            tracker.PrintMethodsByAuthor();
        }

        [Author("NoName")]
        static void Test()
        {

        }
    }
}
