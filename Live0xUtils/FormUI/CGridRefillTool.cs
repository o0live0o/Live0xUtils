using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Live0xUtils.FormUI
{
	internal sealed class CGridRefillTool : CGridToolBase
	{
		internal CGridRefillTool(DataGridView dgv)
			: this(dgv, null)
		{
		}

		internal CGridRefillTool(DataGridView dgv, ColumnMember[] columnMembers)
			: base(dgv, columnMembers, BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty)
		{
		}

		internal IList GetRefilledRowDataList()
		{
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < m_dgv.Rows.Count; i++)
			{
				FillRowData(i);
				if (m_dgv.Rows[i].Tag != null)
				{
					arrayList.Add(m_dgv.Rows[i].Tag);
				}
			}
			return arrayList.ToArray();
		}

		internal IList GetChangedRowDataList(string columnName, bool clearChangedMark)
		{
			ArrayList arrayList = new ArrayList();
			if (columnName != null && m_dgv.Columns.Contains(columnName))
			{
				for (int i = 0; i < m_dgv.Rows.Count; i++)
				{
					if (m_dgv[columnName, i].Value is bool && (bool)m_dgv[columnName, i].Value)
					{
						FillRowData(i);
						if (m_dgv.Rows[i].Tag != null)
						{
							arrayList.Add(m_dgv.Rows[i].Tag);
						}
						if (clearChangedMark)
						{
							m_dgv[columnName, i].Value = false;
						}
					}
				}
			}
			return arrayList.ToArray();
		}

		internal IList GetRefilledRowDataList(Type type)
		{
			ArrayList arrayList = new ArrayList();
			if (type != null)
			{
				for (int i = 0; i < m_dgv.Rows.Count; i++)
				{
					if (!type.Equals(m_dgv.Rows[i].Tag))
					{
						bool flag = true;
						ColumnMember[] columnMembers = m_columnMembers;
						foreach (ColumnMember columnMember in columnMembers)
						{
							object value = m_dgv[columnMember.ColumnName, i].Value;
							if (value != null && value.ToString() != string.Empty)
							{
								flag = false;
								break;
							}
						}
						if (!flag)
						{
							m_dgv.Rows[i].Tag = Activator.CreateInstance(type);
						}
					}
					FillRowData(i);
					if (m_dgv.Rows[i].Tag != null)
					{
						arrayList.Add(m_dgv.Rows[i].Tag);
					}
				}
			}
			return arrayList.ToArray();
		}

		internal T GetRefilledEntity<T>() where T : class
		{
			if (m_dgv.CurrentRow != null && m_dgv.CurrentRow.Tag is T)
			{
				return (T)m_dgv.CurrentRow.Tag;
			}
			return null;
		}

		internal IList<T> GetRefilledRowDataList<T>() where T : class
		{
			List<T> list = new List<T>();
			for (int i = 0; i < m_dgv.Rows.Count; i++)
			{
				FillRowData(i);
				if (m_dgv.Rows[i].Tag is T)
				{
					list.Add((T)m_dgv.Rows[i].Tag);
				}
			}
			return list.ToArray();
		}

		internal IList<T> GetChangedRowDataList<T>(string columnName, bool clearChangedMark)
		{
			List<T> list = new List<T>();
			if (columnName != null && m_dgv.Columns.Contains(columnName))
			{
				for (int i = 0; i < m_dgv.Rows.Count; i++)
				{
					if (m_dgv[columnName, i].Value is bool && (bool)m_dgv[columnName, i].Value)
					{
						FillRowData(i);
						if (m_dgv.Rows[i].Tag is T)
						{
							list.Add((T)m_dgv.Rows[i].Tag);
						}
						if (clearChangedMark)
						{
							m_dgv[columnName, i].Value = false;
						}
					}
				}
			}
			return list.ToArray();
		}
	}
}
