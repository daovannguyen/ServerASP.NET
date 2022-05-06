using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data.SqlClient;
using System.Web.Script.Serialization;

namespace Web2.Controllers
{
    public class HomeController : Controller
    {
        public class Player
        {
            public string Name;
            public string Username;
        }
        //[HttpPost]
        public ActionResult Index()//FormCollection collection)
        {
            //string x = collection["data"];
            //string path = Server.MapPath("TextFile.txt");
            //StreamWriter SW = new StreamWriter(path, false);
            //SW.Close();
            //ViewBag.Content = collection["data"];
            ////return View();
            ///
            //string path = Server.MapPath("TextFile.txt");
            ////StreamReader SW = new StreamReader(path);
            ////ViewBag.Content = SW.ReadToEnd();
            ////SW.Close();
            //return View();


            //Player a = new Player();
            //ViewBag.Content = new JavaScriptSerializer().Serialize(a);
            //return View();

            string json = "{\"Name\":\"Dao Van Nguyen\",\"Username\":\"DaoVanNguyen\"}";
            Player a = new JavaScriptSerializer().Deserialize<Player>(json);

            ViewBag.Content = a.Name;
            return View();

        }

        [HttpPost]
        public ActionResult Hello(FormCollection collection)
        {
            string a = collection["data"];
            ViewBag.Content = a;
            return View();
        }

    }
}