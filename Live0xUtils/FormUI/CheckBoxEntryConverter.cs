using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Live0xUtils.FormUI
{
	internal class CheckBoxEntryConverter : ExpandableObjectConverter
	{
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == typeof(InstanceDescriptor))
			{
				return true;
			}
			return base.CanConvertTo(context, destinationType);
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(InstanceDescriptor) && value is CheckBoxEntry)
			{
				Type[] types = new Type[1]
				{
					typeof(string)
				};
				CheckBoxEntry checkBoxEntry = (CheckBoxEntry)value;
				object[] arguments = new object[1]
				{
					checkBoxEntry.CheckedMember
				};
				return new InstanceDescriptor(typeof(CheckBoxEntry).GetConstructor(types), arguments);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
