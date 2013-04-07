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
            return String.Format(" {0}{1}", Relation == ConditionRelation.None ? string.Empty : ",", Field);
        }
    }

    public class SelectCondition<TField> : Condition<TField>
    {
        public SelectCondition(TField field)
        {
            Field = field;
        }

        internal override string ToSql()
        {
            var field = FieldHelper.ConvertField(Field);
            if (field == null)
            {
                return string.Empty;
            }
            return String.Format(" {0}{1}", Relation == ConditionRelation.None ? string.Empty : ",", field);
        }
    }

    public class SelectCountCondition : SelectCondition
    {
        public SelectCountCondition(string field) : base(field)
        {
        }

        internal override string ToSql()
        {
            return String.Format(" {0}count({1})", Relation == ConditionRelation.None ? string.Empty : ",", Field);
        }
    }

    public class SelectCountCondition<TField> : Condition<TField>
    {
        public SelectCountCondition(TField field)
        {
            Field = field;
        }

        internal override string ToSql()
        {
            var field = FieldHelper.ConvertField(Field);
            if (field == null)
            {
                return string.Empty;
            }
            return String.Format(" {0}count({1})", Relation == ConditionRelation.None ? string.Empty : ",", field);
        }
    }
}

