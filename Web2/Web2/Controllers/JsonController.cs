using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web2.Models;

namespace Web2.Controllers
{
    public class JsonController : Controller
    {
        // GET: Json
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            JsonASP pd = JsonConvert.DeserializeObject<JsonASP>(collection["data"]);

            string name = pd.Name;
            if (name == "")
            {
                name = GenerateRandomPassword(10);
            }
            WorkFile.WriteNewFile(GetPathJson(name), pd.Message);
            ViewBag.Content = pd.ToSuccess();
            return View();
        }

        public ActionResult Get()
        {
            ViewBag.Content = new JsonASP().ToSuccess();
            return View();
        }


        [HttpPost]
        public ActionResult Read(FormCollection collection)
        {
            string name = collection["data"];
            if (WorkFile.FIleExist(GetPathJson(name)))
            {
                ViewBag.Content = WorkFile.ReadFileAsText(GetPathJson(name));
            }
            else
            {
                ViewBag.Content = new JsonASP().ToError();
            }
            return View();
        }
        public ActionResult Delete()
        {
            DeleteFiles(0);
            ViewBag.Content = "Success";
            return View();
        }
        public static string GenerateRandomPassword(int length)
        {
            string allowedLetterChars = "abcdefghijkmnpqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ";
            string allowedNumberChars = "0123456789";
            char[] chars = new char[length];
            Random rd = new Random();
            bool useLetter = true;
            for (int i = 0; i < length; i++)
            {
                if (useLetter)
                {
                    chars[i] = allowedLetterChars[rd.Next(0, allowedLetterChars.Length)];
                    useLetter = false;
                }
                else
                {
                    chars[i] = allowedNumberChars[rd.Next(0, allowedNumberChars.Length)];
                    useLetter = true;
                }
            }
            return new string(chars);
        }
        public string GetPathJson(string name)
        {
            return Server.MapPath(PathServer.Json + name);
        }
        protected void DeleteFiles(double time)
        {
            string[] files = Directory.GetFiles(Server.MapPath(PathServer.Json));
            int iCnt = 0;

            foreach (string file in files)
            {

                FileInfo info = new FileInfo(file);

                info.Refresh();

                if (info.LastWriteTime <= DateTime.Now.AddDays(time))
                {
                    info.Delete();
                    iCnt += 1;
                }
            }
        }
    }
}