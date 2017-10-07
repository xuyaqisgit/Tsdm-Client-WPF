using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LaCODESoftware.Tsdm.Data
{
    [Serializable()]
    public class PersonCollection : ObservableCollection<Person>
    {
    }
    [Serializable()]
    public class Person
    {
        private CookieContainer _PersonCookie;

        public CookieContainer PersonCookie
        {
            get { return _PersonCookie; }
            set { _PersonCookie = value; }
        }

        private int uid;

        public int Uid
        {
            get { return uid; }
            set { uid = value; }
        }

        private int readAcess;

        public int ReadAcess
        {
            get { return readAcess; }
            set { readAcess = value; }
        }

        private string userName;

        public string Username
        {
            get { return userName; }
            set { userName = value; }
        }

        private Uri imageUri;

        public Uri ImageUri
        {
            get { return imageUri; }
            set { imageUri = value; }
        }

    }
    public class ForumCollection : ObservableCollection<ForumList>
    {

    }
    public class ForumList : ObservableCollection<Forum>
    {
        public string Gid { get; set; }
        public string GroupName { get; set; }
    }
    public class Forum
    {
        public string Fid { get; set; }
        public string Title { get; set; }
        public string Todaypost { get; set; }
        public string ForumCover { get; set; }
    }

}
