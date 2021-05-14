using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Live0xUtils.FormUI
{
	[TypeConverter(typeof(CheckBoxEntryConverter))]
	public class CheckBoxEntry : BaseEntry
	{
		private string m_checkedMember = string.Empty;

		[DefaultValue("")]
		[Description("获取或设置控件的 Checked 属性对应填充实体的属性名称。")]
		[Category("Data")]
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

		public CheckBoxEntry()
		{
		}

		public CheckBoxEntry(string checkedMember)
		{
			m_checkedMember = checkedMember;
		}

		internal override void Clear(Control control)
		{
			if (control is CheckBox)
			{
				((CheckBox)control).Checked = false;
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
				object propertyValue = PropertyHelper.GetPropertyValue(m_checkedMember, entity);
				if (propertyValue == null)
				{
					return;
				}
				try
				{
					if (control is CheckBox)
					{
						((CheckBox)control).Checked = Convert.ToBoolean(propertyValue);
					}
				}
				catch
				{
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
				if (control is CheckBox)
				{
					PropertyHelper.SetPropertyValue(m_checkedMember, entity, ((CheckBox)control).Checked);
				}
			}
			catch
			{
				throw;
			}
		}
	}
}
