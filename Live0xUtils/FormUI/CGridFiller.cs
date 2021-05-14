using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Live0xUtils.FormUI
{
    [ProvideProperty("Columns", typeof(Control))]
    [ToolboxBitmap(typeof(CGridFiller))]
    public class CGridFiller : Component, IExtenderProvider
    {
		private HybridDictionary m_fillEntrys;

		private HybridDictionary m_watchEntrys;

		private HybridDictionary m_watchChanges;

        public CGridFiller()
        {
			m_fillEntrys = new HybridDictionary(9);
			m_watchEntrys = new HybridDictionary(9);
			m_watchChanges = new HybridDictionary(9);
		}
		public bool CanExtend(object extendee)
        {
            return extendee is DataGridView;
        }
		[Editor(typeof(GridColumnMemberCollectionEditor), typeof(UITypeEditor))]
		[Category("填充")]
		[Description("获取DataGridView表格控件的填充设置。")]
		[DefaultValue(null)]
		[ExtenderProvidedProperty]
		public ColumnMember[] GetColumns(Control control)
		{
			return m_fillEntrys[control] as ColumnMember[];
		}

		[ExtenderProvidedProperty]
		[Description("设置DataGridView表格控件的填充设置。")]
		[Editor(typeof(GridColumnMemberCollectionEditor), typeof(UITypeEditor))]
		public void SetColumns(Control control, ColumnMember[] value)
		{
			m_fillEntrys[control] = value;
		}

		public void AddGridRow<T>(DataGridView dgv, T entity) where T : class
		{
			if (dgv != null)
			{
				try
				{
					int index = dgv.Rows.Add();
					FillGridRow(dgv, index, entity);
				}
				catch
				{
					throw;
				}
			}
		}

		public void AddRowRange(DataGridView dgv, IList list)
		{
			if (dgv != null && list != null)
			{
				try
				{
					ColumnMember[] columnMembers = m_fillEntrys[dgv] as ColumnMember[];
					CGridTool cGridTool = new CGridTool(dgv, columnMembers);
					cGridTool.AddRowRange(list);
				}
				catch
				{
					throw;
				}
			}
		}

		public void AddRowRange<T>(DataGridView dgv, IList<T> list) where T : class
		{
			if (dgv != null && list != null)
			{
				try
				{
					ColumnMember[] columnMembers = m_fillEntrys[dgv] as ColumnMember[];
					CGridTool cGridTool = new CGridTool(dgv, columnMembers);
					cGridTool.AddRowRange(list);
				}
				catch
				{
					throw;
				}
			}
		}

		public void AddRowRange<T>(DataGridView dgv, T[] list) where T : class
		{
			try
			{
				AddRowRange(dgv, (IList<T>)list);
			}
			catch
			{
				throw;
			}
		}

		public void FillGrid(DataGridView dgv, IList list)
		{
			if (dgv != null)
			{
				try
				{
					ColumnMember[] columnMembers = m_fillEntrys[dgv] as ColumnMember[];
					CGridTool cGridTool = new CGridTool(dgv, columnMembers);
					cGridTool.FillGrid(list);
				}
				catch
				{
					throw;
				}
			}
		}

		public void FillGrid<T>(DataGridView dgv, IList<T> list) where T : class
		{
			if (dgv != null)
			{
				try
				{
					ColumnMember[] columnMembers = m_fillEntrys[dgv] as ColumnMember[];
					CGridTool cGridTool = new CGridTool(dgv, columnMembers);
					cGridTool.FillGrid(list);
				}
				catch
				{
					throw;
				}
			}
		}

		public void FillGrid<T>(DataGridView dgv, T[] list) where T : class
		{
			try
			{
				FillGrid(dgv, (IList<T>)list);
			}
			catch
			{
				throw;
			}
		}

		public void FillGridRow<T>(DataGridView dgv, T entity) where T : class
		{
			if (dgv != null)
			{
				try
				{
					int index = dgv.CurrentRow.Index;
					FillGridRow(dgv, index, entity);
				}
				catch
				{
					throw;
				}
			}
		}

		public void FillGridRow<T>(DataGridView dgv, int index, T entity) where T : class
		{
			try
			{
				ColumnMember[] columnMembers = m_fillEntrys[dgv] as ColumnMember[];
				CGridTool cGridTool = new CGridTool(dgv, columnMembers);
				cGridTool.FillRow(index, entity);
			}
			catch
			{
				throw;
			}
		}

		public void FillPartGridRow<T>(DataGridView dgv, T entity) where T : class
		{
			if (dgv != null)
			{
				FillPartGridRow(dgv, dgv.CurrentRow.Index, entity);
			}
		}

		public void FillPartGridRow<T>(DataGridView dgv, int index, T entity) where T : class
		{
			try
			{
				PropertyInfo[] properties = entity.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);
				if (properties == null || properties.Length <= 0)
				{
					return;
				}
				ArrayList arrayList = new ArrayList();
				ColumnMember[] array = m_fillEntrys[dgv] as ColumnMember[];
				if (array == null)
				{
					PropertyInfo[] array2 = properties;
					foreach (PropertyInfo propertyInfo in array2)
					{
						if (dgv.Columns.Contains(propertyInfo.Name))
						{
							arrayList.Add(new ColumnMember(propertyInfo.Name, propertyInfo.Name));
						}
					}
				}
				else
				{
					HybridDictionary hybridDictionary = new HybridDictionary(10);
					PropertyInfo[] array3 = properties;
					foreach (PropertyInfo propertyInfo2 in array3)
					{
						if (!hybridDictionary.Contains(propertyInfo2.Name))
						{
							hybridDictionary.Add(propertyInfo2.Name, null);
						}
					}
					ColumnMember[] array4 = array;
					foreach (ColumnMember columnMember in array4)
					{
						if (hybridDictionary.Contains(columnMember.Member))
						{
							arrayList.Add(columnMember);
						}
					}
				}
				if (arrayList.Count > 0)
				{
					CGridTool cGridTool = new CGridTool(dgv, (ColumnMember[])arrayList.ToArray(typeof(ColumnMember)));
					cGridTool.FillGridRowOfMember(index, entity);
				}
			}
			catch
			{
				throw;
			}
		}

		public IList GetRefilledEntityList(DataGridView dgv)
		{
			ColumnMember[] columnMembers = m_fillEntrys[dgv] as ColumnMember[];
			CGridRefillTool cGridRefillTool = new CGridRefillTool(dgv, columnMembers);
			return cGridRefillTool.GetRefilledRowDataList();
		}

		public IList<T> GetRefilledEntityList<T>(DataGridView dgv) where T : class, new()
		{
			ColumnMember[] columnMembers = m_fillEntrys[dgv] as ColumnMember[];
			CGridRefillTool cGridRefillTool = new CGridRefillTool(dgv, columnMembers);
			return cGridRefillTool.GetRefilledRowDataList<T>();
		}

		public T GetRefilledEntity<T>(DataGridView dgv) where T : class, new()
		{
			ColumnMember[] columnMembers = m_fillEntrys[dgv] as ColumnMember[];
			CGridRefillTool cGridRefillTool = new CGridRefillTool(dgv, columnMembers);
			return cGridRefillTool.GetRefilledEntity<T>();
		}

		public IList GetChangedEntityList(DataGridView dgv)
		{
			return GetChangedEntityList(dgv, clearChangedMark: true);
		}

		public IList<T> GetChangedEntityList<T>(DataGridView dgv) where T : class
		{
			return GetChangedEntityList<T>(dgv, clearChangedMark: true);
		}

		public IList GetChangedEntityList(DataGridView dgv, bool clearChangedMark)
		{
			string text = m_watchEntrys[dgv] as string;
			if (text != null)
			{
				ColumnMember[] columnMembers = m_fillEntrys[dgv] as ColumnMember[];
				CGridRefillTool cGridRefillTool = new CGridRefillTool(dgv, columnMembers);
				try
				{
					dgv.CellValueChanged -= DataGridView_CellValueChanged;
					if (clearChangedMark)
					{
						m_watchChanges.Remove(dgv);
					}
					return cGridRefillTool.GetChangedRowDataList(text, clearChangedMark);
				}
				catch
				{
					throw;
				}
				finally
				{
					if (dgv.Columns.Contains(text))
					{
						dgv.CellValueChanged += Dgv_CellValueChanged;
					}
				}
			}
			return new ArrayList();
		}

		private void Dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			throw new NotImplementedException();
		}

		public IList<T> GetChangedEntityList<T>(DataGridView dgv, bool clearChangedMark) where T : class
		{
			string text = m_watchEntrys[dgv] as string;
			if (text != null)
			{
				ColumnMember[] columnMembers = m_fillEntrys[dgv] as ColumnMember[];
				CGridRefillTool cGridRefillTool = new CGridRefillTool(dgv, columnMembers);
				try
				{
					dgv.CellValueChanged -= DataGridView_CellValueChanged;
					if (clearChangedMark)
					{
						m_watchChanges.Remove(dgv);
					}
					return cGridRefillTool.GetChangedRowDataList<T>(text, clearChangedMark);
				}
				catch
				{
					throw;
				}
				finally
				{
					if (dgv.Columns.Contains(text))
					{
						dgv.CellValueChanged += DataGridView_CellValueChanged;
					}
				}
			}
			return new T[0];
		}

		public bool CheckChanged(DataGridView dgv)
		{
			if (m_watchChanges.Contains(dgv))
			{
				return true;
			}
			return false;
		}

		public void StartChangedWatcher(DataGridView dgv)
		{
			if (!m_watchEntrys.Contains(dgv))
			{
				m_watchEntrys.Add(dgv, Guid.NewGuid().ToString());
			}
			m_watchChanges.Remove(dgv);
			string text = m_watchEntrys[dgv] as string;
			if (text == null)
			{
				return;
			}
			if (!dgv.Columns.Contains(text))
			{
				try
				{
					int index = dgv.Columns.Add(text, text);
					dgv.Columns[index].Visible = false;
					dgv.Columns[index].ValueType = typeof(bool);
				}
				catch
				{
				}
			}
			if (dgv.Columns.Contains(text))
			{
				dgv.CellValueChanged -= DataGridView_CellValueChanged;
				foreach (DataGridViewRow item in (IEnumerable)dgv.Rows)
				{
					item.Cells[dgv.Columns[text].Index].Value = false;
				}
			}
			dgv.CellValueChanged += DataGridView_CellValueChanged;
		}

		public void StopChangedWatcher(DataGridView dgv)
		{
			string text = m_watchEntrys[dgv] as string;
			if (text != null && dgv.Columns.Contains(text))
			{
				dgv.CellValueChanged -= DataGridView_CellValueChanged;
				dgv.Columns.Remove(text);
			}
			m_watchEntrys.Remove(dgv);
			m_watchChanges.Remove(dgv);
		}

		private void DataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			DataGridView dataGridView = sender as DataGridView;
			string text = m_watchEntrys[dataGridView] as string;
			if (text != null && dataGridView.Columns.Contains(text))
			{
				dataGridView[text, e.RowIndex].Value = true;
				if (!m_watchChanges.Contains(dataGridView))
				{
					m_watchChanges.Add(dataGridView, true);
				}
			}
		}
	}
}
