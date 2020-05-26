using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EncryptionUtils
{
    public partial class RegForm : Form
    {
        private string _publicKey = "";

        private string _regNo = "";

        private EncryptionHelper _encryptionHelper = null;

        public RegForm(string publicKey, EncryptionHelper encryptionHelper)
        {
            InitializeComponent();
            this._publicKey = publicKey;
            this._encryptionHelper = encryptionHelper;
            Init();
        }

        private void Init()
        {
            try
            {
                SoftReg softReg = new SoftReg();
                txtComputerNo.Text = softReg.GetMNum();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public string GetRegNo() => _regNo;

        public string GetStationNo() => _regNo;


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void uC_Button1_BtnClick(object sender, EventArgs e)
        {
            try
            {
                bool succ = _encryptionHelper.Validate(_publicKey, txtRegNo.Text.Trim(), true, out string msg);

                if (succ)
                {
                    _regNo = txtRegNo.Text.Trim();
                    this.DialogResult = DialogResult.OK;
                    MessageBox.Show("注册成功");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("注册失败: " + msg);

                }
            }
            catch (Exception ex)
            {
                this.DialogResult = DialogResult.Cancel;
                MessageBox.Show(ex.Message);
            }
        }
    }
}
