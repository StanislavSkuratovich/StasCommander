using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeSize.Classes
{
   public class FolderModel : FIleObject
    {
        public override string Type { get; set; } = "Folder";
        public FolderModel()
        {

        }
    }
}
