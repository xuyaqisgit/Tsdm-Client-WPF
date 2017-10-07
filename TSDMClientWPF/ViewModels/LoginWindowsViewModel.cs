using LaCODESoftware.Tsdm.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows;
using LaCODESoftware.Tsdm.Views;

namespace LaCODESoftware.Tsdm.ViewModels
{
    public class LoginWindowsViewModel : ViewModelBase
    {
        private Person _person;
        public Person Person
        {
            get
            {
                return _person;
            }
            set
            {
                if (_person != value)
                {
                    _person = value;
                    OnPropertyChanged("Person");
                }
            }
        }
        private BitmapImage _avatar;
        public BitmapImage Avatar
        {
            get
            {
                return _avatar;
            }
            set
            {
                if (_avatar != value)
                {
                    _avatar = value;
                    OnPropertyChanged("Avatar");
                }
            }
        }
        private BitmapImage _verfyCodeStream;
        public BitmapImage VerfyCodeStream
        {
            get
            {
                return _verfyCodeStream;
            }
            set
            {
                if (_verfyCodeStream != value)
                {
                    _verfyCodeStream = value;
                    OnPropertyChanged("VerfyCodeStream");
                }
            }
        }
        private string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged("Password");
                }
            }
        }
        private string _username;
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged("Username");
                }
            }
        }
        private string _verfyCode;
        public string VerfyCode
        {
            get
            {
                return _verfyCode;
            }
            set
            {
                if (_verfyCode != value)
                {
                    _verfyCode = value;
                    OnPropertyChanged("VerfyCode");
                }
            }
        }
        private List<ComboBoxItem> _logWays;
        public List<ComboBoxItem> LogWays
        {
            get
            {
                return _logWays;
            }
            set
            {
                if (_logWays != value)
                {
                    _logWays = value;
                    OnPropertyChanged("LogWays");
                }
            }
        }
        private int _selectedLogWaysIndex;
        public int SelectedLogWaysIndex
        {
            get
            {
                return _selectedLogWaysIndex;
            }
            set
            {
                if (_selectedLogWaysIndex != value)
                {
                    _selectedLogWaysIndex = value;
                    OnPropertyChanged("SelectedLogWaysIndex");
                }
            }
        }
        private bool _loginComplete = false;
        public bool LoginComplete
        {
            get
            {
                return _loginComplete;
            }
            set
            {
                if (_loginComplete != value)
                {
                    _loginComplete = value;
                    OnPropertyChanged("LoginComplete");
                }
            }
        }
        private bool _userexist = false;
        public bool Userexist
        {
            get
            {
                return _userexist;
            }
            set
            {
                if (_userexist != value)
                {
                    _userexist = value;
                    OnPropertyChanged("Userexist");
                }
            }
        }
        private LoginWindow _window;
        public LoginWindow Window
        {
            get
            {
                return _window;
            }
            set
            {
                if (_window != value)
                {
                    _window = value;
                    OnPropertyChanged("Window");
                }
            }
        }

        public LoginWindowsViewModel(LoginWindow window)
        {
            LogWays = new List<ComboBoxItem>
            {
                new ComboBoxItem() { Name = "username", Content = "用户名" },
                new ComboBoxItem() { Name = "uid", Content = "Uid" },
                new ComboBoxItem() { Name = "email", Content = "邮箱" }
            };
            _loginWindowCommands = new Commands.LoginWindowCommands(this);
            Avatar = new BitmapImage(new Uri("http://www.tsdm.me/uc_server/images/noavatar_middle.gif"));
            VerfyCodeStream = new BitmapImage(new Uri("http://www.tsdm.me/plugin.php?id=oracle:verify"));
            Person = new Person();
            Window = window;
        }

        private Commands.LoginWindowCommands _loginWindowCommands;
        public Commands.LoginWindowCommands LoginWindowCommands { get { return _loginWindowCommands; } }
    }
}
