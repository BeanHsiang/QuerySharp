using System;

namespace QuerySharp
{
    [AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field, AllowMultiple = false)]
    public class AliasAttribute : Attribute
    {
        public AliasAttribute(string shrt)
        {
            Short = shrt;
        }

        public string Short { get; private set; }
        public string Item { get; set; }
    }
}
