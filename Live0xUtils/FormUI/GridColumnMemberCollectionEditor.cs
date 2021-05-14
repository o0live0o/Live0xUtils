using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Live0xUtils.FormUI
{
	public class GridColumnMemberCollectionEditor : ArrayEditor
	{
		public GridColumnMemberCollectionEditor(Type type)
			: base(type)
		{
		}

		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (context != null && context.Instance is DataGridView && provider != null)
			{
				ColumnMember[] array = value as ColumnMember[];
				HybridDictionary hybridDictionary = new HybridDictionary(10);
				if (array != null && array.Length > 0)
				{
					for (int i = 0; i < array.Length; i++)
					{
						if (!hybridDictionary.Contains(array[i].ColumnName))
						{
							hybridDictionary.Add(array[i].ColumnName, array[i].Member);
						}
					}
				}
				DataGridView dataGridView = (DataGridView)context.Instance;
				ArrayList arrayList = new ArrayList();
				foreach (DataGridViewColumn column in dataGridView.Columns)
				{
					string dataPropertyName = column.DataPropertyName;
					if (dataPropertyName.Trim() != string.Empty)
					{
						arrayList.Add(new ColumnMember(column.Name, column.DataPropertyName));
					}
				}
				if (arrayList.Count > 0)
				{
					value = new ColumnMember[arrayList.Count];
					arrayList.CopyTo((ColumnMember[])value);
				}
			}
			return base.EditValue(context, provider, value);
		}
	}
}
