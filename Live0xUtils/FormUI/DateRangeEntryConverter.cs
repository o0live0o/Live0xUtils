using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Live0xUtils.FormUI
{
	internal class DateRangeEntryConverter : ExpandableObjectConverter
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
			if (destinationType == typeof(InstanceDescriptor) && value is DateRangeEntry)
			{
				Type[] types = new Type[5]
				{
					typeof(string),
					typeof(string),
					typeof(string),
					typeof(string),
					typeof(string)
				};
				DateRangeEntry dateRangeEntry = (DateRangeEntry)value;
				object[] arguments = new object[5]
				{
					dateRangeEntry.CaptionMember,
					dateRangeEntry.CheckedMember,
					dateRangeEntry.BeginDateMember,
					dateRangeEntry.EndDateMember,
					dateRangeEntry.TagMember
				};
				return new InstanceDescriptor(typeof(DateRangeEntry).GetConstructor(types), arguments);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
