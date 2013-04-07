using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuerySharp
{
    [AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field, AllowMultiple = false)]
    public class AliasAttribute : Attribute
    {
        public AliasAttribute(string item)
        {
            Item = item;
        }

        public string Short { get; private set; }
        public string Item { get; set; }
    }
}
