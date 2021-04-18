using System;
using System.Diagnostics;

namespace FilePackager
{
    class Program
    {
        static string MGCB_PATH = @"C:\Program Files (x86)\MSBuild\MonoGame\v3.0\Tools\MGCB.exe";
        static string CONTENT_PATH = @"C:\Users\Benny\source\repos\BloodKnightParty\BloodKnightParty\Ressources\Content.mgcb";

        static void Main(string[] args)
        {

            var buildProcess = new Process()
            {
                StartInfo = new ProcessStartInfo(MGCB_PATH)
                {
                    Arguments = $"/build:\"{CONTENT_PATH}\""
                }
            };

            Console.WriteLine("Started MGCB build!");
            buildProcess.Start();
            Console.WriteLine("Building MGCB ...");
            buildProcess.WaitForExit();
            Console.WriteLine("Done building MGCB!");

            new Packager()
                .AddFolder(@"C:\Users\Benny\source\repos\BloodKnightParty\BloodKnightParty\Ressources\bin\windows\")
                .Create(@"C:\Users\Benny\source\repos\BloodKnightParty\BloodKnightParty\Packages\test.kco");
            Console.WriteLine("Done!");
            Console.ReadLine();
        }

    }
}
