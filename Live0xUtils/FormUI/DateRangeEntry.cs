using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Live0xUtils.FormUI
{
	[TypeConverter(typeof(DateRangeEntryConverter))]
	public class DateRangeEntry : BaseEntry
	{
		private string m_captionMember = string.Empty;

		private string m_checkedMember = string.Empty;

		private string m_beginDateMember = string.Empty;

		private string m_endDateMember = string.Empty;

		private string m_tagMember = string.Empty;

		[Category("Data")]
		[Description("与控件 Caption 属性关联的数据实体的属性名称。")]
		[DefaultValue("")]
		public string CaptionMember
		{
			get
			{
				return m_captionMember;
			}
			set
			{
				m_captionMember = value;
			}
		}

		[DefaultValue("")]
		[Category("Data")]
		[Description("与控件 Checked 属性关联的数据实体的属性名称。")]
		public string CheckedMember
		{
			get
			{
				return m_checkedMember;
			}
			set
			{
				m_checkedMember = value;
			}
		}

		[DefaultValue("")]
		[Category("Data")]
		[Description("与控件 BeginDate 属性关联的数据实体的属性名称。")]
		public string BeginDateMember
		{
			get
			{
				return m_beginDateMember;
			}
			set
			{
				m_beginDateMember = value;
			}
		}

		[Description("与控件 EndDate 属性关联的数据实体的属性名称。")]
		[DefaultValue("")]
		[Category("Data")]
		public string EndDateMember
		{
			get
			{
				return m_endDateMember;
			}
			set
			{
				m_endDateMember = value;
			}
		}

		[DefaultValue("")]
		[Description("与控件 Tag 属性关联的数据实体的属性名称。")]
		[Category("Data")]
		public string TagMember
		{
			get
			{
				return m_tagMember;
			}
			set
			{
				m_tagMember = value;
			}
		}

		public DateRangeEntry()
		{
		}

		public DateRangeEntry(string captionMember, string checkedMember, string beginDateMember, string endDateMember, string tagMember)
		{
			m_captionMember = captionMember;
			m_checkedMember = checkedMember;
			m_beginDateMember = beginDateMember;
			m_endDateMember = endDateMember;
			m_tagMember = tagMember;
		}

		internal override void Clear(Control control)
		{
			DateRange dateRange = control as DateRange;
			if (dateRange != null)
			{
				if (m_captionMember != string.Empty)
				{
					dateRange.Caption = string.Empty;
				}
				if (m_checkedMember != string.Empty)
				{
					dateRange.Checked = false;
				}
				if (m_tagMember != string.Empty)
				{
					dateRange.Tag = null;
				}
			}
		}

		internal override void DisplayEntity(Control control, object entity)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}
			try
			{
				DateRange dateRange = control as DateRange;
				if (dateRange == null)
				{
					return;
				}
				if (m_captionMember != string.Empty)
				{
					try
					{
						dateRange.Caption = Convert.ToString(PropertyHelper.GetPropertyValue(m_captionMember, entity));
					}
					catch
					{
					}
				}
				if (m_checkedMember != string.Empty)
				{
					try
					{
						dateRange.Checked = Convert.ToBoolean(PropertyHelper.GetPropertyValue(m_checkedMember, entity));
					}
					catch
					{
					}
				}
				if (m_beginDateMember != string.Empty)
				{
					try
					{
						dateRange.BeginDate = Convert.ToDateTime(PropertyHelper.GetPropertyValue(m_beginDateMember, entity));
					}
					catch
					{
					}
				}
				if (m_endDateMember != string.Empty)
				{
					try
					{
						dateRange.EndDate = Convert.ToDateTime(PropertyHelper.GetPropertyValue(m_endDateMember, entity));
					}
					catch
					{
					}
				}
				if (m_tagMember != string.Empty)
				{
					try
					{
						dateRange.Tag = PropertyHelper.GetPropertyValue(m_tagMember, entity);
					}
					catch
					{
					}
				}
			}
			catch
			{
				throw;
			}
		}

		internal override void FillEntity(Control control, object entity)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (entity == null)
			{
				throw new ArgumentNullException("entity");
			}
			try
			{
				DateRange dateRange = control as DateRange;
				if (dateRange == null)
				{
					return;
				}
				if (m_captionMember != string.Empty)
				{
					try
					{
						PropertyHelper.SetPropertyValue(m_captionMember, entity, dateRange.Caption);
					}
					catch
					{
					}
				}
				if (m_checkedMember != string.Empty)
				{
					try
					{
						PropertyHelper.SetPropertyValue(m_checkedMember, entity, dateRange.Checked);
					}
					catch
					{
					}
				}
				if (m_beginDateMember != string.Empty)
				{
					try
					{
						PropertyHelper.SetPropertyValue(m_beginDateMember, entity, dateRange.BeginDate);
					}
					catch
					{
					}
				}
				if (m_endDateMember != string.Empty)
				{
					try
					{
						PropertyHelper.SetPropertyValue(m_endDateMember, entity, dateRange.EndDate);
					}
					catch
					{
					}
				}
				if (m_tagMember != string.Empty)
				{
					try
					{
						PropertyHelper.SetPropertyValue(m_tagMember, entity, dateRange.Tag);
					}
					catch
					{
					}
				}
			}
			catch
			{
				throw;
			}
		}
	}
}
