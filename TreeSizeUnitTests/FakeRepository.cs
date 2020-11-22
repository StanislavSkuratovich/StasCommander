using System;
using System.Collections.Generic;
using System.Text;
using TreeSize.Classes;
using TreeSize.Persistence;

namespace TreeSizeUnitTests
{
    public class FakeRepository : IDiscReadable, IDiscReWritable
    {
        public List<FIleObject> ExternalCollection = new List<FIleObject>();
        public List<FIleObject> InnerCollection = new List<FIleObject>();
        private void FillExternalCollection()
        {
            ExternalCollection.Clear();
            InnerCollection.Clear();
            var externalFolder = new FolderModel { FullPath = FolderSizeCounterTests.ExternalFolderName, Name = "externalFolder" };
            var testFile1 = new FileModel { FullPath = externalFolder.FullPath + @"\testFile1", Name = "testFile1", Size = 1000 };
            ExternalCollection.Add(testFile1);
            var testFile2 = new FileModel { FullPath = externalFolder.FullPath + @"\testFile2", Name = "testFile2", Size = 1000 };
            ExternalCollection.Add(testFile2);
            var innerFolder = new FolderModel { FullPath = externalFolder.FullPath + @"\"+ FolderSizeCounterTests.InnerFolderName, Name = "innerFolder" };
            ExternalCollection.Add(innerFolder);
            var testFile3 = new FileModel { FullPath = innerFolder.FullPath + @"\testFile3", Name = "testFile3", Size = 1000 };
            InnerCollection.Add(testFile3);
            var testFile4 = new FileModel { FullPath = innerFolder.FullPath + @"\testFile4", Name = "testFile4", Size = 1000 };
            InnerCollection.Add(testFile4);
        }
        public List<FIleObject> GetDirectories(string path)
        {
            FillExternalCollection();
           if(path == FolderSizeCounterTests.ExternalFolderName)
            {
                return ExternalCollection.FindAll(i=>i.GetType() == typeof(FolderModel));
            }
           else if(path == FolderSizeCounterTests.InnerFolderName)
            {
                return null;
            }
           else throw new NotImplementedException();
        }

        public List<FIleObject> GetFiles(string path)
        {
            FillExternalCollection();
            if (path == FolderSizeCounterTests.ExternalFolderName)
            {
                return ExternalCollection.FindAll(i => i.GetType() == typeof(FileModel));
            }
            else if (path == FolderSizeCounterTests.InnerFolderName)
            {
                return InnerCollection.FindAll(i => i.GetType() == typeof(FileModel));
            }
            else throw new NotImplementedException();
        }

        public string[] GetDiscs()
        {
            throw new NotImplementedException();
        }

        public List<FIleObject> GetFileObjects(string path)
        {
            throw new NotImplementedException();
        }

        void IDiscReWritable.DeletePath(string path)
        {
            throw new NotImplementedException();
        }

        void IDiscReWritable.CopyPath(string targetPath, string sourcePath)
        {
            throw new NotImplementedException();
        }
    }
}
