using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public QueryManager SetSelectCondition(Condition condition)
        {
            SelectCondition = condition;
            BuildSelect();
            return this;
        }

        public QueryManager SetSelectCondition(SelectCondition condition)
        {
            SelectCondition = condition;
            BuildSelect();
            return this;
        }

        public QueryManager SetSelectCondition<T>(SelectCondition<T> condition)
        {
            SelectCondition = condition;
            BuildSelect();
            return this;
        }

        public QueryManager SetSelectCountCondition<T>(SelectCountCondition<T> condition)
        {
            SelectCountCondition = condition;
            return this;
        }

        public QueryManager SetFromCondition(FromCondition condition)
        {
            FromCondition = condition;
            BuildFrom();
            return this;
        }

        public QueryManager SetWhereCondition<T>(WhereCondition<T> condition)
        {
            WhereCondition = condition;
            BuildWhere();
            return this;
        }

        public QueryManager SetOrderCondition<T>(OrderCondition<T> condition)
        {
            OrderCondition = condition;
            BuildOrder();
            return this;
        }

        public QueryManager SetPageCondition(PageCondition condition)
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
                if (condition != null) _selectBuilder.AppendFormat(" {0}", condition.ToSql());
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
            sb.Append("select");
            sb.Append(_selectBuilder);
            sb.Append(" from");
            sb.Append(_fromBuilder);
            if (_whereBuilder.Length > 0)
            {
                sb.Append(" where");
                sb.Append(_whereBuilder);
            }
            if (_orderBuilder.Length > 0)
            {
                sb.Append(" order by");
                sb.Append(_orderBuilder);
            }
            sb.Append(PageCondition.ToSql());
            return sb.ToString();
        }

        public string GenerateCountSql()
        {
            var sb = new StringBuilder();
            sb.Append("select");
            sb.Append(SelectCountCondition.ToSql());
            sb.Append(" from");
            sb.Append(_fromBuilder);
            if (_whereBuilder.Length > 0)
            {
                sb.Append(" where");
                sb.Append(_whereBuilder);
            }
            return sb.ToString();
        }
    }
}
