using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Live0xUtils.FormUI
{
	internal class TextEntryConverter : ExpandableObjectConverter
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
			try
			{
				if (destinationType == typeof(InstanceDescriptor) && value is TextEntry)
				{
					Type[] types = new Type[2]
					{
						typeof(string),
						typeof(string)
					};
					TextEntry textEntry = (TextEntry)value;
					object[] arguments = new object[2]
					{
						textEntry.TextMember,
						textEntry.TagMember
					};
					return new InstanceDescriptor(typeof(TextEntry).GetConstructor(types), arguments);
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}
			catch
			{
				return null;
			}
		}
	}
}
