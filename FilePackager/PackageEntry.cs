using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilePackager
{
    public class PackageEntry
    {

        public string Output { get; set; }
        public Func<string, string> NameFilter { get; set; }

    }
}
