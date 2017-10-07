using LaCODESoftware.Tsdm.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

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
        private ForumCollection _forumCollection;
        public ForumCollection ForumCollection
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

    }
}
