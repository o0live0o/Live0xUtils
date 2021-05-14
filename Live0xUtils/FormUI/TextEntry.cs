using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Live0xUtils.FormUI
{
    [TypeConverter(typeof(TextEntryConverter))]
    public class TextEntry : BaseEntry
    {
        private string m_textMember = string.Empty;

        private string m_tagMember = string.Empty;

        [Category("Data")]
        [DefaultValue("")]
        [Description("获取或设置控件的 Text 属性对应填充实体的属性名称。")]
        public string TextMember
        {
            get
            {
                return m_textMember;
            }
            set
            {
                m_textMember = value;
            }
        }

        [Category("Data")]
        [DefaultValue("")]
        [Description("获取或设置控件的 Tag 属性对应填充实体的属性名称。")]
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

        public TextEntry()
        {
        }

        public TextEntry(string textMember, string tagMember)
        {
            m_textMember = textMember;
            m_tagMember = tagMember;
        }

        internal override void Clear(Control control)
        {
            if (control == null)
            {
                return;
            }
            if (m_textMember != string.Empty)
            {
                if (control is ComboBox)
                {
                    (control as ComboBox)?.Items.Clear();
                }
                else
                {
                    control.Text = "";
                }
            }
            if (m_tagMember != string.Empty)
            {
                control.Tag = null;
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
                if (m_textMember != string.Empty)
                {
                    PropertyHelper.SetPropertyValue(m_textMember, entity, control.Text);
                }
                if (m_tagMember != string.Empty)
                {
                    PropertyHelper.SetPropertyValue(m_tagMember, entity, control.Tag);
                }
            }
            catch
            {
                throw;
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
                m_textMember = m_textMember.Trim();
                if (m_textMember != string.Empty)
                {
                    string text = string.Empty;
                    string[] array = Regex.Split(m_textMember, "(\\S+)");
                    string[] array2 = array;
                    foreach (string text2 in array2)
                    {
                        string text3 = text2.Trim();
                        if (text3 == string.Empty)
                        {
                            text += text2;
                            continue;
                        }
                        object propertyValue = PropertyHelper.GetPropertyValue(text3, entity);
                        if (propertyValue == null)
                        {
                            continue;
                        }
                        if (propertyValue is DateTime)
                        {
                            string format = "yyyy-MM-dd";
                            text += ((DateTime)propertyValue).ToString(format);
                        }
                        else
                        {
                            text += propertyValue.ToString();
                        }
                    }
                    if (control is ComboBox)
                    {
                        ComboBox comboBox = control as ComboBox;
                        if (comboBox != null)
                        {
                            comboBox.Items.Clear();
                            comboBox.Items.Add(text);
                            try
                            {
                                comboBox.SelectedIndex = 0;
                            }
                            catch
                            {
                            }
                        }
                    }
                    else
                    {
                        control.Text = text;
                    }
                }
                if (m_tagMember != string.Empty)
                {
                    control.Tag = PropertyHelper.GetPropertyValue(m_tagMember, entity);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
