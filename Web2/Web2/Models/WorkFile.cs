using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Web2.Models
{
    public static class WorkFile
    {
        public static string ReadFileAsText(string path)
        {
            StreamReader reader = new StreamReader(path);
            string a = reader.ReadToEnd();
            reader.Close();
            return a;
        }

        public static void WriteNewFile(string path, string content)
        {
            StreamWriter writer = new StreamWriter(path, false);
            writer.Write(content);
            writer.Close();
        }

        public static string ReadAllBytesAsBase64(string fileName)
        {
            byte[] buffer = null;
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                buffer = new byte[fs.Length];
                fs.Read(buffer, 0, (int)fs.Length);
            }
            return Convert.ToBase64String(buffer);
        }
        public static void WriteBase64stringToBytes(string base64, string fileName)
        {
            byte[] buffer = Convert.FromBase64String(base64);

            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
            {

                fs.Write(buffer, 0, buffer.Length);
            }
        }
        public static bool FIleExist(string path)
        {
            if (!System.IO.File.Exists(path))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}