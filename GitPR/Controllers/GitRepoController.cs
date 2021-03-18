using GitPR.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GitPR.Controllers
{
    public class GitRepoController : Controller
    {
        // GET: GitRepo
        public ActionResult Index()
        {
            string Url = "https://api.github.com/users";
            //string Url = "https://api.github.com/users/{myuser}/repos";
            CookieContainer cookieJar = new CookieContainer();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.CookieContainer = cookieJar;
            request.UseDefaultCredentials = true;
            request.UserAgent = "Foo";
            request.Accept = "application/json";
            request.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            String htmlString;
            IList<GitRepoClass> rep = null;
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                htmlString = reader.ReadToEnd();
                rep = JsonConvert.DeserializeObject<List<GitRepoClass>>(htmlString);
            }

            return View(rep);
        }
    }
}