using System.Text;
using System.Text.RegularExpressions;

namespace QuerySharp
{
    public class QueryManager
    {
        public Condition SelectCondition { get; private set; }
        public Condition SelectCountCondition { get; private set; }
        public Condition FromCondition { get; private set; }
        public Condition WhereCondition { get; private set; }
        public Condition OrderCondition { get; private set; }
        public PageCondition PageCondition { get; private set; }

        private StringBuilder _selectBuilder;
        private StringBuilder _fromBuilder;
        private StringBuilder _whereBuilder;
        private StringBuilder _orderBuilder;

        #region Set

        public QueryManager Select(Condition condition)
        {
            SelectCondition = condition;
            BuildSelect();
            return this;
        }

        public QueryManager SelectCount(Condition condition)
        {
            SelectCountCondition = condition;
            return this;
        }

        //public QueryManager SetSelectCondition(SelectCondition condition)
        //{
        //    SelectCondition = condition;
        //    BuildSelect();
        //    return this;
        //}

        //public QueryManager SetSelectCondition<T>(SelectCondition<T> condition)
        //{
        //    SelectCondition = condition;
        //    BuildSelect();
        //    return this;
        //}

        //public QueryManager SetSelectCountCondition<T>(SelectCountCondition<T> condition)
        //{
        //    SelectCountCondition = condition;
        //    return this;
        //}

        public QueryManager From(Condition condition)
        {
            FromCondition = condition;
            BuildFrom();
            return this;
        }

        //public QueryManager SetFromCondition(FromCondition condition)
        //{
        //    return SetFromCondition(condition);
        //}

        public QueryManager Where(Condition condition)
        {
            WhereCondition = condition;
            BuildWhere();
            return this;
        }

        public QueryManager Order(Condition condition)
        {
            OrderCondition = condition;
            BuildOrder();
            return this;
        }

        public QueryManager Page(PageCondition condition)
        {
            PageCondition = condition;
            return this;
        }

        #endregion

        #region Build

        private void BuildSelect()
        {
            _selectBuilder = new StringBuilder();
            var ie = SelectCondition.GetEnumerator();
            while (ie.MoveNext())
            {
                var condition = ie.Current;
                if (condition != null) _selectBuilder.Append(condition.ToSql());
            }
        }

        private void BuildFrom()
        {
            _fromBuilder = new StringBuilder();
            var ie = FromCondition.GetEnumerator();
            while (ie.MoveNext())
            {
                var condition = ie.Current;
                if (condition != null) _fromBuilder.AppendFormat(" {0}", condition.ToSql());
            }
        }

        private void BuildWhere()
        {
            _whereBuilder = new StringBuilder();
            var ie = WhereCondition.GetEnumerator();
            while (ie.MoveNext())
            {
                var condition = ie.Current;
                if (condition != null) _whereBuilder.AppendFormat(" {0}", condition.ToSql());
            }
        }

        private void BuildOrder()
        {
            _orderBuilder = new StringBuilder();
            var ie = OrderCondition.GetEnumerator();
            while (ie.MoveNext())
            {
                var condition = ie.Current;
                if (condition != null) _orderBuilder.AppendFormat(" {0}", condition.ToSql());
            }
        }

        #endregion

        public string GenerateSql()
        {
            var sb = new StringBuilder();
            sb.Append("select ");
            sb.Append(_selectBuilder);
            sb.Append(" from ");
            sb.Append(_fromBuilder);
            if (_whereBuilder.Length > 0)
            {
                sb.Append(" where ");
                sb.Append(_whereBuilder);
            }
            if (_orderBuilder.Length > 0)
            {
                sb.Append(" order by ");
                sb.Append(_orderBuilder);
            }
            sb.Append(PageCondition.ToSql());
            sb.Append(";");
            return Regex.Replace(sb.ToString(), "\\s{2}", " ");
        }

        public string GenerateCountSql()
        {
            var sb = new StringBuilder();
            sb.Append("select ");
            sb.Append(SelectCountCondition.ToSql());
            sb.Append(" from ");
            sb.Append(_fromBuilder);
            if (_whereBuilder.Length > 0)
            {
                sb.Append(" where ");
                sb.Append(_whereBuilder);
            }
            sb.Append(";");
            return Regex.Replace(sb.ToString(), "\\s{2}", " ");
        }
    }
}
