using System.Text;

namespace QuerySharp
{
    public class FromCondition : Condition<string>
    {
        public FromOperation Operation { get; protected set; }
        public string Alias { get; protected set; }

        public FromCondition(string field, FromOperation operation, string alias)
        {
            Field = field;
            Operation = operation;
            Alias = alias;
        }

        internal override string ToSql()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{0}{1}", Relation == ConditionRelation.None ? string.Empty : ConvertOperation(), Field);
            if (!string.IsNullOrEmpty(Alias))
            {
                sb.AppendFormat(" as {0}", Alias);
            }
            return sb.ToString();
        }

        private string ConvertOperation()
        {
            string op;
            switch (Operation)
            {
                case FromOperation.None:
                    op = string.Empty;
                    break;
                case FromOperation.InnerJoin:
                    op = "inner join ";
                    break;
                case FromOperation.LeftJoin:
                    op = "left join ";
                    break;
                default:
                    op = string.Empty;
                    break;
            }
            return op;
        }
    }
}
