using System;

namespace CustomRandomList
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            RandomList list = new RandomList() { "aaa", "bbb", "ccc" };
            Console.WriteLine(list.RandomString());
            Console.WriteLine(string.Join(", ", list));
        }
    }
}
