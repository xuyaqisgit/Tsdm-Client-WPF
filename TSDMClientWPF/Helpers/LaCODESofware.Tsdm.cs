using LaCODESoftware.BasicHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace LaCODESoftware.Tsdm
{
    /// <summary>
    /// 天使动漫api
    /// </summary>
    public class TsdmHelper
    {
        private static string webcss = StreamHelper.StreamToString(Assembly.GetExecutingAssembly().GetManifestResourceStream("LaCODESoftware.Tsdm.Assets.WebStyle.css")).Replace("\r", "").Replace("\n", "");
        /// <summary>
        /// 在网站搜索指定内容
        /// </summary>
        /// <param name="body">内容</param>
        /// <returns>返回搜索所得到的字符串形式html</returns>
        public static async Task<string> SreachAsync(string body,string page,CookieContainer cookie)
        {
            Tuple<Stream, CookieContainer> tuple = await WebHelper.GetStreamAsync(cookie, String.Format("http://www.tsdm.me/plugin.php?id=Kahrpba:search&query={0}&mobile=yes&page={1}", body,page));
            string[] temp = StreamHelper.StreamToString(tuple.Item1).Split('\n');
            string head = "<html xmlns=\"http://www.w3.org/1999/xhtml\"><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" /><base href=\"http://www.tsdm.me/\" />" +
                "<style>.ts_se_rs {width:600px;overflow:hidden;margin-bottom: 23px;}.r_ a{cursor: pointer;color: #1a0dab;font-size: medium;}.r_ a em{color: #FF0000;}.a_ {}.a_ a {color: #006621;font-size: 13px;}.dateshow {color: #808080;}" +
                "#otherSE li span{margin-left:10px;}#otherSE li a {font-weight: bold;}#otherSE {margin-top:20px;}.sel {color:#FF0000}.setur {color:#0000FF;margin-left:10px}.otip {color:#669900}* {word-wrap: break-word;}body {width:290;}" +
                "</style></head><body>";
            string post = "";
            for (int i = 283; i < temp.Length-41; i++)
            {
                post += temp[i];
            }
            return head + post.Replace(" target=\"_blank\"", "") + "</body></html>";
        }
        /// <summary>
        /// 主题购买
        /// </summary>
        /// <param name="tid">帖子代码</param>
        public static async Task PayAsync(string tid,CookieContainer cookie)
        {
            Tuple<Stream, CookieContainer> tuple = await WebHelper.GetStreamAsync(cookie, "http://www.tsdm.me/forum.php?mobile=yes&tsdmapp=3&mod=post&action=reply&tid=628244");
            Json json = JsonHelper.DataContractJsonDeserialize<Json>(tuple.Item1);
            string formhash = json.formhash;
            await WebHelper.GetStreamAsync(cookie, "http://www.tsdm.me/forum.php?mod=misc&action=pay&mobile=yes&paysubmit=yes&infloat=yes", String.Format("formhash={0}&referer=http://www.tsdm.me/./&tid={1}&paysubmit=true", formhash, tid));
        }
        /// <summary>
        /// 获取帖子内容
        /// </summary>
        /// <param name="tid">帖子代码</param>
        /// <param name="page">页码</param>
        /// <returns>返回字符串形式Html网页</returns>
        public static async Task<string> GetThreadAsync(string tid, string page, CookieContainer cookie)
        {
            Tuple<Stream, CookieContainer> tuple = await WebHelper.GetStreamAsync(cookie, String.Format("http://www.tsdm.me/forum.php?mod=viewthread&mobile=yes&tsdmapp=3&tid={0}&page={1} ", tid, page));
            string result = StreamHelper.StreamToString(tuple.Item1).Replace("\n", "").Replace("\r", "");
            if (result.Contains("<?xml") == true)
            {
                return "xml_cannot_deserialize";
            }
            Json json = JsonHelper.DataContractJsonDeserialize<Json>(result);
            if (json.status == -1)
            {
                return "可能没有相关权限，见系统回复：\n"+json.message+ "</body></html>";
            }
            else
            {
                string head = "<html xmlns=\"http://www.w3.org/1999/xhtml\"><head>" +
                    "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" /><base href=\"http://www.tsdm.me/\" />" +
                    String.Format("<style type=\"text/css\">{0}</style></head>",webcss);
                string post = "";
                if (json.thread_price != null && json.thread_paid == "0")
                {
                    post += String.Format("<div><a href=\"http://www.tsdm.me/forum.php?mod=misc&action=pay&mobile=yes&paysubmit=yes&infloat=yes\">购买主题 价格:{0}</a><div>", json.thread_price);
                }
                foreach (var item in json.postlist)
                {
                    post += String.Format("<table class=\"post_table\"><tbody><tr><td class=\"user_td\"><div class=\"user_div\"><div class=\"user_avatar_div\"><img class=\"avatar_img\" src=\"{0}\" onerror=\"this.onerror=null;this.src=\'http://www.tsdm.me/uc_server/images/noavatar_middle.gif\'\" /></div><divclass=\"username_div\">{1}</div><div class=\"nickname_div\">{2}</div><div class=\"userlevel_div\">{3}</div></div></td><td class=\"mainpost_td\"><div><div><table class=\"mainpost_table\"><tbody><tr><td class=\"post_td\">{4}</td></tr></tbody></table></td></tr></tbody></table>", item.avatar.ToString(), item.author.ToString(), item.author_nickname.ToString(), item.authortitle.ToString(), item.message.ToString());
                }
                return head + "<body>" + post.Replace(" target=\"_blank\"", "") + "</body></html>";
            }
        }
        /// <summary>
        /// 取得板块列表
        /// </summary>
        /// <param name="gid">板块代码，当取""时为主页板块</param>
        /// <returns>返回包含所需信息的实体类</returns>
        public static async Task<Json> GetForumAsync(string gid, CookieContainer cookie)
        {
            Tuple<Stream, CookieContainer> tuple = await WebHelper.GetStreamAsync(cookie, String.Format("http://www.tsdm.me/forum.php?mobile=yes&tsdmapp=3&gid={0}", gid));
            return JsonHelper.DataContractJsonDeserialize<Json>(StreamHelper.StreamToString(tuple.Item1));
        }
        /// <summary>
        /// 取得帖子列表
        /// </summary>
        /// <param name="fid">板块代码</param>
        /// <param name="page">页码</param>
        /// <returns>返回包含所需信息的实体类</returns>
        public static async Task<Json> GetForumAsync(string fid, string page, CookieContainer cookie)
        {
            Tuple<Stream, CookieContainer> tuple = await WebHelper.GetStreamAsync(cookie, String.Format("http://www.tsdm.me/forum.php?mobile=yes&tsdmapp=3&mod=forumdisplay&fid={0}&page={1}", fid, page));
            return JsonHelper.DataContractJsonDeserialize<Json>((StreamHelper.StreamToString(tuple.Item1)));
        }
        /// <summary>
        /// 签到
        /// </summary>
        /// <returns>返回签到回执信息</returns>
        public static async Task<string> CheckAsync(CookieContainer cookie)
        {
            Tuple<Stream, CookieContainer> tuple = await WebHelper.GetStreamAsync(cookie, "http://www.tsdm.me/forum.php?mobile=yes&tsdmapp=3&mod=post&action=reply&tid=628244");
            Json json = JsonHelper.DataContractJsonDeserialize<Json>(StreamHelper.StreamToString(tuple.Item1));
            tuple = await WebHelper.GetStreamAsync(cookie, "http://www.tsdm.me/plugin.php?mobile=yes&tsdmapp=3&id=dsu_paulsign:sign&operation=qiandao&infloat=1&inajax=1", String.Format("qdmode=1&formhash={0}&fastreply=1&qdxq=kx&todaysay=winform客户端签到", json.formhash));
            json = JsonHelper.DataContractJsonDeserialize<Json>(StreamHelper.StreamToString(tuple.Item1));
            return json.message;
        }
        /// <summary>
        /// 回复帖子
        /// </summary>
        /// <param name="tid">帖子代码</param>
        /// <param name="body">回复内容</param>
        /// <returns>返回是否成功的布尔值</returns>
        public static async Task<Tuple<bool,string>> ReplyAsync(string tid, string body, CookieContainer cookie)
        {
            Tuple<Stream, CookieContainer> tuple = await WebHelper.GetStreamAsync(cookie, String.Format("http://www.tsdm.me/forum.php?mobile=yes&tsdmapp=3&mod=post&action=reply&tid={0}", tid));
            Json json = JsonHelper.DataContractJsonDeserialize<Json>(StreamHelper.StreamToString(tuple.Item1));
            tuple = await WebHelper.GetStreamAsync(cookie, "http://www.tsdm.me/forum.php?mobile=yes&tsdmapp=2&mod=post&action=reply&replysubmit=yes", String.Format("message={0}&formhash={1}&clienthash=DC0EEB7B38AA1F07AF76895A8E14747B&tid={2}&", body, json.formhash, tid));
            json = JsonHelper.DataContractJsonDeserialize<Json>(StreamHelper.StreamToString(tuple.Item1));
            if (json.message == "post_reply_succeed")
            {
                return new Tuple<bool, string>(true,"回复成功");
            }
            else
            {
                return new Tuple<bool, string>(false, "回复失败系统回复："+json.message);
            }
        }
        /// <summary>
        /// 取得用户信息并更新Cookie
        /// </summary>
        /// <returns>返回包含所需信息的实体类</returns>
        public static async Task<Tuple<Json,CookieContainer>> UserInfoAsync(CookieContainer cookie)
        {
            Tuple<Stream, CookieContainer> tuple = await WebHelper.GetStreamAsync(cookie, "http://www.tsdm.me/home.php?mobile=yes&tsdmapp=3&mod=space&do=profile ");
            return new Tuple<Json, CookieContainer>(JsonHelper.DataContractJsonDeserialize<Json>(StreamHelper.StreamToString(tuple.Item1)),tuple.Item2);
        }
        /// <summary>
        /// 取得指定用户信息
        /// </summary>
        /// <param name="user">用户属性</param>
        /// <param name="way">用户属性代码</param>
        /// <returns></returns>
        public static async Task<Json> UserInfoAsync(string user,string way)
        {
            Tuple<Stream, CookieContainer> tuple = await WebHelper.GetStreamAsync(new CookieContainer(), String.Format("http://www.tsdm.me/home.php?mobile=yes&tsdmapp=3&mod=space&do=profile&{0}={1}",way,user));
            return JsonHelper.DataContractJsonDeserialize<Json>(StreamHelper.StreamToString(tuple.Item1));
        }
        /// <summary>
        /// 取得验证码流
        /// </summary>
        /// <returns>返回流</returns>
        public static Stream VerifyCode() => WebHelper.GetStream("http://www.tsdm.me/plugin.php?id=oracle:verify");

        /// <summary>
        /// 登录论坛
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="verifycode">验证码</param>
        /// <param name="loginfield">登录方法(在email,username,uid中选择)</param>
        /// <returns>返回是否登录成功的布尔值</returns>
        public static async Task<Tuple<bool,CookieContainer>> LogInAsync(string username, string password, string verifycode, string loginfield)
        {
            string body = String.Format("password={0}&tsdm_verify={1}&fastloginfield={2}&answer=&username={3}&questionid=0&", password, verifycode, loginfield, username);
            Tuple<Stream, CookieContainer> tuple = await WebHelper.GetStreamAsync(new CookieContainer(), "http://www.tsdm.me/member.php?mobile=yes&tsdmapp=3&mod=logging&action=login&loginsubmit=yes", body);
            Json LogResult = JsonHelper.DataContractJsonDeserialize<Json>(StreamHelper.StreamToString(tuple.Item1));
            if (LogResult.message == "location_login_succeed_mobile")
            {
                return new Tuple<bool,CookieContainer>(true,tuple.Item2);
            }
            else
            {
                return new Tuple<bool, CookieContainer>(false, tuple.Item2);
            }
        }
    }
    #region Json类及其所有子类都由计算机生成请勿修改
    public class Extra
    {
        public string login { get; set; }
    }
    public class Values
    {
        public string formulamessage { get; set; }
    }
    public class Author
    {
        public string login { get; set; }
        public int id { get; set; }
        public string avatar_url { get; set; }
        public string gravatar_id { get; set; }
        public string url { get; set; }
        public string html_url { get; set; }
        public string followers_url { get; set; }
        public string following_url { get; set; }
        public string gists_url { get; set; }
        public string starred_url { get; set; }
        public string subscriptions_url { get; set; }
        public string organizations_url { get; set; }
        public string repos_url { get; set; }
        public string events_url { get; set; }
        public string received_events_url { get; set; }
        public string type { get; set; }
        public bool site_admin { get; set; }
    }
    public class Uploader
    {
        public string login { get; set; }
        public int id { get; set; }
        public string avatar_url { get; set; }
        public string gravatar_id { get; set; }
        public string url { get; set; }
        public string html_url { get; set; }
        public string followers_url { get; set; }
        public string following_url { get; set; }
        public string gists_url { get; set; }
        public string starred_url { get; set; }
        public string subscriptions_url { get; set; }
        public string organizations_url { get; set; }
        public string repos_url { get; set; }
        public string events_url { get; set; }
        public string received_events_url { get; set; }
        public string type { get; set; }
        public bool site_admin { get; set; }
    }
    public class Asset
    {
        public string url { get; set; }
        public string browser_download_url { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string label { get; set; }
        public string state { get; set; }
        public string content_type { get; set; }
        public int size { get; set; }
        public int download_count { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public Uploader uploader { get; set; }
    }
    public class Group
    {
        public string title { get; set; }
        public string gid { get; set; }
    }
    public class Forum
    {
        public string fid { get; set; }
        public string title { get; set; }
        public string todaypost { get; set; }
    }
    public class Thread
    {
        public string title { get; set; }
        public string tid { get; set; }
        public string authorid { get; set; }
        public string author { get; set; }
        public string displayorder { get; set; }
        public string showclass { get; set; }
        public string showstyle { get; set; }
        public string views { get; set; }
        public string replies { get; set; }
        public string lastpost { get; set; }
        public string lastposter { get; set; }
        public string typeid { get; set; }
        public string closed { get; set; }
        public string typehtml { get; set; }
        public string readperm { get; set; }
    }
    public class Subforum
    {
        public string fid { get; set; }
        public string name { get; set; }
    }
    public class Moderator
    {
        public string username { get; set; }
        public string uid { get; set; }
    }
    public class Recommend
    {
        public string tid { get; set; }
        public string title { get; set; }
    }
    public class Threadtype
    {
        public string typeid { get; set; }
        public string name { get; set; }
    }
    public class Ratelog
    {
        public string uid { get; set; }
        public string username { get; set; }
        public string status { get; set; }
        public IList<string> score { get; set; }
        public string reason { get; set; }
    }
    public class Postlist
    {
        public string pid { get; set; }
        public string author { get; set; }
        public string authorid { get; set; }
        public string avatar { get; set; }
        public string timestamp { get; set; }
        public string subject { get; set; }
        public string message { get; set; }
        public string first { get; set; }
        public string floor { get; set; }
        public object platform { get; set; }
        public string authortitle { get; set; }
        public string authorgid { get; set; }
        public IList<Ratelog> ratelog { get; set; }
        public IList<string> ratelogextcredits { get; set; }
        public string author_nickname { get; set; }
    }
    public class Extcreditsname
    {
    }
    /// <summary>
    /// Json类及其所有子类都由计算机生成请勿修改
    /// </summary>
    public class Json
    {
        public int status { get; set; }
        public IList<Group> group { get; set; }
        public string message { get; set; }
        public string uid { get; set; }
        public string username { get; set; }
        public string nickname { get; set; }
        public string gid { get; set; }
        public string aid { get; set; }
        public string credits { get; set; }
        public string miku { get; set; }
        public string threads { get; set; }
        public string posts { get; set; }
        public string readaccess { get; set; }
        public string regdate { get; set; }
        public string cpuid { get; set; }
        public string cpusername { get; set; }
        public string customstatus { get; set; }
        public string avatar { get; set; }
        public string extcredits1 { get; set; }
        public string extcredits2 { get; set; }
        public string extcredits3 { get; set; }
        public string extcredits4 { get; set; }
        public string extcredits5 { get; set; }
        public string extcredits6 { get; set; }
        public string extcredits7 { get; set; }
        public IList<Forum> forum { get; set; }
        public string groupname { get; set; }
        public IList<Thread> thread { get; set; }
        public string forum_cover { get; set; }
        public IList<Subforum> subforum { get; set; }
        public string threadcnt { get; set; }
        public string total { get; set; }
        public string forumname { get; set; }
        public string ismoderator { get; set; }
        public IList<Moderator> moderator { get; set; }
        public IList<Recommend> recommend { get; set; }
        public IList<Threadtype> threadtype { get; set; }
        public IList<Postlist> postlist { get; set; }
        public string totalpost { get; set; }
        public string tpp { get; set; }
        public string subject { get; set; }
        public string fid { get; set; }
        public string thread_author { get; set; }
        public string thread_authorid { get; set; }
        public Extcreditsname extcreditsname { get; set; }
        public string thread_price { get; set; }
        public string thread_paid { get; set; }
        public string formhash { get; set; }
        public string quotemsg { get; set; }
        public string sechash { get; set; }
        public string answerkey { get; set; }
        public string noticeauthor { get; set; }
        public string noticetrimstr { get; set; }
        public string noticeauthormsg { get; set; }
        public string tid { get; set; }
        public string tag_name { get; set; }
        public object url { get; set; }
        public string html_url { get; set; }
        public string assets_url { get; set; }
        public string upload_url { get; set; }
        public string tarball_url { get; set; }
        public string zipball_url { get; set; }
        public int id { get; set; }
        public string target_commitish { get; set; }
        public string name { get; set; }
        public string body { get; set; }
        public bool draft { get; set; }
        public bool prerelease { get; set; }
        public string created_at { get; set; }
        public string published_at { get; set; }
        public Author author { get; set; }
        public IList<Asset> assets { get; set; }
        public Extra extra { get; set; }
        public Values values { get; set; }
    }
    #endregion
}
