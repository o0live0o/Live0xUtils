using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Live0xUtils.FormUI
{
    public class EntryEditor : UITypeEditor
	{
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (context != null && context.Instance != null && provider != null)
			{
				BaseEntry baseEntry = value as BaseEntry;
				switch (context.Instance.GetType().Name)
				{
					case "DataGridView":
						{
							CGridEntry cGridEntry = baseEntry as CGridEntry;
							if (cGridEntry == null)
							{
								cGridEntry = new CGridEntry();
							}
							HybridDictionary hybridDictionary2 = new HybridDictionary(10);
							if (cGridEntry.ColumnMembers != null && cGridEntry.ColumnMembers.Length > 0)
							{
								for (int j = 0; j < cGridEntry.ColumnMembers.Length; j++)
								{
									if (!hybridDictionary2.Contains(cGridEntry.ColumnMembers[j].ColumnName))
									{
										hybridDictionary2.Add(cGridEntry.ColumnMembers[j].ColumnName, cGridEntry.ColumnMembers[j].Member);
									}
								}
							}
							ArrayList arrayList2 = new ArrayList();
							DataGridView dataGridView = context.Instance as DataGridView;
							foreach (DataGridViewColumn column in dataGridView.Columns)
							{
								string dataPropertyName = column.DataPropertyName;
								if (dataPropertyName.Trim() != string.Empty)
								{
									arrayList2.Add(new ColumnMember(column.Name, hybridDictionary2.Contains(dataPropertyName) ? hybridDictionary2[dataPropertyName].ToString() : column.DataPropertyName));
								}
							}
							if (arrayList2.Count > 0)
							{
								cGridEntry.ColumnMembers = new ColumnMember[arrayList2.Count];
								arrayList2.CopyTo(cGridEntry.ColumnMembers);
							}
							baseEntry = cGridEntry;
							break;
						}
					case "CheckBox":
					case "ToolStripCheckBox":
						if (baseEntry == null)
						{
							baseEntry = new CheckBoxEntry();
						}
						break;
					case "DateRange":
						if (baseEntry == null)
						{
							baseEntry = new DateRangeEntry();
						}
						break;
					default:
						if (baseEntry == null)
						{
							baseEntry = new TextEntry();
						}
						break;
				}
				IWindowsFormsEditorService windowsFormsEditorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
				if (windowsFormsEditorService == null)
				{
					return null;
				}
				EntryForm entryForm = new EntryForm(baseEntry);
				if (windowsFormsEditorService.ShowDialog(entryForm) == DialogResult.OK)
				{
					baseEntry = entryForm.Entry;
					context.OnComponentChanged();
					return baseEntry;
				}
				return value;
			}
			return base.EditValue(context, provider, value);
		}

		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}
	}
}
