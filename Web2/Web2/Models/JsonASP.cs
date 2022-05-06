using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web2.Models
{
    public class JsonASP
    {
        public string Name;
        public string Message;
        public string Result;

        public JsonASP(string name, string message, string result)
        {
            Name = name;
            Message = message;
            Result = result;
        }
        public JsonASP()
        {
            Name = "";
            Message = "";
            Result = "";
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
        public string MessageEmpty(string result)
        {
            Message = "";
            Result = result;
            return ToJson();
        }
        public string ToSuccess()
        {
            return MessageEmpty("Success");
        }
        public string ToSuccessWithMessage()
        {
            Result = "Success";
            return ToJson();
        }
        public string ToError()
        {
            return MessageEmpty("Error");
        }
    }
}