using KantanEngine.Debugging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;

namespace KantanEngine.IO
{
    internal enum FileType 
    {
        NormalFile,
        KCPPackage
    }

    public class KantanLoader
    {

        #region Fields

        private readonly Dictionary<FileType, string> _loadQueue = new Dictionary<FileType, string>();
        private readonly Dictionary<string, byte[]> _bufferedRessources = new Dictionary<string, byte[]>();

        #endregion

        #region Properties

        public Action<string, byte[]> OnLoad;

        #endregion

        #region Public Methods

        public MemoryStream GetLoadedRessource(string path)
        {
            if (!Path.HasExtension(path))
            {
                path += ".xnb";
            }

            return new MemoryStream(_bufferedRessources[path]);
        }

        public KantanLoader AddFileToQueue(string path)
        {
            _loadQueue.Add(FileType.NormalFile, path);
            return this;
        }

        public KantanLoader AddPackageToQueue(string path)
        {
            _loadQueue.Add(FileType.KCPPackage, path);
            return this;
        }

        public async Task<KantanLoader> LoadAsync()
        {
            foreach (var item in _loadQueue)
            {
                if (!File.Exists(item.Value)) continue;

                try
                {
                    Log.Default.Write($"Loading '{item.Value}'");
                    switch (item.Key)
                    {
                        case FileType.KCPPackage:
                            await LoadPackageAsync(item.Value);
                            break;
                        case FileType.NormalFile:
                            await LoadFileAsync(item.Value);
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    var type = Enum.GetName(typeof(FileType), item.Key);
                    Log.Default.Write($"Couldn't load '{item.Value}' ({type}) ex: {ex.Message}");
                }

            }

            return this;
        }

        #endregion

        #region Private Methods

        private async Task LoadFileAsync(string path)
        {
            using (var fs = new FileStream(path, FileMode.Open))
            using (var stream = new MemoryStream())
            {
                await fs.CopyToAsync(stream);
                // TODO Implement this.
                //_bufferedRessources.Add(GetRelativeFileName(path, ), stream.ToArray());
                //OnLoad?.Invoke(Path.GetFileNameWithoutExtension(path), stream);
            }
        }

        private async Task LoadPackageAsync(string path)
        {
            using (var fs = new FileStream(path, FileMode.Open))
            using (var gzip = new GZipStream(fs, CompressionMode.Decompress))
            {
                using (var sr = new StreamReader(gzip, Encoding.UTF8))
                {
                    var result = await sr.ReadToEndAsync();
                    var tmpRessources = JsonConvert.DeserializeObject<Dictionary<string, byte[]>>(result);

                    /*
                    foreach(var ressource in tmpRessources)
                    {
                        File.WriteAllBytes($"{ressource.Key}", ressource.Value);
                    }
                    */

                    CopyDictionaryToTarget(tmpRessources);
                }
            }
        }

        private void CopyDictionaryToTarget(Dictionary<string, byte[]> dict)
        {
            foreach (var item in dict)
            {
                _bufferedRessources.Add(item.Key, item.Value);
                OnLoad?.Invoke(item.Key, item.Value);

                //using (var memStream = new MemoryStream())
                //{
                //    using (var sw = new StreamWriter(memStream, Encoding.UTF8))
                //        await sw.WriteAsync(item.Value);

                //    _bufferedRessources.Add(item.Key, memStream.ToArray());
                //    OnLoad?.Invoke(item.Key, memStream);
                //}
            }
        }

        private string GetRelativeFileName(string directory, string file)
        {
            var dirUri = new Uri(directory);
            var fileUri = new Uri(file);
            return dirUri.MakeRelativeUri(fileUri).OriginalString;
        }

        #endregion

    }
}
