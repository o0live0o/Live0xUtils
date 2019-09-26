using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Live0xUtils.HttpUtils
{
    public class HttpRequest
    {
        public static string FORM_URLENCODE = "application/x-www-form-urlencoded";

        public string HttpPost(string url,string content)
        {
            return Request(url, content, "POST");
        }

        public string HttpGet(string url, string content)
        {
            return Request(url,content,"GET");
        }

        public string Request(string url, string content, string type)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = FORM_URLENCODE;
            httpWebRequest.Method = type;
            byte[] b = System.Text.Encoding.UTF8.GetBytes(content);
            httpWebRequest.ContentLength = b.Length;
            Stream writer;
            try
            {
                writer = httpWebRequest.GetRequestStream();
            }
            catch (Exception)
            {
                throw;
            }

            writer.Write(b, 0, b.Length);
            writer.Close();

            HttpWebResponse httpWebResponse;
            try
            {
                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            }
            catch (WebException we)
            {
                httpWebResponse = we.Response as HttpWebResponse;
            }

            Stream stream = httpWebResponse.GetResponseStream();
            StreamReader streamReader = new StreamReader(stream);

            string s = streamReader.ReadToEnd();
            streamReader.Close();

            return s;
        }

    }
}
