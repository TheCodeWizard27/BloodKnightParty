using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodKnightParty.src.Core
{
    class TestContentLoader : ContentManager
    {
        public TestContentLoader(IServiceProvider serviceProvider) : base(serviceProvider, "Content")
        {

        }

        override protected Stream OpenStream(string assetName)
        {
            var test = base.OpenStream(assetName);
            /*using (var sr = new StreamReader(test))
            {
                var test2 = sr.ReadToEnd();
                File.WriteAllText(@"C:\Users\Benny\Desktop\test.fbx", test2, Encoding.UTF8);
            }*/
            
            return test;
        }
    }
}
