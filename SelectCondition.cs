using System;

namespace QuerySharp
{
    public class SelectCondition : Condition<string>
    {
        public SelectCondition(string field)
        {
            Field = field;
        }

        internal override string ToSql()
        {
            return string.IsNullOrWhiteSpace(Field) ? string.Empty : String.Format("{0}{1}", Relation == ConditionRelation.None ? string.Empty : ",", Field);
        }
    }

    public class SelectCondition<TField> : SelectCondition
    {
        public SelectCondition(TField field)
            : base(FieldHelper.ConvertField(field))
        {

        }
    }

    public class SelectCountCondition : SelectCondition
    {
        public SelectCountCondition(string field)
            : base(field)
        {
        }

        internal override string ToSql()
        {
            return string.IsNullOrWhiteSpace(Field) ? string.Empty : String.Format("{0}count({1})", Relation == ConditionRelation.None ? string.Empty : ",", Field);
        }
    }

    public class SelectCountCondition<TField> : SelectCountCondition
    {
        public SelectCountCondition(TField field)
            : base(FieldHelper.ConvertField(field))
        {

        }
    }
}

