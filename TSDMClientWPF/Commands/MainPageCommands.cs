using LaCODESoftware.Tsdm.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LaCODESoftware.Tsdm.Views;
using System.Windows.Controls;
using LaCODESoftware.Tsdm.Data;
using System.Windows;

namespace LaCODESoftware.Tsdm.Commands
{
    public enum MainPageCommandParameterType
    {
        gid=1,
        fid=2,
        search=3,
        userinfo=4,
        setting=5,
        message=6,
        check=7
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
            Parameter tuple = (Parameter)parameter;
            MainPageCommandParameterType type = (MainPageCommandParameterType)Enum.Parse(typeof(MainPageCommandParameterType), tuple.Parameter2, true);
            switch (type)
            {
                case MainPageCommandParameterType.gid:
                    GetForumList(tuple);
                    break;
                case MainPageCommandParameterType.check:
                    Check();
                    break;
            }
        }
        private async void GetForumList(Parameter tuple)
        {
            Json json = await TsdmHelper.GetForumAsync(tuple.Parameter1, _data.Person.PersonCookie);
            _data.ForumCollection.Clear();
            foreach (var item in json.forum)
            {
                ForumList forumList = new ForumList() { Title = item.title, Parameter = new Parameter() { Parameter1 = item.fid, Parameter2 = "fid" } };
                _data.ForumCollection.Add(forumList);
            }
        }
        private async void Check()
        {
            string CheckCallBack = await TsdmHelper.CheckAsync(_data.Person.PersonCookie);
            MessageBox.Show(CheckCallBack, "签到回执", MessageBoxButton.OK);
        }
    }
}
