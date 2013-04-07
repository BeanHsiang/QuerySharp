using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuerySharp
{
    public class PageCondition
    {
        private int _itemCount;
        private const int DefaultPageSize = 15;
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public int PageCount { get; private set; }
        public int ItemCount
        {
            get { return _itemCount; }
            set
            {
                if (value <= 0) return;
                _itemCount = value;
                PageCount = (int)(_itemCount - 1) / PageSize + 1;
                if (PageIndex > PageCount) PageIndex = PageCount;
            }
        }

        public PageCondition(int pageIndex, int pageSize = 0)
        {
            if (pageIndex < 1)
                PageIndex = 1;
            else
                PageIndex = pageIndex;

            if (pageSize <= 0)
                PageSize = DefaultPageSize;
            else
                PageSize = pageSize;
        }

        public string ToSql()
        {
            return string.Format(" limit {0},{1}", PageIndex * PageSize, PageSize);
        }
    }
}
