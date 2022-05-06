using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using Web2.Models;
using Newtonsoft.Json;

namespace Web2.Controllers
{
    public class PlayerDataController : Controller
    {

        #region Login Regester
        // GET: PlayerData
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            PlayerData pd = JsonConvert.DeserializeObject<PlayerData>(collection["data"]);
            if (pd.Method == "Get")
            {
                ViewBag.Content = Login(pd);
            }
            else if (pd.Method == "Set")
            {
                ViewBag.Content = Register(pd);
            }
            else if (pd.Method == "Update")
            {
                ViewBag.Content = Update(pd);
            }
            return View();
        }
        private string Update(PlayerData pd)
        {
            if (!AccoutExist(pd.Username))
            {
                return pd.ToError();
            }
            else
            {
                string jsonPlayer = WorkFile.ReadFileAsText(GetAccountPath(pd.Username));
                PlayerData pdget = JsonConvert.DeserializeObject<PlayerData>(jsonPlayer);
                if (pdget.Password == pd.Password)
                {
                    //không update anh pd.Avatar > 100;
                    if (pd.Avatar.Length < 100)
                    {
                        WorkFile.WriteNewFile(GetAccountPath(pd.Username), pd.ToJson());
                        return pd.ToSuccess();
                    }
                    else
                    {
                        WorkFile.WriteBase64stringToBytes(pd.Avatar, GetAvatarPath(pd.Username));
                        pd.Avatar = GetAvatarPath(pd.Username);
                        WorkFile.WriteNewFile(GetAccountPath(pd.Username), pd.ToJson());
                        return pd.ToSuccess();
                    }
                }
                else
                {
                    return pd.ToError();
                }
            }
        }
        private string Login(PlayerData pd)
        {
            if (!AccoutExist(pd.Username))
            {
                return pd.ToError();
            }
            else
            {
                string path = GetAccountPath(pd.Username);
                string jsonPlayer = WorkFile.ReadFileAsText(path);
                PlayerData pdget = JsonConvert.DeserializeObject<PlayerData>(jsonPlayer);
                if (pdget.Password == pd.Password)
                {
                    return jsonPlayer;
                }
                else
                {
                    return pd.ToError();
                }
            }
        }
        private string Register(PlayerData pd)
        {
            if (AccoutExist(pd.Username))
            {
                return pd.ToError();
            }
            else
            {
                // tao mới
                string path = GetAccountPath(pd.Username);
                pd.Avatar = GetDefaultAvatarPath();
                WorkFile.WriteNewFile(path, pd.ToJson());
                return pd.ToJson();
            }
        }
        private bool AccoutExist(string username)
        {
            string path = GetAccountPath(username);
            if (!System.IO.File.Exists(path))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public string GetAvatarPath(string username)
        {
            return Server.MapPath(PathServer.Accounts + "/avatar/" + username + ".png");
        }
        public string GetDefaultAvatarPath()
        {
            return Server.MapPath(PathServer.Avatar + "avatar.png");
        }
        public string GetAccountPath(string username)
        {
            return Server.MapPath(PathServer.Accounts + "/infor/" + username + ".txt");
        }

        #endregion

        #region Avatar
        [HttpPost]
        public ActionResult Avatar(FormCollection collection)
        {
            PlayerData pd = JsonConvert.DeserializeObject<PlayerData>(collection["data"]);
            if (pd.Method == "Get" || pd.Method == "Set")
            {
                ViewBag.Content = GetAvatar(pd);
            }
            else if (pd.Method == "Update")
            {
                ViewBag.Content = UpdateAvatar(pd);
            }
            return View();
        }

        private string UpdateAvatar(PlayerData pd)
        {
            WorkFile.WriteBase64stringToBytes(pd.Avatar, GetAvatarPath(pd.Username));
            return pd.ToSuccess();
        }

        private string GetAvatar(PlayerData pd)
        {
            pd.Avatar = WorkFile.ReadAllBytesAsBase64(pd.Avatar);
            return pd.ToJson();
        }

        #endregion

        [HttpPost]
        public ActionResult GetAvatar(FormCollection collection)
        {
            string path = collection["data"];
            ViewBag.Content = WorkFile.ReadAllBytesAsBase64(path);
            
            return View();
        }
        public ActionResult Image(string name, string base64String)
        {
            var path = Server.MapPath(PathServer.Avatar + name);
            return base.File(path, "image/jpeg");
        }

    }
}