using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Live0xUtils.FormUI
{
	[ToolboxBitmap(typeof(DateRange), "ToolboxBitmap.DateRange.ico")]
	public class DateRange : UserControl
	{
		private CheckBox chkValid;

		private Label lblTo;

		private DateTimePicker dtpBeginDate;

		private DateTimePicker dtpEndDate;

		private Label lblCaption;

		private Container components;

		[Category("Appearance")]
		[Localizable(true)]
		[Description("标题文字。")]
		[Browsable(true)]
		[DefaultValue("条件：")]
		public string Caption
		{
			get
			{
				return lblCaption.Text;
			}
			set
			{
				lblCaption.Text = value;
				Invalidate();
				AdjustPosition();
			}
		}

		[Description("间隔文字。")]
		[Category("Appearance")]
		[DefaultValue("至：")]
		[Browsable(true)]
		[Localizable(true)]
		public string SplitCaption
		{
			get
			{
				return lblTo.Text;
			}
			set
			{
				lblTo.Text = value;
				Invalidate();
				AdjustPosition();
			}
		}

		[DefaultValue(true)]
		[Browsable(true)]
		[Description("是否显示复选框。")]
		[Category("Appearance")]
		public bool ShowCheckBox
		{
			get
			{
				return chkValid.Visible;
			}
			set
			{
				chkValid.Visible = value;
				if (!chkValid.Visible)
				{
					chkValid.Checked = true;
				}
				AdjustPosition();
			}
		}

		[Browsable(true)]
		[Category("Appearance")]
		[DefaultValue(false)]
		[Description("条件是否有效。")]
		public bool Checked
		{
			get
			{
				return chkValid.Checked;
			}
			set
			{
				chkValid.Checked = value;
				dtpBeginDate.Enabled = value;
				dtpEndDate.Enabled = value;
			}
		}

		[DefaultValue(DateTimePickerFormat.Short)]
		[Description("控件显示的日期和时间格式。")]
		[Category("Appearance")]
		[Browsable(true)]
		public DateTimePickerFormat Format
		{
			get
			{
				return dtpBeginDate.Format;
			}
			set
			{
				dtpBeginDate.Format = value;
				dtpEndDate.Format = value;
			}
		}

		[DefaultValue("")]
		[Category("Behavior")]
		[Description("自定义日期/时间格式字符串。")]
		[Browsable(true)]
		public string CustomFormat
		{
			get
			{
				return dtpBeginDate.CustomFormat;
			}
			set
			{
				dtpBeginDate.CustomFormat = value;
				dtpEndDate.CustomFormat = value;
			}
		}

		[Browsable(true)]
		[DefaultValue("2099-12-31 23:59:59.99")]
		[Description("可选择的最大日期和时间。")]
		[Category("Behavior")]
		public DateTime MaxDate
		{
			get
			{
				return dtpBeginDate.MaxDate;
			}
			set
			{
				dtpBeginDate.MaxDate = value;
				dtpEndDate.MaxDate = value;
			}
		}

		[Category("Behavior")]
		[Description("可选择的最小日期和时间。")]
		[Browsable(true)]
		[DefaultValue("1980-1-1")]
		public DateTime MinDate
		{
			get
			{
				return dtpBeginDate.MinDate;
			}
			set
			{
				dtpBeginDate.MinDate = value;
				dtpEndDate.MinDate = value;
			}
		}

		[Description("起始日期和时间。")]
		[Category("Behavior")]
		[Browsable(true)]
		public DateTime BeginDate
		{
			get
			{
				return ConvertBaseFormat(dtpBeginDate.Value);
			}
			set
			{
				dtpBeginDate.Value = ConvertBaseFormat(value);
			}
		}

		[Browsable(true)]
		[Description("截止日期和时间。")]
		[Category("Behavior")]
		public DateTime EndDate
		{
			get
			{
				return ConvertBaseFormat(dtpEndDate.Value);
			}
			set
			{
				dtpEndDate.Value = ConvertBaseFormat(value);
			}
		}

		public event EventHandler ValueChanged;

		public event EventHandler CheckedChanged;

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			chkValid = new System.Windows.Forms.CheckBox();
			dtpBeginDate = new System.Windows.Forms.DateTimePicker();
			lblTo = new System.Windows.Forms.Label();
			dtpEndDate = new System.Windows.Forms.DateTimePicker();
			lblCaption = new System.Windows.Forms.Label();
			SuspendLayout();
			chkValid.BackColor = System.Drawing.Color.Transparent;
			chkValid.Location = new System.Drawing.Point(6, 6);
			chkValid.Name = "chkValid";
			chkValid.Size = new System.Drawing.Size(15, 21);
			chkValid.TabIndex = 0;
			chkValid.UseVisualStyleBackColor = false;
			chkValid.CheckedChanged += new System.EventHandler(chkValid_CheckedChanged);
			dtpBeginDate.Enabled = false;
			dtpBeginDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dtpBeginDate.Location = new System.Drawing.Point(75, 6);
			dtpBeginDate.MaxDate = new System.DateTime(2099, 12, 31, 23, 59, 59, 990);
			dtpBeginDate.MinDate = new System.DateTime(1980, 1, 1, 0, 0, 0, 0);
			dtpBeginDate.Name = "dtpBeginDate";
			dtpBeginDate.Size = new System.Drawing.Size(87, 21);
			dtpBeginDate.TabIndex = 2;
			dtpBeginDate.ValueChanged += new System.EventHandler(dtpBeginDate_ValueChanged);
			lblTo.AutoSize = true;
			lblTo.BackColor = System.Drawing.Color.Transparent;
			lblTo.Location = new System.Drawing.Point(168, 6);
			lblTo.Name = "lblTo";
			lblTo.Size = new System.Drawing.Size(35, 12);
			lblTo.TabIndex = 3;
			lblTo.Text = " 至：";
			lblTo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			lblTo.Click += new System.EventHandler(NormalClick);
			dtpEndDate.Enabled = false;
			dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dtpEndDate.Location = new System.Drawing.Point(219, 6);
			dtpEndDate.MaxDate = new System.DateTime(2099, 12, 31, 23, 59, 59, 990);
			dtpEndDate.MinDate = new System.DateTime(1980, 1, 1, 0, 0, 0, 0);
			dtpEndDate.Name = "dtpEndDate";
			dtpEndDate.Size = new System.Drawing.Size(87, 21);
			dtpEndDate.TabIndex = 4;
			dtpEndDate.ValueChanged += new System.EventHandler(dtpEndDate_ValueChanged);
			lblCaption.AutoSize = true;
			lblCaption.BackColor = System.Drawing.Color.Transparent;
			lblCaption.Location = new System.Drawing.Point(24, 6);
			lblCaption.Name = "lblCaption";
			lblCaption.Size = new System.Drawing.Size(41, 12);
			lblCaption.TabIndex = 1;
			lblCaption.Text = "条件：";
			lblCaption.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			lblCaption.Click += new System.EventHandler(NormalClick);
			BackColor = System.Drawing.Color.Transparent;
			base.Controls.Add(dtpEndDate);
			base.Controls.Add(dtpBeginDate);
			base.Controls.Add(lblTo);
			base.Controls.Add(lblCaption);
			base.Controls.Add(chkValid);
			base.Name = "DateRange";
			base.Size = new System.Drawing.Size(324, 33);
			base.Click += new System.EventHandler(NormalClick);
			ResumeLayout(false);
			PerformLayout();
		}

		public DateRange()
		{
			InitializeComponent();
			SetStyle(ControlStyles.ContainerControl | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, value: true);
			BackColor = Color.Transparent;
			dtpBeginDate.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
			dtpBeginDate.Value = Convert.ToDateTime(DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd"));
		}

		public override string ToString()
		{
			if (chkValid.Checked)
			{
				return $"{lblCaption.Text}{dtpBeginDate.Text}{lblTo.Text}{dtpEndDate.Text}";
			}
			return string.Empty;
		}

		protected override void OnLayout(LayoutEventArgs levent)
		{
			base.OnLayout(levent);
			base.Height = 21;
			AdjustPosition();
		}

		public void AdjustPosition()
		{
			int num = 0;
			chkValid.Location = new Point(0, 0);
			if (chkValid.Visible)
			{
				lblCaption.Location = new Point(chkValid.Bounds.Right, (base.ClientSize.Height - lblCaption.Height) / 2);
				num = (base.ClientSize.Width - chkValid.Size.Width - lblCaption.Size.Width - lblTo.Size.Width + 6) / 2;
			}
			else
			{
				lblCaption.Location = new Point(0, (base.ClientSize.Height - lblCaption.Height) / 2);
				num = (base.ClientSize.Width - lblCaption.Size.Width - lblTo.Size.Width + 6) / 2;
			}
			if (num < 30)
			{
				num = 30;
			}
			dtpBeginDate.SetBounds(lblCaption.Bounds.Right - 3, 0, num, base.ClientSize.Height);
			lblTo.Location = new Point(dtpBeginDate.Bounds.Right, (base.ClientSize.Height - lblTo.Height) / 2);
			dtpEndDate.SetBounds(lblTo.Bounds.Right - 3, 0, num, base.ClientSize.Height);
		}

		private DateTime ConvertBaseFormat(DateTime d)
		{
			switch (dtpBeginDate.Format)
			{
			case DateTimePickerFormat.Long:
				return Convert.ToDateTime(d.ToLongDateString());
			case DateTimePickerFormat.Short:
				return Convert.ToDateTime(d.ToShortDateString());
			case DateTimePickerFormat.Time:
				return Convert.ToDateTime(d.ToLongTimeString());
			case DateTimePickerFormat.Custom:
				return Convert.ToDateTime(d.ToString(dtpBeginDate.CustomFormat));
			default:
				return d;
			}
		}

		private void chkValid_CheckedChanged(object sender, EventArgs e)
		{
			dtpBeginDate.Enabled = chkValid.Checked;
			dtpEndDate.Enabled = chkValid.Checked;
			if (this.CheckedChanged != null)
			{
				this.CheckedChanged(this, e);
			}
		}

		private void dtpBeginDate_ValueChanged(object sender, EventArgs e)
		{
			if (this.ValueChanged != null)
			{
				this.ValueChanged(this, e);
			}
		}

		private void dtpEndDate_ValueChanged(object sender, EventArgs e)
		{
			if (this.ValueChanged != null)
			{
				this.ValueChanged(this, e);
			}
		}

		private void NormalClick(object sender, EventArgs e)
		{
			if (ShowCheckBox)
			{
				chkValid.Checked = !chkValid.Checked;
			}
		}
	}
}
