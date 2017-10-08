using LaCODESoftware.Tsdm.Data;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Collections.ObjectModel;

namespace LaCODESoftware.Tsdm.ViewModels
{
    public class MainWindowsViewModel : ViewModelBase
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
        private PersonCollection _personCollection;
        public PersonCollection PersonCollection
        {
            get
            {
                return _personCollection;
            }
            set
            {
                if (_personCollection != value)
                {
                    _personCollection = value;
                    OnPropertyChanged("PersonCollection");
                }
            }
        }
        private Page _pages;
        public Page Pages
        {
            get
            {
                return _pages;
            }
            set
            {
                if (_pages != value)
                {
                    _pages = value;
                    OnPropertyChanged("Pages");
                }
            }
        }
        private ObservableCollection<ForumList> _forumCollection;
        public ObservableCollection<ForumList> ForumCollection
        {
            get
            {
                return _forumCollection;
            }
            set
            {
                if (_forumCollection != value)
                {
                    _forumCollection = value;
                    OnPropertyChanged("ForumCollection");
                }
            }
        }
        private ObservableCollection<ForumList> _groupCollection;
        public ObservableCollection<ForumList> GroupCollection
        {
            get
            {
                return _groupCollection;
            }
            set
            {
                if (_groupCollection != value)
                {
                    _groupCollection = value;
                    OnPropertyChanged("GroupCollection");
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
        public MainWindowsViewModel()
        {
            Person = new Person();
            PersonCollection = new PersonCollection();
            ForumCollection = new ObservableCollection<ForumList>();
            GroupCollection = new ObservableCollection<ForumList>();
            _mainPageCommands = new Commands.MainPageCommands(this);
        }
        private Commands.MainPageCommands _mainPageCommands;
        public Commands.MainPageCommands MainPageCommands { get { return _mainPageCommands; } }
    }
}
