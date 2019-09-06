using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Live0xUtils.DbUtils.Attributes
{
    [AttributeUsage(AttributeTargets.Field,AllowMultiple =true)]
    public class RegAttribute : AbstractAttribute
    {
        public RegAttribute(string field) : base(field)
        {
        }

        public override bool Validate(object o)
        {
            string s = o == null ? null : o.ToString();
            if (string.IsNullOrEmpty(s))
                return false;
            Regex regex = new Regex(base._field);
            return regex.IsMatch(s);
        }
    }
}
