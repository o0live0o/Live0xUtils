namespace EncryptionUtils
{
    partial class EncryptionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCreate = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtKeyFile = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSelect = new System.Windows.Forms.Button();
            this.txtStationNo = new System.Windows.Forms.TextBox();
            this.txtComputerNo = new System.Windows.Forms.TextBox();
            this.dtMaxDate = new System.Windows.Forms.DateTimePicker();
            this.btnCreateKey = new System.Windows.Forms.Button();
            this.txtKeyVal = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCreateLog = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "创建密钥对：";
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(80, 49);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(146, 21);
            this.txtFileName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "文件名";
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(264, 49);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(75, 23);
            this.btnCreate.TabIndex = 3;
            this.btnCreate.Text = "创建";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "加密字符串";
            // 
            // txtKeyFile
            // 
            this.txtKeyFile.Location = new System.Drawing.Point(80, 138);
            this.txtKeyFile.Name = "txtKeyFile";
            this.txtKeyFile.Size = new System.Drawing.Size(146, 21);
            this.txtKeyFile.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "私钥路径";
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(246, 138);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 6;
            this.btnSelect.Text = "……";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // txtStationNo
            // 
            this.txtStationNo.Location = new System.Drawing.Point(24, 204);
            this.txtStationNo.Name = "txtStationNo";
            this.txtStationNo.Size = new System.Drawing.Size(180, 21);
            this.txtStationNo.TabIndex = 5;
            // 
            // txtComputerNo
            // 
            this.txtComputerNo.Location = new System.Drawing.Point(217, 204);
            this.txtComputerNo.Name = "txtComputerNo";
            this.txtComputerNo.Size = new System.Drawing.Size(180, 21);
            this.txtComputerNo.TabIndex = 5;
            // 
            // dtMaxDate
            // 
            this.dtMaxDate.Location = new System.Drawing.Point(410, 204);
            this.dtMaxDate.Name = "dtMaxDate";
            this.dtMaxDate.Size = new System.Drawing.Size(123, 21);
            this.dtMaxDate.TabIndex = 7;
            // 
            // btnCreateKey
            // 
            this.btnCreateKey.Location = new System.Drawing.Point(24, 247);
            this.btnCreateKey.Name = "btnCreateKey";
            this.btnCreateKey.Size = new System.Drawing.Size(75, 23);
            this.btnCreateKey.TabIndex = 8;
            this.btnCreateKey.Text = "生成注册码";
            this.btnCreateKey.UseVisualStyleBackColor = true;
            this.btnCreateKey.Click += new System.EventHandler(this.btnCreateKey_Click);
            // 
            // txtKeyVal
            // 
            this.txtKeyVal.Location = new System.Drawing.Point(24, 286);
            this.txtKeyVal.Name = "txtKeyVal";
            this.txtKeyVal.Size = new System.Drawing.Size(562, 96);
            this.txtKeyVal.TabIndex = 9;
            this.txtKeyVal.Text = "";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 177);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "检测站编号";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(215, 177);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 2;
            this.label6.Text = "机器码";
            // 
            // txtCreateLog
            // 
            this.txtCreateLog.BackColor = System.Drawing.Color.White;
            this.txtCreateLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCreateLog.Location = new System.Drawing.Point(360, 49);
            this.txtCreateLog.Name = "txtCreateLog";
            this.txtCreateLog.ReadOnly = true;
            this.txtCreateLog.Size = new System.Drawing.Size(250, 112);
            this.txtCreateLog.TabIndex = 10;
            this.txtCreateLog.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(114, 247);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "打开文件位置";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(458, 247);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 11;
            this.button2.Text = "测试";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(408, 177);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "到期时间";
            // 
            // EncryptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(622, 413);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtCreateLog);
            this.Controls.Add(this.txtKeyVal);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnCreateKey);
            this.Controls.Add(this.dtMaxDate);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.txtComputerNo);
            this.Controls.Add(this.txtStationNo);
            this.Controls.Add(this.txtKeyFile);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.label1);
            this.Name = "EncryptionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtKeyFile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.TextBox txtStationNo;
        private System.Windows.Forms.TextBox txtComputerNo;
        private System.Windows.Forms.DateTimePicker dtMaxDate;
        private System.Windows.Forms.Button btnCreateKey;
        private System.Windows.Forms.RichTextBox txtKeyVal;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RichTextBox txtCreateLog;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label7;
    }
}