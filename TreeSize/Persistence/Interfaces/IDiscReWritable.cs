using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeSize.Persistence
{
    public interface IDiscReWritable
    {
        void DeletePath(string path);
        void CopyPath(string targetPath, string sourcePath);

    }
}
