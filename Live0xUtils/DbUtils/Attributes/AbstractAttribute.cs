using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Live0xUtils.DbUtils.Attributes
{
    [AttributeUsage(AttributeTargets.All,AllowMultiple =true)]
    public abstract class AbstractAttribute : Attribute
    {
        protected string _field;

        public AbstractAttribute(string field)
        {
            this._field = field;
        }

        public abstract bool Validate(object o);
    }
}
