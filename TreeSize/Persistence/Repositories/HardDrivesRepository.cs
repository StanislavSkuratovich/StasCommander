using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using TreeSize.Classes;

namespace TreeSize.Persistence
{
    public class HardDrivesRepository: IDiscReadable, IDiscReWritable
    {

        public string[] GetDiscs()
        {
            var result = Directory.GetLogicalDrives().ToArray();
            return result;
        }

        public List<FIleObject> GetFileObjects(string path)
        {;
            var result = new List<FIleObject>();
            result.AddRange(GetDirectories(path));
            result.AddRange(GetFiles(path));
            return result;
        }

        public List<FIleObject> GetDirectories(string path)
        {
            var info = new DirectoryInfo(path);            
            var result = new List<FIleObject>();
            foreach (var item in info.GetDirectories())
            {
                var folder = new FolderModel { FullPath = item.FullName, Name = item.Name };
                result.Add(folder);
            }
            return result;
        }

        public List<FIleObject> GetFiles(string path)
        {
            var info = new DirectoryInfo(path);
            var result = new List<FIleObject>();
            var foo = info.GetFiles();
            foreach (var item in info.GetFiles())
            {
                var file = new FileModel
                {
                    FullPath = item.FullName,
                    Name = item.Name,
                    Size = item.Length,
                    Type = item.Extension
                };
                result.Add(file);
            }
            return result;
        }

        public void DeleteFileObject(FIleObject fIleObject)
        {
            var type = fIleObject.GetType();
            try
            {
                if(type == typeof(FileModel))
                {
                    var file = new FileInfo(fIleObject.FullPath);
                    file.Delete();/// todo next
                }
                else if((type == typeof(FolderModel)))
                {
                    var folder = new DirectoryInfo(fIleObject.FullPath);
                    folder.Delete(true);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeletePath(string path)
        {
            if (IsPathFileOrDirectory(path))
            {
                DeleteFile(path);
            }
            else
            {
                DeleteFolder(path);
            }
        }

        private bool IsPathFileOrDirectory(string path)
        {
            // get the file attributes for file or directory
            FileAttributes attr = File.GetAttributes(path);
            //detect whether its a directory or file
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                return false;
            }
            return true;
        }

        private void DeleteFolder(string path)
        {
            var directory = new DirectoryInfo(path);
            var DeleteInnerFileObjects = true;
            if (directory.Exists)// make sure is it necessary
            {
                try
                {
                    directory.Delete(DeleteInnerFileObjects);
                }

                catch (System.IO.IOException e)
                {
                    Console.WriteLine(e.Message);//todo
                }
            }
        }

        private void DeleteFile(string path)
        {
            var file = new FileInfo(path);
            try
            {
                file.Delete();
            }
            catch (System.IO.IOException e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public void CopyPath(string targetPath, string sourcePath)
        {

            if (null != sourcePath)
            {
                if (IsPathFileOrDirectory(sourcePath))
                {
                    CopyFile(targetPath, sourcePath);
                }
                else
                {
                    CopyFolder(targetPath, sourcePath);
                }
            }
        }

        public void CopyFolder(string targetPath, string sourcePath)
        {
            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*",
                SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(sourcePath, "*.*",
                SearchOption.AllDirectories))
                File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
        }

        public void CopyFile(string targetPath, string sourcePath)
        {
            try
            {
                var file = new FileInfo(sourcePath);
                string fileName = file.Name;
                string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
                string destFile = System.IO.Path.Combine(targetPath, fileName);
                System.IO.File.Copy(sourcePath, destFile, true);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
