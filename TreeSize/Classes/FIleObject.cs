using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeSize.Classes
{
    public abstract class FIleObject
    {
        public string Name { get; set; }

        public string FullPath { get; set; }
        public abstract  string Type { get; set; }       
        public long Size { get; set; }
    }
}
