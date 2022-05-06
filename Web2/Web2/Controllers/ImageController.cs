using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web2.Models;

namespace Web2.Controllers
{
    public class ImageController : Controller
    {
        public ActionResult Get(string uri)
        {
            var path = Server.MapPath(PathServer.Image + uri);
            return base.File(path, "image/jpeg");
            //ViewBag.Content = path;
            //return View();
        }

        [HttpPost]
        public ActionResult Post(string uri, string base64String)
        {
            var path = Server.MapPath(PathServer.Image + uri);
            WorkFile.WriteBase64stringToBytes(base64String, path);
            ViewBag.Content = "Da xong!!!";
            return View();
        }
    }
}