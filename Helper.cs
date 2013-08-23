using System.Reflection;
using System;

namespace QuerySharp
{
    public class FieldHelper
    {
        /// <summary>
        /// 将查询字段转成映射后的名称
        /// </summary>
        /// <returns></returns>
        internal static string ConvertField<T>(T t)
        {
            //if (t.GetType().IsClass && t == null)
            //{
            //    return null;
            //}
            if (t is string)
            {
                return Convert.ToString(t);
            }
            string filed = t.ToString();
            var fi = t.GetType().GetField(filed, BindingFlags.Static | BindingFlags.Public);
            if (fi == null)
            {
                return filed;
            }
            var attrs = fi.GetCustomAttributes(typeof(QualifierAttribute), false);
            if (attrs.Length == 0)
            {
                return fi.Name;
            }
            var attr = attrs[0] as QualifierAttribute;
            return string.Format("{0}.{1}", attr.Prefix, string.IsNullOrWhiteSpace(attr.Item) ? fi.Name : attr.Item);
        }
    }
}
