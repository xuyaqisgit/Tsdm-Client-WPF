using LaCODESoftware.Tsdm.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LaCODESoftware.Tsdm.Views;

namespace LaCODESoftware.Tsdm.Commands
{
    public enum MainPageCommandParameterType
    {
        MainWindowsLoaded=1,
        MainWindowsSearchClick=2,
    }
    public class MainPageCommands : ICommand
    {
        private MainWindowsViewModel _data;
        public MainPageCommands(MainWindowsViewModel data)
        {
            _data = data;
        }
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            MainPageCommandParameterType type = (MainPageCommandParameterType)Enum.Parse(typeof(MainPageCommandParameterType), (string)parameter, true);
            switch (type)
            {
                case MainPageCommandParameterType.MainWindowsLoaded:
                    _data.Pages = new LoadingPage();
                    break;
                
            }
        }
    }
}
