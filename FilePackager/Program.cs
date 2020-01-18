using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilePackager
{
    class Program
    {
        public string Extension { get; set; } = "kbo"; // Kantan Package File

        private Dictionary<string, PackageEntry> _packages = new Dictionary<string, PackageEntry>();

        private Dictionary<string, string> _package = new Dictionary<string, string>();

        static void Main(string[] args)
        {
            new Program()
                .Prepare()
                .Create();
            Console.WriteLine("Done!");
            Console.ReadLine();
        }

        public Program Prepare()
        { 
            AddPackage("package", "test");

            return this;
        }

        public Program Create() 
        {
            Console.WriteLine("Creating...");

            foreach(var entry in _packages) 
            {
                if (!Directory.Exists(entry.Key)) continue;

                var files = Directory.GetFiles(entry.Key);
                FillPackage(files, entry.Value.NameFilter);
                SavePackage(entry.Value.Output);
            }

            Console.WriteLine("Done Creating");
            return this;
        }


        public void AddPackage(string folder, string output)
        {
            AddPackage(folder, output, x => x);
        }
        public void AddPackage(string folder, string output, Func<string, string> filter)
        {
            _packages.Add(folder, new PackageEntry()
            {
                Output = output,
                NameFilter = filter
            });
        }

        private void FillPackage(string[] files, Func<string, string> filter)
        {
            _package.Clear();

            foreach (var file in files)
            {
                var fileName = Path.GetFileNameWithoutExtension(file);
                using (var stream = new StreamReader(File.OpenRead(file)))
                {
                    _package.Add(filter(fileName), stream.ReadToEnd());
                }
            }
        }
        private void SavePackage(string output) 
        {
            var packageName = $"{output}.{Extension}";
            using (var stream = File.OpenWrite(packageName))
            {
                var packageJson = JsonConvert.SerializeObject(_package);
                var compressedWriter = new StreamWriter(new GZipStream(stream, CompressionMode.Compress));
                compressedWriter.Write(packageJson);
            }
        }

    }
}
