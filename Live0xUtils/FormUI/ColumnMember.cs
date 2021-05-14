using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Live0xUtils.FormUI
{
	[TypeConverter(typeof(ColumnMemberConverter))]
	public class ColumnMember
	{
		private string m_columnName = string.Empty;

		private string m_member = string.Empty;

		[Description("获取或设置表格列名称。")]
		[DefaultValue("")]
		[Category("Data")]
		public string ColumnName
		{
			get
			{
				return m_columnName;
			}
			set
			{
				m_columnName = value;
			}
		}

		[DefaultValue("")]
		[Description("获取或设置表格列对应填充实体的属性名称。")]
		[Category("Data")]
		public string Member
		{
			get
			{
				return m_member;
			}
			set
			{
				m_member = value;
			}
		}

		public ColumnMember()
		{
		}

		public ColumnMember(string columnName, string member)
		{
			m_columnName = columnName;
			m_member = member;
		}
	}
}
