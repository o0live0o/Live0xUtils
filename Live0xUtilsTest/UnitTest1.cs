using Live0xUtils.RegexUtils;
using System;
using Xunit;
using Xunit.Abstractions;
using Live0xUtils.DbUtils;
using Live0xUtils.DbUtils.Sqlite;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
            t = RegexText.MatchVal(s, pattern,1);
            Assert.True(b);
        }

        [Fact]
        public void Test1()
        {
            string xml = "<?xml version=\"1.0\" encoding=\"GBK\"?><Request Name=\"CMD_CHECKSELF_LLJ\" Method=\"WriteCalib\"><Result><Row><StartTime>2018-07-25 11:34:55</StartTime><EndTime>2018-07-25 11:35:30</EndTime><CalibName>流量计检查</CalibName><O2>20.50</O2><FLOW>95</FLOW><ALL_PD>1</ALL_PD><JCXH>1</JCXH></Row></Result></Request>";
            var result = RegexXML.MatchProterty(xml, "Request Name");
            Assert.Equal("CMD_CHECKSELF_LLJ",result);
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
            string xml = File.ReadAllText("ASM.txt",Encoding.Default);
            Live0xUtils.XMLUtils.XMLHelper.XMLToList<Moc>(xml, "ProcessData");
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

        }



        public class Moc
        {
            public int? ID { get; set; }

            public string HPHM { get; set; }

            public string HPZL { get; set; }

            public string YZZLZ { get; set; }

            public string JCLSH { get; set; }
        }
    }
}
