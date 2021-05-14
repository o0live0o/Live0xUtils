using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Live0xUtils.FormUI
{
    public partial class EntryForm : Form
    {


		private Button btnOK;

		private Button btnCancel;

		private Label lblCaption;

		private PropertyGrid propertyGrid1;

		private Container components;

		public BaseEntry Entry => (BaseEntry)propertyGrid1.SelectedObject;
		public EntryForm(BaseEntry entry)
        {
            InitializeComponent();
			propertyGrid1.SelectedObject = entry;
			if (entry != null)
			{
				lblCaption.Text = entry.GetType().Name + " 属性：";
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			Close();
		}
		private void InitializeComponent()
		{
			btnOK = new System.Windows.Forms.Button();
			btnCancel = new System.Windows.Forms.Button();
			lblCaption = new System.Windows.Forms.Label();
			propertyGrid1 = new System.Windows.Forms.PropertyGrid();
			SuspendLayout();
			btnOK.Location = new System.Drawing.Point(96, 330);
			btnOK.Name = "btnOK";
			btnOK.Size = new System.Drawing.Size(72, 24);
			btnOK.TabIndex = 0;
			btnOK.Text = "确认";
			btnOK.Click += new System.EventHandler(btnOK_Click);
			btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			btnCancel.Location = new System.Drawing.Point(180, 330);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new System.Drawing.Size(72, 24);
			btnCancel.TabIndex = 1;
			btnCancel.Text = "取消";
			lblCaption.Location = new System.Drawing.Point(12, 6);
			lblCaption.Name = "lblCaption";
			lblCaption.Size = new System.Drawing.Size(240, 15);
			lblCaption.TabIndex = 3;
			lblCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			propertyGrid1.CommandsVisibleIfAvailable = true;
			propertyGrid1.LargeButtons = false;
			propertyGrid1.LineColor = System.Drawing.SystemColors.ScrollBar;
			propertyGrid1.Location = new System.Drawing.Point(12, 24);
			propertyGrid1.Name = "propertyGrid1";
			propertyGrid1.Size = new System.Drawing.Size(240, 297);
			propertyGrid1.TabIndex = 4;
			propertyGrid1.Text = "propertyGrid1";
			propertyGrid1.ToolbarVisible = false;
			propertyGrid1.ViewBackColor = System.Drawing.SystemColors.Window;
			propertyGrid1.ViewForeColor = System.Drawing.SystemColors.WindowText;
			base.AcceptButton = btnOK;
			AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			base.CancelButton = btnCancel;
			base.ClientSize = new System.Drawing.Size(267, 371);
			base.Controls.Add(propertyGrid1);
			base.Controls.Add(lblCaption);
			base.Controls.Add(btnCancel);
			base.Controls.Add(btnOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "EntryForm";
			base.ShowInTaskbar = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			Text = "填充属性";
			ResumeLayout(false);
		}
	}
}
