using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Live0xUtils.FormUI
{
	public sealed class PropertyHelper
	{
		private static BindingFlags m_getValueBinding = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty;

		private static BindingFlags m_setValueBinding = BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty;

		public static PropertyInfo GetPropertyInfo(string name, object entity)
		{
			if (name == null)
			{
				return null;
			}
			name = name.Replace(" ", "");
			if (name.StartsWith("this."))
			{
				name = name.Substring("this.".Length);
			}
			if (name == string.Empty)
			{
				return null;
			}
			if (entity == null)
			{
				return null;
			}
			string text = string.Empty;
			int num = name.IndexOf('.');
			if (num > 0 && num < name.Length - 1)
			{
				text = name.Substring(num + 1);
				name = name.Substring(0, num);
			}
			try
			{
				if (text == string.Empty)
				{
					return entity.GetType().GetProperty(name);
				}
				object propertyValue = GetPropertyValue(name, entity);
				return GetPropertyInfo(text, propertyValue);
			}
			catch
			{
				return null;
			}
		}

		public static Type GetPropertyType(string name, object entity)
		{
			if (name == null)
			{
				return null;
			}
			name = name.Replace(" ", "");
			if (name.StartsWith("this."))
			{
				name = name.Substring("this.".Length);
			}
			if (name == string.Empty)
			{
				return null;
			}
			if (entity == null)
			{
				return null;
			}
			string text = string.Empty;
			int num = name.IndexOf('.');
			if (num > 0 && num < name.Length - 1)
			{
				text = name.Substring(num + 1);
				name = name.Substring(0, num);
			}
			try
			{
				if (text == string.Empty)
				{
					return entity.GetType().GetProperty(name).PropertyType;
				}
				object propertyValue = GetPropertyValue(name, entity);
				return GetPropertyType(text, propertyValue);
			}
			catch
			{
				return null;
			}
		}

		public static object GetPropertyValue(string name, object entity)
		{
			if (name != null && name.Trim() != string.Empty && entity != null)
			{
				name = name.Replace(" ", "");
				string text = string.Empty;
				int num = name.IndexOf('.');
				if (num > 0 && num < name.Length - 1)
				{
					text = name.Substring(num + 1);
					name = name.Substring(0, num);
				}
				try
				{
					if (name == "this")
					{
						if (text == string.Empty)
						{
							return entity;
						}
						return GetPropertyValue(text, entity);
					}
					Type type = entity.GetType();
					if (type.IsEnum)
					{
						switch (name.ToLower())
						{
							case "value":
								return entity;
							case "name":
								return Enum.GetName(type, entity);
							case "description":
								{
									string name2 = Enum.GetName(type, entity);
									object[] customAttributes = type.GetField(name2).GetCustomAttributes(typeof(DescriptionAttribute), inherit: false);
									if (customAttributes.Length > 0)
									{
										return ((DescriptionAttribute)customAttributes[0]).Description;
									}
									return entity;
								}
							default:
								return entity;
						}
					}
					if (type.IsClass)
					{
						object obj = type.InvokeMember(name, m_getValueBinding, null, entity, null);
						if (text == string.Empty)
						{
							return obj;
						}
						return GetPropertyValue(text, obj);
					}
					return null;
				}
				catch
				{
					return null;
				}
			}
			return null;
		}

		public static void SetPropertyValue(string name, object obj, object value)
		{
			if (name == null || obj == null)
			{
				return;
			}
			name = name.Trim();
			if (name == string.Empty)
			{
				return;
			}
			string text = string.Empty;
			int num = name.IndexOf('.');
			if (num > 0 && num < name.Length - 1)
			{
				text = name.Substring(num + 1);
				name = name.Substring(0, num);
			}
			try
			{
				if (name == "this")
				{
					if (text == string.Empty)
					{
						obj = value;
					}
					else
					{
						SetPropertyValue(text, obj, value);
					}
					return;
				}
			}
			catch
			{
				return;
			}
			if (!obj.GetType().IsClass)
			{
				return;
			}
			PropertyInfo property = obj.GetType().GetProperty(name, m_setValueBinding);
			if (property == null)
			{
				return;
			}
			if (text != string.Empty)
			{
				try
				{
					if (property.PropertyType.IsEnum)
					{
						switch (text.ToLower())
						{
							case "value":
								property.SetValue(obj, value, null);
								break;
							case "name":
								property.SetValue(obj, Enum.Parse(property.PropertyType, value.ToString()), null);
								break;
							case "description":
								property.SetValue(obj, EnumCommonHelper.GetValue(property.PropertyType, value.ToString()), null);
								break;
							default:
								property.SetValue(obj, value, null);
								break;
						}
					}
					else
					{
						object obj3 = GetPropertyValue(name, obj);
						if (obj3 == null)
						{
							obj3 = Activator.CreateInstance(GetPropertyType(name, obj));
							property.SetValue(obj, obj3, null);
						}
						SetPropertyValue(text, obj3, value);
					}
				}
				catch
				{
				}
			}
			else
			{
				object value2 = ConvertPropertyValue(property, value);
				try
				{
					property.SetValue(obj, value2, null);
				}
				catch
				{
				}
			}
		}

		public static void SetPropertyValue(PropertyInfo info, object obj, object value)
		{
			if (!(info == null) && obj != null)
			{
				object value2 = ConvertPropertyValue(info, value);
				try
				{
					info.SetValue(obj, value2, null);
				}
				catch
				{
				}
			}
		}

		private static object ConvertPropertyValue(PropertyInfo info, object value)
		{
			try
			{
				object obj = null;
				if (info.PropertyType.IsValueType)
				{
					if (value is string && (string)value == string.Empty)
					{
						return Activator.CreateInstance(info.PropertyType);
					}
					TypeConverter converter = TypeDescriptor.GetConverter(info.PropertyType);
					if (value != null && converter.CanConvertFrom(value.GetType()))
					{
						if (value is string && info.PropertyType.Equals(typeof(decimal)))
						{
							string pattern = "[" + CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol + " ]";
							string text = Regex.Replace((string)value, pattern, "");
							pattern = CultureInfo.CurrentCulture.NumberFormat.PercentSymbol;
							if (text.Trim().EndsWith(pattern))
							{
								text = text.Replace(pattern, "");
								text = (decimal.Parse(text, NumberStyles.Any) / 100m).ToString();
							}
							return decimal.Parse(text, NumberStyles.Any);
						}
						return converter.ConvertFrom(value);
					}
					return value;
				}
				if (info.PropertyType.Equals(typeof(string)) && value == null)
				{
					return string.Empty;
				}
				return value;
			}
			catch
			{
				return null;
			}
		}
	}
}
