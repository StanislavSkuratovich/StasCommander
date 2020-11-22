using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TreeSize.Classes;
using TreeSize.Persistence;

namespace TreeSize.Controllers
{
    public class FolderCounter
    {
        
        IDiscReadable _reader { get; set; }

    public FolderCounter(IDiscReadable reader)
        {
            _reader = reader;
        }
        public async Task <List<FIleObject>> CountSizeFoldersOfFolderViewModelAsync(List<FIleObject> fIleObjects)
        { 
            foreach (var item in fIleObjects)
            {
                if (item is FolderModel)
                {
                    var size = await CountSizeFolderAsync(item.FullPath);
                    item.Size = size;
                }
            }
            return fIleObjects;
        }

        private async Task<long> CountSizeFolderAsync(string folderName)
        {
            var innerfolders = await SearchFoldersAsync(folderName);
            var size = await CountSizeFoldersAsync(innerfolders);
            return size;
        }

        private async Task<long> CountSizeFoldersAsync(List<string> folders)
        {
            return await Task.Run(() => CountSizeFolders(folders));
        }

        private  long CountSizeFolders(List<string> folders)
        {
            var result = new long();
            Parallel.ForEach(folders, item =>
            {
                try
                {
                    result += CountSizeFolderFiles(item);
                }
                catch (Exception)
                {/*dosmth*/}
                Console.WriteLine(result / 1024 / 1024);
            });
            return result;
        }

        private async Task<List<string>> SearchFoldersAsync(string path)
        {
            return await Task.Run(() => SearchFolders(path));
        }

        private List<string> SearchFolders(string path)
        {
            var folders = new List<string>();// todo
            folders.Add(path);
            for (int i = 0; i < folders.Count; i++)
            {
                try
                {
                    var innnerFolders = _reader.GetDirectories(folders[i]).Select(n=>n.FullPath);
                    folders = folders.Concat(innnerFolders).ToList();
                }
                catch (Exception)
                {/*dosmth*/}
            }
            return folders;
        }
        private long CountSizeFolderFiles(string path)
        {
            var files = _reader.GetFiles(path);
            var size = files.Select(i => i.Size).Sum();
            return size;
        }
    }
}
