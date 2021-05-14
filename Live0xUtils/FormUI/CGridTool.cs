using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Live0xUtils.FormUI
{
	internal sealed class CGridTool : CGridToolBase
	{
		internal CGridTool(DataGridView fg)
			: this(fg, null)
		{
		}

		internal CGridTool(DataGridView fg, ColumnMember[] columnMembers)
			: base(fg, columnMembers, BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty)
		{
		}

		internal void AddRowRange(IList list)
		{
			if (m_dgv == null || list == null || list.Count == 0)
			{
				return;
			}
			try
			{
				foreach (object item in list)
				{
					if (item != null)
					{
						int index = m_dgv.Rows.Add();
						FillRow(index, item);
						m_dgv.Rows[index].Tag = item;
					}
				}
			}
			catch
			{
				throw;
			}
		}

		internal void AddRowRange<T>(IList<T> list)
		{
			if (m_dgv == null || list == null || list.Count == 0)
			{
				return;
			}
			try
			{
				foreach (T item in list)
				{
					object obj = item;
					if (obj != null)
					{
						int index = m_dgv.Rows.Add();
						FillRow(index, obj);
						m_dgv.Rows[index].Tag = obj;
					}
				}
			}
			catch
			{
				throw;
			}
		}

		internal void FillGrid(IList list)
		{
			if (m_dgv != null)
			{
				m_dgv.Rows.Clear();
			}
			if (m_dgv != null && list != null && list.Count != 0)
			{
				AddRowRange(list);
			}
		}

		internal void FillGrid<T>(IList<T> list)
		{
			if (m_dgv != null)
			{
				m_dgv.Rows.Clear();
			}
			if (m_dgv != null && list != null && list.Count != 0)
			{
				AddRowRange(list);
			}
		}

		internal void FillGridRowOfMember(int index, object entity)
		{
			for (int i = 0; i < m_columnMembers.Length; i++)
			{
				if (m_dgv.Columns.Contains(m_columnMembers[i].ColumnName))
				{
					_ = m_dgv.Columns[m_columnMembers[i].ColumnName].Index;
					m_dgv.Rows[index].Cells.Clear();
				}
			}
			if (entity != null)
			{
				FillRow(index, entity);
			}
		}
	}
}
