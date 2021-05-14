using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Live0xUtils.FormUI
{
	internal class ColumnMemberConverter : ExpandableObjectConverter
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
			if (destinationType == typeof(InstanceDescriptor) && value is ColumnMember)
			{
				Type[] types = new Type[2]
				{
					typeof(string),
					typeof(string)
				};
				ColumnMember columnMember = (ColumnMember)value;
				object[] arguments = new object[2]
				{
					columnMember.ColumnName,
					columnMember.Member
				};
				return new InstanceDescriptor(typeof(ColumnMember).GetConstructor(types), arguments);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
