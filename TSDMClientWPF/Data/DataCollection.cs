using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace LaCODESoftware.Tsdm.Data
{
    [Serializable()]
    public class PersonCollection : ObservableCollection<Person>
    {
        public int LastLog { get; set; }
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

        private string imageUri;

        public string ImageUri
        {
            get { return imageUri; }
            set { imageUri = value; }
        }

        private string timeStamp;

        public string TimeStamp
        {
            get { return timeStamp; }
            set { timeStamp = value; }
        }

    }
    public class ForumList
    {
        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private Parameter parameter;

        public Parameter Parameter
        {
            get { return parameter; }
            set { parameter = value; }
        }

    }

    public class Parameter
    {
        private string parameter1;

        public string Parameter1
        {
            get { return parameter1; }
            set { parameter1 = value; }
        }
        private string parameter2;

        public string Parameter2
        {
            get { return parameter2; }
            set { parameter2 = value; }
        }

        public static implicit operator Parameter(Tuple<string, string> v)
        {
            return new Parameter() { Parameter1 = v.Item1, Parameter2 = v.Item2 };
        }
    }
}
