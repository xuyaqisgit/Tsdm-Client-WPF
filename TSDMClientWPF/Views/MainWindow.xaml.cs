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
using System.Windows.Shapes;

namespace LaCODESoftware.Tsdm.Views
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public LoadingPage LoadingPage { get; set; }
        public MainPage MainPage { get; set; }
        public MainWindowsViewModel MainWindowsViewModel { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            LoadingPage = new LoadingPage();
            MainPage = new MainPage();
            MainPage.DataContextChanged += MainPage_DataContextChanged;
        }

        private void MainPage_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.DataContext = MainPage.DataContext;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainPage = new MainPage();
            LoadingPage.ProgramLoadingFinished += LoadingPage_ProgramLoadingFinished;
            ShellFrame.NavigationService.Navigate(LoadingPage);
        }

        private void LoadingPage_ProgramLoadingFinished(object sender, EventArgs e)
        {
            MainWindowsViewModel = sender as MainWindowsViewModel;
            if (MainWindowsViewModel.LoginComplete)
            {
                this.DataContext = MainWindowsViewModel;
                ShellFrame.NavigationService.Navigate(MainPage);
                ShellFrame.NavigationService.RemoveBackEntry();
                MainPage.DataContext = this.DataContext;
            }
            else
            {
                Close();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindowsViewModel = (MainWindowsViewModel)this.DataContext;
            MainWindowsViewModel.PersonCollection.LastLog = MainWindowsViewModel.PersonCollection.IndexOf(MainWindowsViewModel.Person);
            BasicHelper.StreamHelper.WriteObjectToDisk("cookie", MainWindowsViewModel.PersonCollection);
        }
    }
}
