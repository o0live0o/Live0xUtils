using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Live0xUtils.FormUI
{

    [ToolboxBitmap(typeof(EntitytFiller), "EntityFiller.ico")]
    [ProvideProperty("EntityField", typeof(object))]
    public class EntitytFiller : Component, IExtenderProvider
    {
        private Hashtable _fillEntrys;

        public EntitytFiller()
        {
            _fillEntrys = new Hashtable();
        }

        public bool CanExtend(object extendee)
        {
            string text = "Label,TextBox,CheckBox,ComboBox,DateTimePicker";
            if (text.IndexOf(extendee.GetType().Name) == -1)
            {
                return false;
            }
            return true;
        }

        [Description("获取数据实体的关联属性。")]
        [Category("填充")]
        [ExtenderProvidedProperty]
        [DefaultValue(null)]
        [Editor(typeof(EntryEditor), typeof(UITypeEditor))]
        public BaseEntry GetEntityField(object control)
        {
            if (control is Control)
            {
                return _fillEntrys[control] as BaseEntry;
            }
            if (control is ToolStripControlHost)
            {
                return _fillEntrys[((ToolStripControlHost)control).Control] as BaseEntry;
            }
            return null;
        }

        [ExtenderProvidedProperty]
        [Description("设置数据实体的关联属性。")]
        [Editor(typeof(EntryEditor), typeof(UITypeEditor))]
        public void SetEntityField(object control, BaseEntry value)
        {
            if (control is Control)
            {
                _fillEntrys[control] = value;
            }
            else if (control is ToolStripControlHost)
            {
                _fillEntrys[((ToolStripControlHost)control).Control] = value;
            }
        }

        public void FillEntity(object entity)
        {
            if (entity == null)
            {
                return;
            }
            try
            {
                foreach (DictionaryEntry fillEntry in _fillEntrys)
                {
                    Control control = fillEntry.Key as Control;
                    BaseEntry baseEntry = fillEntry.Value as BaseEntry;
                    if (control != null)
                    {
                        baseEntry?.FillEntity(control, entity);
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public void DisplayEntity(object entity)
        {
            try
            {
                foreach (DictionaryEntry fillEntry in _fillEntrys)
                {
                    Control control = fillEntry.Key as Control;
                    BaseEntry baseEntry = fillEntry.Value as BaseEntry;
                    if (control != null && baseEntry != null)
                    {
                        baseEntry.Clear(control);
                        if (entity != null)
                        {
                            baseEntry.DisplayEntity(control, entity);
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        internal void ClearSingle(Control control)
        {
            if (control == null)
            {
                return;
            }
            try
            {
                BaseEntry baseEntry = _fillEntrys[control] as BaseEntry;
                if (control != null && baseEntry != null)
                {

                    baseEntry.Clear(control);
                }
            }
            catch
            {
                throw;
            }
        }

        internal void DisplaySingle(Control control, object entity)
        {
            if (control == null)
            {
                return;
            }
            try
            {
                BaseEntry baseEntry = _fillEntrys[control] as BaseEntry;
                if (control == null || baseEntry == null)
                {
                    return;
                }
                //if (baseEntry is GridEntry)
                //{
                //    ((GridEntry)baseEntry).ClearSingle(control);
                //    ((GridEntry)baseEntry).DisplaySingle(control, entity);
                //    return;
                //}
                baseEntry.Clear(control);
                if (entity != null)
                {
                    baseEntry.DisplayEntity(control, entity);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
