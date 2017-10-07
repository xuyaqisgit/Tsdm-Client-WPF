using LaCODESoftware.Tsdm.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace LaCODESoftware.Tsdm.Commands
{
    public class LoginWindowCommands : ICommand
    {
        private LoginWindowsViewModel loginWindow;

        public LoginWindowCommands(LoginWindowsViewModel loginWindowsViewModel)
        {
            loginWindow = loginWindowsViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
                return true;
        }

        public void Execute(object parameter)
        {
            string command = (string)parameter;
            if (command=="Login")
            {
                Login();
            }
            else if (command=="Check")
            {
                if (!loginWindow.Userexist)
                {
                    Check();
                }
            }
            else if (command=="Renew")
            {
                loginWindow.VerfyCodeStream = new BitmapImage();
                loginWindow.VerfyCodeStream = new BitmapImage(new Uri("http://www.tsdm.me/plugin.php?id=oracle:verify"));
            }
        }
        public async void Login()
        {
            Tuple<bool, CookieContainer> tuple = await TsdmHelper.LogInAsync(loginWindow.Username, loginWindow.Password, loginWindow.VerfyCode, loginWindow.LogWays[loginWindow.SelectedLogWaysIndex].Name);
            if (tuple.Item1)
            {
                loginWindow.Person.PersonCookie = tuple.Item2;
                Json personinfo = (await TsdmHelper.UserInfoAsync(tuple.Item2)).Item1;
                loginWindow.Person.ImageUri = new Uri(personinfo.avatar);
                loginWindow.Person.Uid = Int32.Parse(personinfo.uid);
                loginWindow.Person.ReadAcess = Int32.Parse(personinfo.readaccess);
                loginWindow.Person.Username = personinfo.username;
                loginWindow.LoginComplete = true;
                loginWindow.Window.Person = loginWindow.Person;
                loginWindow.Window.Close();
            }
            else
            {
                MessageBox.Show("登录失败", String.Format("因某些原因登录失败错误代码:{0}", "Log_Failed"), MessageBoxButton.OK);
                loginWindow.VerfyCodeStream = new BitmapImage(new Uri("http://www.tsdm.me/plugin.php?id=oracle:verify"));
            }
        }
        public async void Check()
        {
            Json personinfo = (await TsdmHelper.UserInfoAsync(loginWindow.Username, loginWindow.LogWays[loginWindow.SelectedLogWaysIndex].Name));
            if (personinfo.status == 0)
            {
                loginWindow.Userexist = true;
                loginWindow.Avatar = new BitmapImage(new Uri(personinfo.avatar));
            }
        }
    }
}
