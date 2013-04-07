﻿
namespace QuerySharp
{
    public enum WhereOperation
    {
        Equal,
        NotEqual,
        Like,
        GreatThan,
        GreatThanOrEqual,
        LessThan,
        LessThanOrEqual,
        Regexp
    }

    public enum ConditionRelation
    {
        None,
        And,
        Or
    }

    public enum OrderOperation
    {
        Asc,
        Desc
    }

    public enum FromOperation
    {
        None,
        InnerJoin,
        LeftJoin
    }
}