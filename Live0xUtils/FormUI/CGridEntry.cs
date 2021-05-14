using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Live0xUtils.FormUI
{
    [TypeConverter(typeof(CGridEntryConverter))]
    public class CGridEntry : BaseEntry
    {
        private string m_member = string.Empty;

        private ColumnMember[] m_columnMembers;

        [Description("获取或设置表格控件对应填充实体的属性名称。")]
        [DefaultValue("")]
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

        [DefaultValue("")]
        [Category("Behavior")]
        [Description("获取或设置DataGridView表格各列对应填充实体的属性名称。")]
        public ColumnMember[] ColumnMembers
        {
            get
            {
                return m_columnMembers;
            }
            set
            {
                m_columnMembers = value;
            }
        }

        public CGridEntry()
        {
        }

        public CGridEntry(string member, ColumnMember[] columnMembers)
        {
            m_member = member;
            m_columnMembers = columnMembers;
        }

        internal override void Clear(Control control)
        {
            (control as DataGridView)?.Rows.Clear();
        }

        internal void ClearSingle(Control control)
        {
            DataGridView dataGridView = control as DataGridView;
            if (dataGridView == null)
            {
                return;
            }
            int index = dataGridView.CurrentRow.Index;
            ColumnMember[] columnMembers = m_columnMembers;
            foreach (ColumnMember columnMember in columnMembers)
            {
                if (!(columnMember.Member.Trim() != string.Empty))
                {
                    continue;
                }
                string columnName = columnMember.ColumnName.Trim();
                if (dataGridView.Columns.Contains(columnName))
                {
                    try
                    {
                        dataGridView[dataGridView.Columns[columnName].Index, index].Value = string.Empty;
                    }
                    catch
                    {
                    }
                }
            }
        }

        internal override void FillEntity(Control control, object entity)
        {
            DataGridView dataGridView = control as DataGridView;
            if (control == null || dataGridView == null)
            {
                throw new ArgumentNullException("control");
            }
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            if (m_member.Trim() == string.Empty || m_columnMembers == null || m_columnMembers.Length <= 0)
            {
                return;
            }
            try
            {
                IList list = null;
                Type propertyType = PropertyHelper.GetPropertyType(m_member, entity);
                if (propertyType.IsArray)
                {
                    Type elementType = propertyType.GetElementType();
                    CGridRefillTool cGridRefillTool = new CGridRefillTool(dataGridView, m_columnMembers);
                    list = cGridRefillTool.GetRefilledRowDataList(elementType);
                    if (list != null)
                    {
                        ArrayList arrayList = new ArrayList(list);
                        list = arrayList.ToArray(elementType);
                    }
                }
                PropertyHelper.SetPropertyValue(m_member, entity, list);
            }
            catch
            {
                throw;
            }
        }

        internal object GetRefilledMemberItem(DataGridView dgv, int index, Type type)
        {
            if (dgv == null)
            {
                throw new ArgumentNullException("dgv");
            }
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            if (m_columnMembers == null || m_columnMembers.Length <= 0)
            {
                return null;
            }
            try
            {
                CGridRefillTool cGridRefillTool = new CGridRefillTool(dgv, m_columnMembers);
                return cGridRefillTool.GetRefillEntity(index, type);
            }
            catch
            {
                throw;
            }
        }

        internal override void DisplayEntity(Control control, object entity)
        {
            DataGridView dataGridView = control as DataGridView;
            if (control == null || dataGridView == null)
            {
                throw new ArgumentNullException("control");
            }
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            dataGridView.Rows.Clear();
            if (m_member.Trim() == string.Empty || m_columnMembers == null || m_columnMembers.Length <= 0)
            {
                return;
            }
            try
            {
                object propertyValue = PropertyHelper.GetPropertyValue(m_member, entity);
                if (propertyValue != null && propertyValue is IList)
                {
                    CGridTool cGridTool = new CGridTool(dataGridView, m_columnMembers);
                    cGridTool.FillGrid((IList)propertyValue);
                }
            }
            catch
            {
                throw;
            }
        }

        internal void DisplaySingle(Control control, object entity)
        {
            DataGridView dataGridView = control as DataGridView;
            if (control == null || dataGridView == null)
            {
                throw new ArgumentNullException("control");
            }
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            if (m_columnMembers != null && m_columnMembers.Length > 0)
            {
                try
                {
                    CGridTool cGridTool = new CGridTool(dataGridView, m_columnMembers);
                    cGridTool.FillGridRowOfMember(dataGridView.CurrentRow.Index, entity);
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}