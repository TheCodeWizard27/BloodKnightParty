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
    public class Packager
    {

        #region Fields

        private Dictionary<string, PackageEntry> _preparedPackage = new Dictionary<string, PackageEntry>();

        private Dictionary<string, string> _package = new Dictionary<string, string>();

        private readonly string TEXTURE_PREFIX = "texture";

        private readonly string MISC_PREFIX = "misc";

        #endregion


        #region Properties

        public string Extension { get; set; } = "kco"; // Kantan compressed object

        #endregion


        public Packager Prepare()
        {
            var ressourcesFolder = @"C:\Users\Benny\source\repos\BloodKnightParty\BloodKnightParty\Ressources";
            var outputFile = Directory.Exists(ressourcesFolder) ? ressourcesFolder+@"\test" : "test";
            
            AddPackage("package", outputFile);

            return this;
        }


        #region Public Methods

        public Packager Create()
        {
            Console.WriteLine("Creating...");

            foreach (var entry in _preparedPackage)
            {
                if (!Directory.Exists(entry.Key)) continue;

                var files = Directory.GetFiles(entry.Key, "*", SearchOption.AllDirectories);
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
            _preparedPackage.Add(folder, new PackageEntry()
            {
                Output = output,
                NameFilter = filter
            });
        }

        #endregion

        #region Private Methods

        private void FillPackage(string[] files, Func<string, string> filter)
        {
            _package.Clear();

            foreach (var file in files)
            {
                var fileName = Path.GetFileNameWithoutExtension(file);
                var extension = Path.GetExtension(file);

                using (var stream = new StreamReader(File.OpenRead(file)))
                {
                    _package.Add($"{GetPrefix(extension)}.{filter(fileName)}", stream.ReadToEnd());
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

        private string GetPrefix(string extension)
        {
            switch(extension)
            {
                case ".png": case ".jpg":
                    return TEXTURE_PREFIX;
                default:
                    return MISC_PREFIX;
            }
        }

        #endregion

    }
}
