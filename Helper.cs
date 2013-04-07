using System.Reflection;

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
            string filed = t.ToString();
            var fi = t.GetType().GetField(filed, BindingFlags.Static | BindingFlags.Public);
            if (fi == null)
            {
                return filed;
            }
            var attrs = fi.GetCustomAttributes(typeof(AliasAttribute), false);
            if (attrs.Length == 0)
            {
                return fi.Name;
            }
            var attr = attrs[0] as AliasAttribute;
            return string.Format("{0}.{1}", attr.Short, string.IsNullOrEmpty(attr.Item) ? fi.Name : attr.Item);
        }
    }
}
