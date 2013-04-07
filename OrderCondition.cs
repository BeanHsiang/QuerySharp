using System.Text;

namespace QuerySharp
{
    public class OrderCondition<TField> : Condition<TField>
    {
        public OrderOperation Operation { get; protected set; }

        public OrderCondition(TField field, OrderOperation operation)
        {
            Field = field;
            Operation = operation;
        }

        internal override string ToSql()
        {
            var field = FieldHelper.ConvertField(Field);
            if (field == null)
            {
                return string.Empty;
            }
            var sb = new StringBuilder();
            sb.AppendFormat(" {0}", Relation == ConditionRelation.None ? string.Empty : ",");
            sb.AppendFormat("{0} {1}", field, Operation);
            return sb.ToString();
        }
    }
}
