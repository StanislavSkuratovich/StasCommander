using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
//using System.IO;
using TreeSize.Controllers;
using TreeSize.Classes;
using System.Diagnostics;
using TreeSize.Views;
using TreeSize.ViewModels;

namespace TreeSize
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ViewModelProvider _provider { get; set; }        
        private string _currentChosenPath { get; set; }
        private  string DefaultPath { get; set; }
        public FolderViewModel ViewModel { get; set; }
        public TreeSize.Controllers.FIlePlaceChanger PlaceChanger { get; set; }
        public TreeSize.Persistence.IDiscReadable _reader { get; set; }
        public TreeSize.Persistence.IDiscReWritable _reWriter { get; set; }

        private Stack<string> myVar;

        public string PreviousPath
        {            
            get 
            {
                if(myVar.Count == 1)
                {
                    return myVar.Peek();
                }
                return myVar.Pop();
            }
            set { myVar.Push(value); }            
        }
        public MainWindow()
        {
            var fileAccess = new Persistence.HardDrivesRepository();
            _reader = fileAccess;
            _reWriter = fileAccess;
            _provider = new ViewModelProvider(fileAccess);
            InitializeComponent();
            myVar = new Stack<string>();
            PlaceChanger = new FIlePlaceChanger(fileAccess, fileAccess);
        }

        private void Mainwindow_loaded(object sender, EventArgs e)
        {
            var diskNames = _reader.GetDiscs();
            foreach (var item in diskNames)
            {
                DiscSelector.Items.Add(item);
            }
            FillViewModel(diskNames[0]);
            DiscSelector.SelectedIndex = 0;
            DiscSelector.Text = diskNames[0];
            _currentChosenPath = diskNames[0];
            DefaultPath = diskNames[0];
            PreviousPath = _currentChosenPath;
            RefreshDirectoriesField(_currentChosenPath);            
        }

        private void FillViewModel(string path, bool isNeedCountFoldersSize = false)
        {
            ViewModel = _provider.CreateFolderViewModel(path, isNeedCountFoldersSize);
        }

        private async void RefreshDirectoriesField(string path)
        {
            var IsNeedCountSizeFolders = (CountFolderSizeTrigger.IsChecked == true);
            FillViewModel(path, IsNeedCountSizeFolders);
            if (IsNeedCountSizeFolders)
            {
                ViewModel = await _provider.CountSizeFoldersOfViewModelAsync(ViewModel);
            }
            DirectoriesField.ItemsSource = ViewModel.FIleObjects;
        }

        private void DirectoriesField_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PreviousPath = _currentChosenPath;
            var fileObject = DirectoriesField.SelectedItem;
            if (fileObject.GetType() == typeof(FileModel))
            {
                FIleObject file = (FIleObject)fileObject;
                Process.Start(file.FullPath);
            }
            else
            {
                FolderModel folder = (FolderModel)fileObject;
                this._currentChosenPath = folder.FullPath;                
                RefreshDirectoriesField(_currentChosenPath);
            }
        }

        private void DirectoriesField_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
           if (DirectoriesField.SelectedItem is FIleObject)
            {
                var fileObject = DirectoriesField.SelectedItem as FIleObject;// todo check is path
                var actionsWindow = new FileActionsWindow(fileObject.FullPath, PlaceChanger);

                actionsWindow.Show();                
            }

        }

        private void DiscSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string path = DiscSelector.SelectedItem.ToString();            
            _currentChosenPath = path;
            PreviousPath = path;
            RefreshDirectoriesField(path);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var path = PreviousPath;
            RefreshDirectoriesField(path);
        }
    }
}
