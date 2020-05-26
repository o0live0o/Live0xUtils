using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EncryptionUtils
{
    public partial class EncryptionForm : Form
    {
        public EncryptionForm()
        {
            InitializeComponent();
        }

        private void btnCreateKey_Click(object sender, EventArgs e)
        {
            string stationNo = txtStationNo.Text;
            string computerNo = txtComputerNo.Text;
            string maxDate = dtMaxDate.Value.ToString("yyyy/MM/dd");

            string ming = $"{stationNo}-{computerNo}-{maxDate}";

            if(string.IsNullOrEmpty(computerNo) || string.IsNullOrEmpty(stationNo))
            {
                MessageBox.Show("请完善注册信息!");
                return;
            }

            string keyPath = txtKeyFile.Text.Trim();
            if (!File.Exists(keyPath))
            {
                MessageBox.Show("选择私钥路径");
                return;
            }

            string privateKey = File.ReadAllText(keyPath);
            RSAHelper rsaHelper = new RSAHelper();
            string val = rsaHelper.EncryptByPrivateKey(ming, privateKey);
            txtKeyVal.Text = val;

            File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Keys", computerNo + ".txt"), val);
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {

            string fileName = txtFileName.Text.Trim();
            if (string.IsNullOrEmpty(fileName))
            {
                MessageBox.Show("请输入钥匙名称!");
                return;
            }

            string directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Keys", fileName);
            if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);

            string publicKeyPath = Path.Combine(directoryPath, "PublicKey");
            string privateKeyPath = Path.Combine(directoryPath, "PrivateKey");

            RSAHelper rsaHelper = new RSAHelper();
            var keys = rsaHelper.GetKey();
            txtCreateLog.Clear();
            File.WriteAllText(publicKeyPath, keys.PublicKey);
            txtCreateLog.AppendText("公钥路径\r\n");
            txtCreateLog.AppendText(publicKeyPath + "\r\n");

            File.WriteAllText(privateKeyPath, keys.PrivateKey);
            txtCreateLog.AppendText("私钥路径\r\n");
            txtCreateLog.AppendText(privateKeyPath + "\r\n");
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.InitialDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Keys");
            if (fd.ShowDialog() == DialogResult.OK)
            {
                txtKeyFile.Text = fd.FileName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Keys"));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string path = @"D:\MCode\C\Live0xUtils\EncryptionUtils\bin\Debug\Keys\IYASAKA\PublicKey";
            EncryptionHelper encryptionHelper = new EncryptionHelper();
            bool succ = encryptionHelper.ValidateWithReg(File.ReadAllText(path),false,out string msg);
            string stationNo = encryptionHelper.StationNo;
        }
    }
}
