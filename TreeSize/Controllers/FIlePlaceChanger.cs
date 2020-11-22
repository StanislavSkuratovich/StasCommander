using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeSize.Persistence;

namespace TreeSize.Controllers
{
    public class FIlePlaceChanger
    {
        public IDiscReadable _reader { get; set; }
        public IDiscReWritable _reWriter { get; set; }
        private string savedPath;
        public delegate void PathPlacementHandler(string path);
        public  Dictionary<string, PathPlacementHandler> Methods => new Dictionary<string, PathPlacementHandler>()
        {
            ["Copy"] = new PathPlacementHandler(CopyPath),
            ["Paste"] = new PathPlacementHandler(PastePath),
            ["Delete"] = new PathPlacementHandler(DeletePath)
        };

        public FIlePlaceChanger(IDiscReadable reader, IDiscReWritable reWriter)
        {
            _reader = reader;
            _reWriter = reWriter;
        }
        public PathPlacementHandler ChooseMethod(string methodName)//todo
        {
            var action = SelectAppropriateDelegate(methodName);
            if (null == action)
            {
                throw new NullReferenceException();
            }
            var bar = action.Method.Name;// it's how can i test it
            return action;
        }

        private PathPlacementHandler SelectAppropriateDelegate(string methodName)
        {
            var chosenmethodName = Methods.Where(m => m.Key == methodName).Single();
            return chosenmethodName.Value;
        }

        private void CopyPath(string path)
        {
            this.savedPath = path;
        }

        private void PastePath(string path)
        {

            if (null != this.savedPath)
            {
                _reWriter.CopyPath(targetPath: path, sourcePath: this.savedPath);
            }
        }

        private void DeletePath(string path)
        {
            _reWriter.DeletePath(path);
        }

    }
}
