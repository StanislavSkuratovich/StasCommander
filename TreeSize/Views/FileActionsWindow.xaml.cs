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
using System.Windows.Shapes;

namespace TreeSize.Views
{
    /// <summary>
    /// Логика взаимодействия для FileActionsWindow.xaml
    /// </summary>
    public partial class FileActionsWindow : Window
    {
        private TreeSize.Controllers.FIlePlaceChanger _placeChanger;
        private string SelectedPath { get; set; }
        public FileActionsWindow(string path, Controllers.FIlePlaceChanger placeChanger)
        {
            _placeChanger = placeChanger;
            SelectedPath = path;
            InitializeComponent();
        }

      

        private void Panel_Click(object sender, RoutedEventArgs e)
        {

            var button = sender as Button;
            string selectedActionName = button.Content as string;// name of a button which was chosen            
            var method = _placeChanger.ChooseMethod(selectedActionName);
            method.Invoke(this.SelectedPath);         
            this.Close();
        }        
    }
}
