using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EncryptionUtils
{
    public class EncryptionHelper
    {
        public string StationNo = "";
        public string MaxDate = "";
        public string ComputerNo = "";
        public DateTime? EndDate = null;

        public bool Validate(string publicKey, string val, bool valifyDate, out string msg)
        {
            msg = "注册码不正确";
            bool succ = false;
            bool validateComputerNo = false;
            bool validateDate = !valifyDate;
            try
            {
                RSAHelper rsaHelper = new RSAHelper();
                string ming = rsaHelper.DecryptByPublicKey(val, publicKey);
                string[] vals = ming.Split('-');
                if (vals.Length == 3)
                {
                    StationNo = vals[0];
                    ComputerNo = vals[1];
                    MaxDate = vals[2];

                    SoftReg softReg = new SoftReg();
                    string valifyNo = softReg.GetMNum();
                    validateComputerNo = valifyNo.Equals(ComputerNo);
                    if (!validateComputerNo)
                    {
                        msg = "机器码不匹配";
                    }

                    if (valifyDate)
                    {
                        DateTime d;
                        if (DateTime.TryParse(MaxDate, out d))
                        {
                            validateDate = d < DateTime.Now;
                            EndDate = d;
                            if (validateDate)
                            {
                                msg = "注册码已过期";
                            }
                        }
                    }
                    if (!valifyDate) succ = validateComputerNo;
                    else succ = validateComputerNo && !validateDate;
                }
            }
            catch (Exception ex)
            {
                succ = false;
            }
            return succ;
        }

        public bool ValidateWithReg(string publicKey, bool valifyDate, out string msg)
        {
            string keyFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Key");
            bool succ = false;
            msg = "";
            string regVal = "";
            if (File.Exists(keyFilePath)) regVal = File.ReadAllText(keyFilePath);

            if (Validate(publicKey, regVal, valifyDate, out msg))
            {
                succ = true;
            }
            else
            {
                MessageBox.Show(msg);
                RegForm regForm = new RegForm(publicKey, this);
                DialogResult dialogResult = regForm.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    succ = true;
                    File.WriteAllText(keyFilePath, regForm.GetRegNo());
                }
            }
            return succ;
        }

    }
}
