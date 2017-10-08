using LaCODESoftware.Tsdm.ViewModels;
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

namespace LaCODESoftware.Tsdm.Views
{
    /// <summary>
    /// MainPage.xaml 的交互逻辑
    /// </summary>
    public partial class MainPage : Page
    {
        public MainWindowsViewModel MainWindowsViewModel { get; set; }
        public MainPage()
        {
            InitializeComponent();
        }
        public MainPage(MainWindowsViewModel mainWindowsViewModel)
        {
            InitializeComponent();
            this.MainWindowsViewModel = mainWindowsViewModel;
            if (mainWindowsViewModel.Person.ImageUri.Length>0)
            {
                MainWindowsViewModel.Avatar = new BitmapImage(new Uri(mainWindowsViewModel.Person.ImageUri));
            }
            else
            {
                MainWindowsViewModel.Avatar = new BitmapImage(new Uri("http://www.tsdm.me/uc_server/images/noavatar_middle.gif"));
            }
            this.DataContext = MainWindowsViewModel;
        }
    }
}
