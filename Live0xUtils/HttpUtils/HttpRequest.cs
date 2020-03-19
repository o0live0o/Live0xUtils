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

        /// <summary>
        /// 资源类型：普通文本
        /// </summary>
        public const string TEXT_PLAIN = "text/plain";

        /// <summary>
        /// 资源类型：JSON字符串
        /// </summary>
        public const string APPLICATION_JSON = "application/json";

        /// <summary>
        /// 资源类型：未知类型(数据流)
        /// </summary>
        public const string APPLICATION_OCTET_STREAM = "application/octet-stream";

        /// <summary>
        /// 资源类型：表单数据(键值对)
        /// </summary>
        public const string WWW_FORM_URLENCODED = "application/x-www-form-urlencoded";

        /// <summary>
        /// 资源类型：表单数据(键值对)。编码方式为 gb2312
        /// </summary>
        public const string WWW_FORM_URLENCODED_GB2312 = "application/x-www-form-urlencoded;charset=gb2312";

        /// <summary>
        /// 资源类型：表单数据(键值对)。编码方式为 utf-8
        /// </summary>
        public const string WWW_FORM_URLENCODED_UTF8 = "application/x-www-form-urlencoded;charset=utf-8";

        /// <summary>
        /// 资源类型：多分部数据
        /// </summary>
        public const string MULTIPART_FORM_DATA = "multipart/form-data";

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
            httpWebRequest.ContentType = WWW_FORM_URLENCODED;
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
