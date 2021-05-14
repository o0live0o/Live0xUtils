using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Live0xUtils.FormUI
{
	public class EnumCommonHelper
	{
		public static string[] GetDescriptionList(Type enumType)
		{
			if (enumType == null || !enumType.IsEnum)
			{
				throw new ArgumentException("类型无效", "enumType");
			}
			Array values = Enum.GetValues(enumType);
			Array.Sort(values);
			List<string> list = new List<string>();
			foreach (object item in values)
			{
				string name = Enum.GetName(enumType, item);
				object[] customAttributes = enumType.GetField(name).GetCustomAttributes(typeof(DescriptionAttribute), inherit: false);
				if (customAttributes.Length > 0)
				{
					list.Add(((DescriptionAttribute)customAttributes[0]).Description);
				}
				else
				{
					list.Add(name);
				}
			}
			return list.ToArray();
		}

		public static string[] GetDescriptionList<T>() where T : struct
		{
			Type typeFromHandle = typeof(T);
			if (!typeFromHandle.IsEnum)
			{
				throw new ArgumentException("类型无效", "enumType");
			}
			Array values = Enum.GetValues(typeFromHandle);
			Array.Sort(values);
			List<string> list = new List<string>(20);
			foreach (object item in values)
			{
				string name = Enum.GetName(typeFromHandle, item);
				object[] customAttributes = typeFromHandle.GetField(name).GetCustomAttributes(typeof(DescriptionAttribute), inherit: false);
				if (customAttributes.Length > 0)
				{
					list.Add(((DescriptionAttribute)customAttributes[0]).Description);
				}
				else
				{
					list.Add(name);
				}
			}
			return list.ToArray();
		}

		public static string GetDescription(Type enumType, object value)
		{
			if (enumType == null || !enumType.IsEnum)
			{
				throw new ArgumentException("类型无效", "enumType");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			string name = Enum.GetName(enumType, value);
			if (name != null)
			{
				object[] customAttributes = enumType.GetField(name).GetCustomAttributes(typeof(DescriptionAttribute), inherit: false);
				if (customAttributes.Length > 0)
				{
					return ((DescriptionAttribute)customAttributes[0]).Description;
				}
				return name;
			}
			return string.Empty;
		}

		public static string GetDescription<T>(T value) where T : struct
		{
			Type typeFromHandle = typeof(T);
			if (typeFromHandle == null || !typeFromHandle.IsEnum)
			{
				throw new ArgumentException("类型无效", "enumType");
			}
			string name = Enum.GetName(typeFromHandle, value);
			if (name != null)
			{
				object[] customAttributes = typeFromHandle.GetField(name).GetCustomAttributes(typeof(DescriptionAttribute), inherit: false);
				if (customAttributes.Length > 0)
				{
					return ((DescriptionAttribute)customAttributes[0]).Description;
				}
				return name;
			}
			return string.Empty;
		}

		public static string GetDescription(Type enumType, string name)
		{
			if (enumType == null || !enumType.IsEnum)
			{
				throw new ArgumentException("类型无效", "enumType");
			}
			if (name == null || name == string.Empty)
			{
				throw new ArgumentNullException("name");
			}
			if (Enum.IsDefined(enumType, name))
			{
				object[] customAttributes = enumType.GetField(name).GetCustomAttributes(typeof(DescriptionAttribute), inherit: false);
				if (customAttributes.Length > 0)
				{
					return ((DescriptionAttribute)customAttributes[0]).Description;
				}
				return name;
			}
			return string.Empty;
		}

		public static string GetDescription<T>(string name) where T : struct
		{
			Type typeFromHandle = typeof(T);
			if (typeFromHandle == null || !typeFromHandle.IsEnum)
			{
				throw new ArgumentException("类型无效", "enumType");
			}
			if (name == null || name == string.Empty)
			{
				throw new ArgumentNullException("name");
			}
			if (Enum.IsDefined(typeFromHandle, name))
			{
				object[] customAttributes = typeFromHandle.GetField(name).GetCustomAttributes(typeof(DescriptionAttribute), inherit: false);
				if (customAttributes.Length > 0)
				{
					return ((DescriptionAttribute)customAttributes[0]).Description;
				}
				return name;
			}
			return string.Empty;
		}

		public static object GetValue(Type enumType, string description)
		{
			if (enumType == null || !enumType.IsEnum)
			{
				throw new ArgumentException("类型无效", "enumType");
			}
			if (description == null)
			{
				throw new ArgumentNullException("description");
			}
			string[] names = Enum.GetNames(enumType);
			foreach (string text in names)
			{
				object[] customAttributes = enumType.GetField(text).GetCustomAttributes(typeof(DescriptionAttribute), inherit: false);
				if (customAttributes.Length > 0)
				{
					if (((DescriptionAttribute)customAttributes[0]).Description == description)
					{
						return Enum.Parse(enumType, text);
					}
				}
				else if (text == description)
				{
					return Enum.Parse(enumType, text);
				}
			}
			return null;
		}

		public static T GetValue<T>(string description) where T : struct
		{
			Type typeFromHandle = typeof(T);
			if (typeFromHandle == null || !typeFromHandle.IsEnum)
			{
				throw new ArgumentException("类型无效", "enumType");
			}
			if (description == null)
			{
				throw new ArgumentNullException("description");
			}
			string[] names = Enum.GetNames(typeFromHandle);
			foreach (string text in names)
			{
				object[] customAttributes = typeFromHandle.GetField(text).GetCustomAttributes(typeof(DescriptionAttribute), inherit: false);
				if (customAttributes.Length > 0)
				{
					if (((DescriptionAttribute)customAttributes[0]).Description == description)
					{
						return (T)Enum.Parse(typeFromHandle, text);
					}
				}
				else if (text == description)
				{
					return (T)Enum.Parse(typeFromHandle, text);
				}
			}
			return default(T);
		}

		public static bool IsDefined(Type enumType, string description)
		{
			if (enumType == null || !enumType.IsEnum)
			{
				throw new ArgumentException("类型无效", "enumType");
			}
			if (description == null)
			{
				throw new ArgumentNullException("description");
			}
			string[] names = Enum.GetNames(enumType);
			foreach (string text in names)
			{
				object[] customAttributes = enumType.GetField(text).GetCustomAttributes(typeof(DescriptionAttribute), inherit: false);
				if (customAttributes.Length > 0)
				{
					if (((DescriptionAttribute)customAttributes[0]).Description == description)
					{
						return true;
					}
				}
				else if (text == description)
				{
					return true;
				}
			}
			return false;
		}

		public static bool IsDefined<T>(string description) where T : struct
		{
			Type typeFromHandle = typeof(T);
			if (typeFromHandle == null || !typeFromHandle.IsEnum)
			{
				throw new ArgumentException("类型无效", "enumType");
			}
			if (description == null)
			{
				throw new ArgumentNullException("description");
			}
			string[] names = Enum.GetNames(typeFromHandle);
			foreach (string text in names)
			{
				object[] customAttributes = typeFromHandle.GetField(text).GetCustomAttributes(typeof(DescriptionAttribute), inherit: false);
				if (customAttributes.Length > 0)
				{
					if (((DescriptionAttribute)customAttributes[0]).Description == description)
					{
						return true;
					}
				}
				else if (text == description)
				{
					return true;
				}
			}
			return false;
		}
	}
}
