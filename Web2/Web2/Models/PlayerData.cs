using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Web2.Models
{
    public class PlayerData
    {
        public string Method; // get hoặc set
        public string Username;
        public string Password;
        public string DisplayName;
        public string Khoa;
        public string QueQuan;
        public string DienThoai;
        public string Avatar;
        public PlayerData(string method, string username, string password)
        {
            Method = method;
            Username = username;
            Password = password;
            DisplayName = "";
            Khoa = "";
            QueQuan = "";
            DienThoai = "";
            Avatar = "";
        }
        public string ToJson()
        {
            return new JavaScriptSerializer().Serialize(this);
        }
        public string MessageEmpty(string method)
        {
            Method = method;
            Username = "";
            Password = "";
            DisplayName = "";
            Khoa = "";
            QueQuan = "";
            DienThoai = "";
            Avatar = "";
            return ToJson();
        }
        public string ToSuccess()
        {
            return MessageEmpty("Success");
        }
        public string ToError()
        {
            return MessageEmpty("Error");
        }
    }
}