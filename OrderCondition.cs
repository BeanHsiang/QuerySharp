using System;
using System.Text;

namespace QuerySharp
{
    public class OrderCondition : Condition<string>
    {
        public OrderOperation Operation { get; protected set; }

        public OrderCondition(string field, OrderOperation operation = OrderOperation.Asc)
        {
            Field = field;
            Operation = operation;
        }

        public OrderCondition(string field, string order = "asc")
        {
            Field = field;
            Operation = order.Equals("desc", StringComparison.OrdinalIgnoreCase) ? OrderOperation.Desc : OrderOperation.Asc;
        }

        internal override string ToSql()
        {
            if (string.IsNullOrWhiteSpace(Field))
            {
                return string.Empty;
            }
            var sb = new StringBuilder();
            sb.Append(Relation == ConditionRelation.None ? string.Empty : ",");
            sb.AppendFormat("{0} {1}", Field, Operation);
            return sb.ToString();
        }
    }

    public class OrderCondition<TField> : OrderCondition
    {
        public OrderCondition(TField field, OrderOperation operation = OrderOperation.Asc)
            : base(FieldHelper.ConvertField(field), operation)
        {

        }

        public OrderCondition(TField field, string order = "asc")
            : base(FieldHelper.ConvertField(field), order)
        {

        }
    }
}
