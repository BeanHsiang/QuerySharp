using System.Text;

namespace QuerySharp
{
    public class WhereCondition<TField> : Condition<TField>
    {
        public object Value { get; protected set; }
        public WhereOperation Operation { get; protected set; }

        public WhereCondition(TField field, WhereOperation operation, object value)
        {
            Field = field;
            Operation = operation;
            Value = value;
        }

        /// <summary>
        /// 将where的条件转成sql语句
        /// </summary>
        /// <returns></returns>
        internal override string ToSql()
        {
            var field = FieldHelper.ConvertField(Field);
            if (field == null || Value == null)
            {
                return string.Empty;
            }
            var sb = new StringBuilder();
            sb.Append(Relation == ConditionRelation.None ? string.Empty : Relation.ToString() + " ");
            string format;
            switch (Operation)
            {
                case WhereOperation.Equal:
                    format = "{0} = {1}";
                    break;
                case WhereOperation.StringEqual:
                    format = "{0} = '{1}'";
                    break;
                case WhereOperation.Like:
                    format = "{0} like '%{1}%'";
                    break;
                case WhereOperation.NotEqual:
                    format = "{0} <> {1}";
                    break;
                case WhereOperation.GreatThan:
                    format = "{0} > {1}";
                    break;
                case WhereOperation.LessThan:
                    format = "{0} < {1}";
                    break;
                case WhereOperation.GreatThanOrEqual:
                    format = "{0} >= {1}";
                    break;
                case WhereOperation.LessThanOrEqual:
                    format = "{0} <= {1}";
                    break;
                case WhereOperation.Regexp:
                    format = "{0} REGEXP '{1}'";
                    break;
                default:
                    format = string.Empty;
                    break;
            }
            sb.AppendFormat(format, field, Value);
            return sb.ToString();
        }
    }
}