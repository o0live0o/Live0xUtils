using System;
using System.Collections;
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

        public string HttpGet(string url, string content)
        {
            string retString = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + (content == "" ? "" : "?") + content);
                request.Method = "GET";
                request.ContentType = "text/html;charset=UTF-8";

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (Stream myResponseStream = response.GetResponseStream())
                {
                    using (StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8))
                    {
                        retString = myStreamReader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                retString = ex.Message;
            }
            return retString;
        }

        public string HttpGet(string url, Hashtable hashtable)
        {
            List<string> list = new List<string>();
            foreach (DictionaryEntry de in hashtable)
            {
                list.Add($"{de.Key}={de.Value}");
            }
            return HttpGet(url,string.Join("&",list.ToArray()));
        }

        public string HttpPost(string url, string content)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = FORM_URLENCODE;
            httpWebRequest.Method = "POST";
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
                string encodings = httpWebResponse.ContentEncoding;
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


        public string HttpPut(string url, string content)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = FORM_URLENCODE;
            httpWebRequest.Method = "PUT";
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
                string encodings = httpWebResponse.ContentEncoding;
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
