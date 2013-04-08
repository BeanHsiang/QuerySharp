using System.Text;

namespace QuerySharp
{
    public abstract class Condition
    {
        /// <summary>
        /// 与前一个条件的连接关系
        /// </summary>
        public ConditionRelation Relation { get; protected set; }
        public dynamic Field { get; protected set; }

        public Condition First { get; protected set; }
        public Condition Next { get; protected set; }

        protected Condition()
        {
            Relation = ConditionRelation.None;
            First = this;
        }

        public Condition Or(Condition condition)
        {
            condition.Relation = ConditionRelation.Or;
            condition.First = First;
            Next = condition;
            return condition;
        }

        public Condition And(Condition condition)
        {
            condition.Relation = ConditionRelation.And;
            condition.First = First;
            Next = condition;
            return condition;
        }

        internal ConditionEnumerator GetEnumerator()
        {
            //var conditon = MemberwiseClone() as Condition;
            //if (conditon != null)
            //{
            //    //conditon.Relation = ConditionRelation.None;
            //    return new ConditionEnumerator(conditon);
            //}
            //return new ConditionEnumerator(null);
            return new ConditionEnumerator(this);
        }

        internal abstract string ToSql();
    }

    public abstract class Condition<TField> : Condition
    {
        public new TField Field { get; protected set; }
    }

    public class GroupCondition : Condition
    {
        private readonly Condition _first;

        public GroupCondition(Condition condition)
        {
            _first = condition;
        }

        internal override string ToSql()
        {
            var sb = new StringBuilder();
            var ie = _first.GetEnumerator();
            while (ie.MoveNext())
            {
                var condition = ie.Current;
                if (condition != null) sb.Append(condition.ToSql());
            }
            if (sb.Length > 0)
            {
                sb.Insert(0, "(");
                sb.Append(")");
                return sb.ToString();
            }
            return string.Empty;
        }
    }
}
