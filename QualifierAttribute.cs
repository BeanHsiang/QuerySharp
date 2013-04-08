using System;

namespace QuerySharp
{
    [AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field, AllowMultiple = false)]
    public class QualifierAttribute : Attribute
    {
        public QualifierAttribute(string prefix)
        {
            Prefix = prefix;
        }

        public string Prefix { get; private set; }
        public string Item { get; set; }
    }
}
