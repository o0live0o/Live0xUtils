namespace EncryptionUtils
{
    partial class RegForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegForm));
            this.txtComputerNo = new System.Windows.Forms.RichTextBox();
            this.txtRegNo = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Panel();
            this.uC_Button1 = new EncryptionUtils.UC_Button();
            this.SuspendLayout();
            // 
            // txtComputerNo
            // 
            this.txtComputerNo.BackColor = System.Drawing.Color.White;
            this.txtComputerNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtComputerNo.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtComputerNo.Location = new System.Drawing.Point(21, 72);
            this.txtComputerNo.Name = "txtComputerNo";
            this.txtComputerNo.ReadOnly = true;
            this.txtComputerNo.Size = new System.Drawing.Size(475, 63);
            this.txtComputerNo.TabIndex = 0;
            this.txtComputerNo.Text = "";
            // 
            // txtRegNo
            // 
            this.txtRegNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRegNo.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtRegNo.Location = new System.Drawing.Point(21, 164);
            this.txtRegNo.Name = "txtRegNo";
            this.txtRegNo.Size = new System.Drawing.Size(475, 71);
            this.txtRegNo.TabIndex = 0;
            this.txtRegNo.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(21, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "机器码";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label2.Location = new System.Drawing.Point(21, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "注册码";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BackgroundImage = global::EncryptionUtils.Properties.Resources.cancel_128px_1205790_easyicon_net;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClose.Location = new System.Drawing.Point(473, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(40, 40);
            this.btnClose.TabIndex = 3;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // uC_Button1
            // 
            this.uC_Button1.BtnText = "注 册";
            this.uC_Button1.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uC_Button1.Location = new System.Drawing.Point(366, 241);
            this.uC_Button1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.uC_Button1.Name = "uC_Button1";
            this.uC_Button1.Size = new System.Drawing.Size(130, 42);
            this.uC_Button1.TabIndex = 4;
            this.uC_Button1.BtnClick += new System.EventHandler(this.uC_Button1_BtnClick);
            // 
            // RegForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::EncryptionUtils.Properties.Resources.预览图_千图网_编号26441812;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(514, 289);
            this.Controls.Add(this.uC_Button1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtRegNo);
            this.Controls.Add(this.txtComputerNo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RegForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtComputerNo;
        private System.Windows.Forms.RichTextBox txtRegNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel btnClose;
        private UC_Button uC_Button1;
    }
}