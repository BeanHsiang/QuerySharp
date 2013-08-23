using System.Collections.Generic;
using System.Text;

namespace QuerySharp
{
    public class WhereCondition : Condition
    {
        public object Value { get; protected set; }
        public WhereOperation Operation { get; protected set; }

        //internal WhereCondition()
        //{
        //}

        public WhereCondition(string field, WhereOperation operation, object value)
        {
            Field = field;
            Operation = operation;
            if (value != null)
            {
                Value = operation == WhereOperation.In ? value : value.ToString().Replace("'", "''").Replace("_", "\\_");
            }
        }

        /// <summary>
        /// 将where的条件转成sql语句
        /// </summary>
        /// <returns></returns>
        internal override string ToSql()
        {
            if (string.IsNullOrEmpty(Field) || Value == null || string.IsNullOrEmpty(Value.ToString()))
            {
                return string.Empty;
            }
            var sb = new StringBuilder();
            sb.Append(Relation == ConditionRelation.None || string.IsNullOrEmpty(Prev.ToSql()) ? string.Empty : Relation.ToString() + " ");
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
                case WhereOperation.In:
                    format = "{0} in ({1})";
                    break;
                case WhereOperation.Regexp:
                    format = "{0} REGEXP '{1}'";
                    break;
                default:
                    format = string.Empty;
                    break;
            }
            sb.AppendFormat(format, Field, Value);
            return sb.ToString();
        }
    }

    public class WhereCondition<TField> : WhereCondition
    {
        public WhereCondition(TField field, WhereOperation operation, object value)
            : base(FieldHelper.ConvertField(field), operation, value)
        {

        }
    }
}