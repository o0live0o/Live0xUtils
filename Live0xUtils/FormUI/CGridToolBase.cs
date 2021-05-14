using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Live0xUtils.FormUI
{
	internal abstract class CGridToolBase
	{
		protected internal BindingFlags m_bindingAttr;

		protected internal DataGridView m_dgv;

		protected internal ColumnMember[] m_columnMembers;

		protected internal bool[] m_complex;

		protected internal PropertyInfo[] m_infos;

		protected internal Type m_entityType;

		protected internal HybridDictionary m_infosCache = new HybridDictionary(10);

		protected internal CGridToolBase(DataGridView dgv, ColumnMember[] columnMembers, BindingFlags flags)
		{
			if (dgv == null)
			{
				throw new ArgumentNullException("dgv");
			}
			m_dgv = dgv;
			m_columnMembers = columnMembers;
			m_bindingAttr = flags;
			Initialize();
		}

		private void Initialize()
		{
			if (m_columnMembers == null)
			{
				ArrayList arrayList = new ArrayList();
				foreach (DataGridViewColumn column in m_dgv.Columns)
				{
					if (column.DataPropertyName.Trim() != string.Empty)
					{
						ColumnMember value = new ColumnMember(column.Name, column.DataPropertyName.Trim());
						arrayList.Add(value);
					}
				}
				if (arrayList.Count > 0)
				{
					m_columnMembers = (arrayList.ToArray(typeof(ColumnMember)) as ColumnMember[]);
				}
				else
				{
					m_columnMembers = new ColumnMember[0];
				}
			}
			m_complex = new bool[m_columnMembers.Length];
			for (int i = 0; i < m_columnMembers.Length; i++)
			{
				string text = m_columnMembers[i].Member.Trim();
				if (text == "this" || text.IndexOf(".") >= 0)
				{
					m_complex[i] = true;
				}
				else
				{
					m_complex[i] = false;
				}
			}
			m_infos = new PropertyInfo[m_columnMembers.Length];
		}

		private void GetPropertyArray(object entity)
		{
			if (entity == null)
			{
				return;
			}
			if (m_entityType == null)
			{
				m_entityType = entity.GetType();
				for (int i = 0; i < m_columnMembers.Length; i++)
				{
					m_infos[i] = m_entityType.GetProperty(m_columnMembers[i].Member, m_bindingAttr);
				}
			}
			else
			{
				if (m_entityType.Equals(entity))
				{
					return;
				}
				if (!m_infosCache.Contains(m_entityType))
				{
					PropertyInfo[] array = new PropertyInfo[m_infos.Length];
					m_infos.CopyTo(array, 0);
					m_infosCache.Add(m_entityType, array);
				}
				m_entityType = entity.GetType();
				if (m_infosCache.Contains(m_entityType))
				{
					m_infos = (m_infosCache[m_entityType] as PropertyInfo[]);
					return;
				}
				for (int j = 0; j < m_columnMembers.Length; j++)
				{
					m_infos[j] = m_entityType.GetProperty(m_columnMembers[j].Member, m_bindingAttr);
				}
			}
		}

		protected internal virtual void FillRow(int index, object entity)
		{
			GetPropertyArray(entity);
			m_dgv.Rows[index].Tag = entity;
			for (int i = 0; i < m_columnMembers.Length; i++)
			{
				try
				{
					if (m_complex[i])
					{
						m_dgv[m_columnMembers[i].ColumnName, index].Value = PropertyHelper.GetPropertyValue(m_columnMembers[i].Member, entity);
					}
					else if (m_infos[i] != null)
					{
						m_dgv[m_columnMembers[i].ColumnName, index].Value = m_infos[i].GetValue(entity, null);
					}
				}
				catch
				{
				}
			}
		}

		protected internal virtual void FillRowData(int index)
		{
			object tag = m_dgv.Rows[index].Tag;
			if (tag == null)
			{
				return;
			}
			GetPropertyArray(tag);
			for (int i = 0; i < m_columnMembers.Length; i++)
			{
				try
				{
					object value = m_dgv[m_columnMembers[i].ColumnName, index].Value;
					if (m_complex[i])
					{
						PropertyHelper.SetPropertyValue(m_columnMembers[i].Member, tag, value);
					}
					else
					{
						PropertyHelper.SetPropertyValue(m_infos[i], tag, value);
					}
				}
				catch
				{
				}
			}
		}

		protected internal virtual object GetRefillEntity(int index, Type type)
		{
			object obj = null;
			try
			{
				if (m_dgv.Rows[index].Tag != null && type != null && type.Equals(m_dgv.Rows[index].Tag))
				{
					obj = CloneHelper.Clone(m_dgv.Rows[index].Tag);
				}
				else if (type != null)
				{
					obj = Activator.CreateInstance(type);
				}
			}
			catch
			{
			}
			if (obj != null)
			{
				GetPropertyArray(obj);
				for (int i = 0; i < m_columnMembers.Length; i++)
				{
					try
					{
						if (m_complex[i])
						{
							PropertyHelper.SetPropertyValue(m_columnMembers[i].Member, obj, m_dgv[m_columnMembers[i].ColumnName, index].Value);
						}
						else
						{
							PropertyHelper.SetPropertyValue(m_infos[i], obj, m_dgv[m_columnMembers[i].ColumnName, index].Value);
						}
					}
					catch
					{
					}
				}
			}
			return obj;
		}
	}
}
