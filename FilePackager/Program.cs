using System;
using System.Diagnostics;
using System.IO;

namespace FilePackager
{
    class Program
    {
        static string MGCB_PATH = @"C:\Program Files (x86)\MSBuild\MonoGame\v3.0\Tools\MGCB.exe";
        static string CONTENT_PATH = @"C:\Users\Benny\source\repos\BloodKnightParty\BloodKnightParty\Ressources\Content.mgcb";

        static void Main(string[] args)
        {

            BuildMGCB();

            new Packager()
                //.AddFolder(@"C:\Users\Benny\source\repos\BloodKnightParty\BloodKnightParty\Ressources\bin\windows\")
                .AddFolder(@"C:\Users\Benny\source\repos\BloodKnightParty\BloodKnightParty\Ressources\bin\DesktopGL\")
                .Create(@"C:\Users\Benny\source\repos\BloodKnightParty\BloodKnightParty\Packages\test.kco");
            Console.WriteLine("Done!");
            Console.ReadLine();
        }

        private static void BuildMGCB()
        {
            var buildProcess = new Process()
            {
                StartInfo = new ProcessStartInfo(MGCB_PATH)
                {
                    WorkingDirectory = Path.GetDirectoryName(CONTENT_PATH),
                    Arguments = $"/clean /@:\"{CONTENT_PATH}\"",
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false
                }
            };

            Console.WriteLine("Started MGCB build!");
            buildProcess.Start();
            Console.WriteLine("Building MGCB ...");
            buildProcess.WaitForExit();

            while(!buildProcess.StandardOutput.EndOfStream)
            {
                Console.WriteLine(buildProcess.StandardOutput.ReadLine());
            }

            if (buildProcess.ExitCode != 0)
            {
                Console.WriteLine("An error occurred while building MGCB");
                Console.WriteLine("Press <Enter> to continue build anyway.");
                Console.ReadLine();
                return;
            }
            Console.WriteLine("Done building MGCB!");
        }

    }
}
