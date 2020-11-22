using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeSize.Classes;

namespace TreeSize.Persistence
{
    public interface IDiscReadable
    {

         List<FIleObject> GetFileObjects(string path);
        List<FIleObject> GetDirectories(string path);
        List<FIleObject> GetFiles(string path);
        string[] GetDiscs();
    }
}
