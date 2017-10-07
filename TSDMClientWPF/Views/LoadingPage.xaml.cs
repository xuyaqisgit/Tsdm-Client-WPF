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
using LaCODESoftware.BasicHelper;
using LaCODESoftware.Tsdm.ViewModels;
using LaCODESoftware.Tsdm.Data;

namespace LaCODESoftware.Tsdm.Views
{
    /// <summary>
    /// LoadingPage.xaml 的交互逻辑
    /// </summary>
    public partial class LoadingPage : Page
    {
        public MainWindowsViewModel MainWindowsViewModel { get; set; }
        public delegate void ProgramLoadingFinishedHandler(object sender, EventArgs e);
        public event ProgramLoadingFinishedHandler ProgramLoadingFinished;
        public LoadingPage()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Json json = new Json();
            #region 启动时测试网络链接
            try
            {
                json = await TsdmHelper.GetForumAsync("", new System.Net.CookieContainer());
            }
            catch (Exception ex)
            {
                MessageBox.Show("程序启动失败", String.Format("Oops，我们遇到了问题，程序将在点击确认后关闭，错误为{0}", ex), MessageBoxButton.OK);
                MainWindowsViewModel.Islogged = false;
                this.ProgramLoadingFinished(MainWindowsViewModel ,new EventArgs());
            }
            #endregion
            #region 查询登录信息并询问登录
            MainWindowsViewModel.PersonCollection = StreamHelper.ReadObjectFromDisk<PersonCollection>("cookie");
            if (MainWindowsViewModel.PersonCollection.Count == 0)
            {
                (new LoginWindow()).Show();
            }
            #endregion
            #region 取得论坛图标和更新信息
            json = await TsdmHelper.GetForumAsync("", MainWindowsViewModel.Person.PersonCookie);
            foreach (var item in json.group)
            {
                Json _json = await TsdmHelper.GetForumAsync(item.gid, MainWindowsViewModel.Person.PersonCookie);
                ForumList forumList = new ForumList() { GroupName = item.title, Gid = item.gid };
                foreach (var _item in _json.forum)
                {
                    Json forum = await TsdmHelper.GetForumAsync(_item.fid, "1", MainWindowsViewModel.Person.PersonCookie);
                    forumList.Add(new Data.Forum() { Fid = _item.fid, Title = _item.title, Todaypost = _item.todaypost, ForumCover = forum.forum_cover });
                }
                MainWindowsViewModel.ForumCollection.Add(forumList);
            }
            #endregion
            MainWindowsViewModel.Islogged = true;
            this.ProgramLoadingFinished(MainWindowsViewModel, new EventArgs());
        }
    }
}
