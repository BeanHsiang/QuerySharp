using System.Text;

namespace QuerySharp
{
    public class FromCondition : Condition<string>
    {
        public FromOperation Operation { get; protected set; }
        public string Alias { get; protected set; }
        public string Association { get; protected set; }

        public FromCondition(string field, string alias, FromOperation operation = FromOperation.None, string association = "")
        {
            Field = field;
            Alias = alias;
            Operation = operation;
            Association = association;
        }

        internal override string ToSql()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{0}{1}", Relation == ConditionRelation.None ? string.Empty : ConvertOperation(), Field);
            if (!string.IsNullOrWhiteSpace(Alias))
            {
                sb.AppendFormat(" as {0}", Alias);
            }
            if (!string.IsNullOrWhiteSpace(Association))
            {
                sb.AppendFormat(" on {0}", Association);
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
                case FromOperation.Simple:
                    op = ", ";
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
