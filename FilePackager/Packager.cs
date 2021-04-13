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

        private Dictionary<string, byte[]> _package = new Dictionary<string, byte[]>();

        #endregion


        #region Properties

        public string Extension { get; set; } = "kco"; // Kantan compressed object

        #endregion

        #region Public Methods

        public Packager Create(string output)
        {
            Console.WriteLine("Creating...");

            var tasks = new List<Task>();

            foreach (var entry in _preparedPackage)
            {
                if (!Directory.Exists(entry.Key)) continue;

                tasks.Add(FillPackage(entry.Key, entry.Value.NameFilter));
            }

            Task.WaitAll(tasks.ToArray());
            SavePackage(output);

            Console.WriteLine("Done Creating");
            return this;
        }


        public Packager AddFolder(string folder) => AddFolder(folder, x => x);
        public Packager AddFolder(string folder, Func<string, string> filter)
        {
            _preparedPackage.Add(folder, new PackageEntry()
            {
                NameFilter = filter
            });
            return this;
        }

        public Packager Clear()
        {
            _preparedPackage.Clear();
            _package.Clear();
            return this;
        }

        #endregion


        #region Private Methods

        private string GetRelativeFileName(string directory, string file)
        {
            var dirUri = new Uri(directory);
            var fileUri = new Uri(file);
            return dirUri.MakeRelativeUri(fileUri).OriginalString;
        }

        private async Task FillPackage(string directoryPath, Func<string, string> filter)
        {
            var files = Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                var fileName = GetRelativeFileName(directoryPath, file);
                var extension = Path.GetExtension(file);

                if (extension == $".{Extension}") continue;

                var keyName = filter(fileName);
                Console.WriteLine($"file: {file} has been added as {keyName}");
                await Task.Run(() =>
                {
                    _package.Add(keyName, File.ReadAllBytes(file));
                });
                /*
                using (var fs = File.OpenRead(file))
                using (var rs = new StreamReader(fs))
                {
                    _package.Add(keyName, await rs.ReadToEndAsync());
                }
                */

                //using (var filestream = new StreamReader(new FileStream(file, FileMode.Open)))
                //{
                //    //await filestream.readtoendasync()


                //    _package.Add(keyName, await filestream.ReadToEndAsync());
                //}

                //using (var fileStream = new StreamReader(new FileStream(file, FileMode.Open)))
                //using (var memoryStream = new MemoryStream())
                //using (var gzipStream = new GZipStream(memoryStream, CompressionMode.Compress))
                //using (var streamWriter = new StreamWriter(gzipStream))
                //{
                //    //await fileStream.ReadToEndAsync()
                //    streamWriter.Write(File.ReadAllText(file));
                //    var keyName = filter(fileName);
                //    Console.WriteLine($"File: {file} has been added as {keyName}");
                //    var arr = memoryStream.ToArray();
                //    _package.Add(keyName, Convert.ToBase64String(arr));
                //}
            }
        }
        private void SavePackage(string output)
        {
            var packageName = $"{output}";
            File.Delete(packageName);
            using (var fileStream = File.OpenWrite(packageName))
            //using (var stream = new StreamWriter(fileStream, Encoding.UTF8))
            using (var stream = new StreamWriter(new GZipStream(fileStream, CompressionMode.Compress), Encoding.UTF8))
            {
                var packageJson = JsonConvert.SerializeObject(_package);
                stream.Write(packageJson);
            }
        }

        #endregion

    }
}
