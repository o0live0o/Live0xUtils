using Live0xUtils.RegexUtils;
using System;
using Xunit;
using Xunit.Abstractions;
using Live0xUtils.DbUtils;
using Live0xUtils.DbUtils.Sqlite;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Linq;
using L = Live0xUtils.DbUtils.SqlServer.SqlHelper;
using System.Text.RegularExpressions;
using System.Collections;
using Live0xUtils.QRCodeUtils;
using System.Drawing;
using System.Drawing.Imaging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Live0xUtilsTest
{
    public class UnitTest1
    {

        [Fact]
        public void HasValTest()
        {
            string pattern = @"@@@(\d{1}),";

            var s = "HJ26440601099302019-07-18 13:17:19@@@2019-07-18 13:17:19.193tek1d####";
            var b = RegexText.HasVal(s, pattern);
            Assert.False(b);

            s = "HJ26440601099302019-07-18 14:49:155fb265d5-0a0e-4359-8eb1-ecc187c49502@@@0,未进行连接注册tek74####";
            b = RegexText.HasVal(s, pattern);
            var t = RegexText.MatchVal(s, pattern);
            Assert.True(b);

            s = "HJ26440601099302019-07-18 13:21:42d06b8091-791a-4c05-a87a-4eb5978c7c57@@@1,2019-07-18 13:21:42tek13####";
            b = RegexText.HasVal(s, pattern);
            t = RegexText.MatchVal(s, pattern);

            s = "HJ26440601099302019-07-18 13:17:19@@@2019-07-18 13:17:19.193tek1d####";
            pattern = "@@@(.*)tek";
            t = RegexText.MatchVal(s, pattern, 1);
            Assert.True(b);
        }

        [Fact]
        public void Test1()
        {
            string xml = "<?xml version=\"1.0\" encoding=\"GBK\"?><Request Name=\"CMD_CHECKSELF_LLJ\" Method=\"WriteCalib\"><Result><Row><StartTime>2018-07-25 11:34:55</StartTime><EndTime>2018-07-25 11:35:30</EndTime><CalibName>流量计检查</CalibName><O2>20.50</O2><FLOW>95</FLOW><ALL_PD>1</ALL_PD><JCXH>1</JCXH></Row></Result></Request>";
            var result = RegexXML.MatchProterty(xml, "Request Name");
            Assert.Equal("CMD_CHECKSELF_LLJ", result);
            result = RegexXML.MatchProterty(xml, "Method");
            GenerateSql<Moc>.Query("ID");
        }

        [Fact]
        public void TestMatch()
        {
            var result = Live0xUtils.ConfigUtils.ReadAppConfig.GetStringByTag("User");
            Assert.Equal("CD", result);
        }


        [Fact]
        public void TestXML()
        {
            //L.init("IVS30","sa","123456",".");
            //L.ExecuteNonQuery(,);
            //string xml = File.ReadAllText("ASM.txt",Encoding.Default);
            //Live0xUtils.XMLUtils.XMLHelper.XMLToList<Moc>(xml, "ProcessData");       
            string s = "T200309P90941140028-03";
            List<string> lstJCLSH = new List<string>();

            if (s.Contains("-"))
            {
                string[] strInfos = s.Split('-');
                int iJCCS = Convert.ToInt32(strInfos[1]) + 1;
                for (int i = 1; i < iJCCS; i++)
                {
                    lstJCLSH.Add(strInfos[0] + "-" + i.ToString("d2"));
                }
            }
            else
            {
                lstJCLSH.Add(s);
            }
        }

        [Fact]
        public void SqliteTest()
        {
            SqliteHelper.GetInstance().init(@"D:\MCode\SqliteDB\yzslz.db");

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("JCLSH", "T190420P91014360004-01");
            Moc moc = SqliteHelper.GetInstance().ExcuteEntity<Moc>("SELECT * FROM RESULT_BRAKE WHERE JCLSH = @JCLSH", dic);
            int i = 0;
        }


        [Fact]
        public void GetXMLNodeTest()
        {
            string s = File.ReadAllText("ASM.txt");
            XDocument xDocument = XDocument.Parse(s);
            var t = from info in xDocument.Descendants("Result").Elements("Row").Elements()
                    select info;
            foreach (XElement node in t)
            {
                string uu = string.Join("@", t.Select(p => p.Name));
            }
        }

        [Fact]
        public void ConvertNullable()
        {
            Moc moc = new Moc();
            object ttt = "10";
            foreach (var p in moc.GetType().GetProperties())
            {
                if (p.Name == "ID")
                {
                    if (!p.PropertyType.IsGenericType)
                    {
                        p.SetValue(moc, Convert.ChangeType(ttt, p.PropertyType), null);
                    }
                    else
                    {
                        Type genericTypeDefinition = p.PropertyType.GetGenericTypeDefinition();
                        if (genericTypeDefinition == typeof(Nullable<>))
                        {
                            p.SetValue(moc, Convert.ChangeType(ttt, p.PropertyType.GetGenericArguments()[0]), null);
                        }
                    }
                }
            }
        }

        [Fact]
        public bool RegText()
        {
            //\u4e00   \u9fa5
            //^[A-Za-z0-9]+@[A-Za-z0-9]+.com
            object o = 1.0000M;
            if (o == null)
                return false;
            string s = o.ToString();
            if (string.IsNullOrEmpty(s))
                return false;
            Regex regex = new Regex(@"^(\d{1})[a-zB][A-Z]{2}$");
            bool succ = regex.IsMatch(s);
            Moc moc = new Moc();
            return succ;

        }

        [Fact]
        public void TestWebservice()
        {
            Live0xUtils.WebServiceUtils.WebServiceHelper webServiceHelper = new Live0xUtils.WebServiceUtils.WebServiceHelper();
            Hashtable hashtable = new Hashtable();
            hashtable.Add("arg0", "1");
         string s =    webServiceHelper.SoapMethod("http://webservice.ajxm.anche.com/", "http://192.168.2.233:9080/web", "query", hashtable,false);
            string s1 = webServiceHelper.SoapMethod("http://webservice.ajxm.anche.com/", "http://192.168.2.233:9080/web", "query", hashtable);
            string s12 = webServiceHelper.SoapMethod("www.yzslz.com/", "http://192.168.2.233/Hello.asmx", "HelloWorld", null, false);
            string s13 = webServiceHelper.SoapMethod("www.yzslz.com/", "http://192.168.2.233/Hello.asmx", "HelloWorld", null, true);
            int i = 0;
            //webServiceHelper.EncodeParaToSoap("Add","www.yzslz.com", hashtable);
        }

        [Fact]
        public void TestIDbHelper()
        {
            Bitmap bitmap =  QRCodeHelper.CreateCode("www.baidu.com", 5, 5, true);
            bitmap.Save("Text.png",ImageFormat.Png);
            Live0xUtils.DbUtils.SqlServer.MssqlHelper mssqlHelper = Live0xUtils.DbUtils.SqlServer.MssqlHelper.GetInstance();
            mssqlHelper.Init(".","IVS30","sa","123456   ");
            Hashtable hashtable = new Hashtable();
            hashtable.Add("HPHM", "晋KHH185");
            //Moc moc = mssqlHelper.Query<Moc>("SELECT * FROM LOGIN_VEHICLE_INFO WHERE HPHM = @HPHM", hashtable);
           object o = mssqlHelper.QueryObject("SELECT SYR FROM LOGIN_VEHICLE_INFO WHERE HPHM = @HPHM", hashtable);
            string u = (string)o;
            int j = 0;
           
        }

        [Fact]
        public void TestGetField()
        {
            string s= File.ReadAllText("Text.json");
            JArray o =  (JArray)JsonConvert.DeserializeObject(s);

            StringBuilder str = new StringBuilder();
            foreach (JObject jObject in o)
            {
                foreach (var item in jObject)
                {
                    str.Append(item.Key + ":" + item.Value + "|,|" + "\r\n");
                }
            }
            File.WriteAllText("TTTT.txt", str.ToString()); ;
            int j = 0;

        }

        public class Moc
        {
            public int? ID { get; set; }

            public string HPHM { get; set; }

            public string VIN { get; set; }

            public string SYR { get; set; }
        }
    }
}
