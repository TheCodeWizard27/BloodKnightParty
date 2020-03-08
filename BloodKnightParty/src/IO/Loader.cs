using BloodKnightParty.src.Debugging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodKnightParty.Core.IO
{
    internal enum FileType 
    {
        NormalFile,
        KCPPackage
    }

    public class Loader
    {

        #region Fields

        private readonly Dictionary<FileType, string> _loadQueue = new Dictionary<FileType, string>();

        #endregion

        #region Properties

        public Action<string, Stream> OnLoad;

        #endregion

        #region Public Methods

        public Loader AddFileToQueue(string path)
        {
            _loadQueue.Add(FileType.NormalFile, path);
            return this;
        }

        public Loader AddPackageToQueue(string path)
        {
            _loadQueue.Add(FileType.KCPPackage, path);
            return this;
        }

        //TODO make this async
        public Loader Load()
        {
            foreach(var item in _loadQueue)
            {
                if (!File.Exists(item.Value)) continue;

                try
                {
                    Log.Default.WriteLine($"Loading '{item.Value}'");
                    switch (item.Key)
                    {
                        case FileType.KCPPackage:
                            LoadKCPPackage(item.Value);
                            break;
                        default:
                            break;
                    }
                }catch(Exception ex)
                {
                    Log.Default.WriteLine($"Couldn't load '{item.Value}' ex: {ex.Message}");
                }
                
            }

            return this;
        }

        #endregion

        #region Private Methods

        private void LoadFile(string path)
        {
            using (var fs = new FileStream(path, FileMode.Open))
            using (var stream = new MemoryStream())
            {
                fs.CopyTo(stream);
                OnLoad?.Invoke(Path.GetFileNameWithoutExtension(path), stream);
            }
        }

        private void LoadKCPPackage(string path)
        {
            using (var fs = new FileStream(path, FileMode.Open))
            using (var gzip = new GZipStream(fs, CompressionMode.Decompress))
            {
                using(var sr = new StreamReader(gzip))
                {
                    var tmpRessources = JsonConvert.DeserializeObject<Dictionary<string, string>>(sr.ReadToEnd());
                    //CopyDictionarToTarget(tmpRessources);
                }
            }
        }

        private void CopyDictionarToTarget(Dictionary<string, Stream> dict)
        {
            foreach(var item in dict)
                OnLoad?.Invoke(item.Key, item.Value);
        }

        #endregion

    }
}
