using System;

namespace FilePackager
{
    class Program
    {
        

        static void Main(string[] args)
        {
            new Packager()
                .Prepare()
                .Create();
            Console.WriteLine("Done!");
            Console.ReadLine();
        }

    }
}
