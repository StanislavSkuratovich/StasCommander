using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeSize.Classes;
using TreeSize.Persistence;
using TreeSize.ViewModels;

namespace TreeSize.Controllers
{
    public class ViewModelProvider
    {
        IDiscReadable _discReader { get; set; }
        public FolderCounter _folderSizeCounter { get; set; }

        public ViewModelProvider(IDiscReadable discReader)
        {
            _discReader = discReader;
            _folderSizeCounter = new FolderCounter(discReader);
        }

        public async Task <FolderViewModel> CountSizeFoldersOfViewModelAsync (FolderViewModel model)
        {
            model.FIleObjects = await _folderSizeCounter.CountSizeFoldersOfFolderViewModelAsync(model.FIleObjects);
            return model;
        }

        public FolderViewModel CreateFolderViewModel(string path, bool needCountFoldersSize)
        {
            var model = new FolderViewModel();
            model.FIleObjects = CreateListOfFileobjects(path);
            model.Name = path;
            return model;
        }

        private List<FIleObject> CreateListOfFileobjects(string path)
        {
            var subFiles = _discReader.GetFileObjects(path);
            return subFiles;
        }       
    }
}
