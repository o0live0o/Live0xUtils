using Live0xUtils.FormUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            TextC c1 = new TextC();
            c1.DREAMTEXT = "12345678";
            c1.HELLOTEXT = 888;
            entitytFiller1.DisplayEntity(c1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
             TextC c = new TextC();
            entitytFiller1.FillEntity(c);
            entitytFiller1.DisplayEntity(new TextC());
        }
    }

    public class TextC
    {
        public string HELLO { get; set; }
        public int HELLOTEXT { get; set; }
        public string DREAM { get; set; }
        public string DREAMTEXT { get; set; }
    }
}
