using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeSize.Classes;

namespace TreeSize.ViewModels
{
    public class FolderViewModel
    {
        public string Name { get; set; }

        public List<FIleObject> FIleObjects { get; set; }

        public FolderViewModel()
        {

        }
    }
}
