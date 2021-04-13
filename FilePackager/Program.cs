using System;

namespace FilePackager
{
    class Program
    {
        

        static void Main(string[] args)
        {
            new Packager()
                .AddFolder(@"C:\Users\Benny\source\repos\BloodKnightParty\BloodKnightParty\Ressources\bin\windows\")
                .Create(@"C:\Users\Benny\source\repos\BloodKnightParty\BloodKnightParty\Packages\test.kco");
            Console.WriteLine("Done!");
            Console.ReadLine();
        }

    }
}
