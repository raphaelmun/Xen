using System;
using XenAspects;

namespace XenScript
{    
    public abstract class ConditionSingleNumericComparison<ComparisonType> : Condition
        where ComparisonType : IComparable, IFormattable, IConvertible, IComparable<ComparisonType>, IEquatable<ComparisonType>
    {
        protected Getter<ComparisonType> _subjectValueGetter;
        protected ComparisonType _targetValue;

        public ConditionSingleNumericComparison( ComparisonType targetValue, Getter<ComparisonType> subject )
        {
            _subjectValueGetter = subject;
            _targetValue = targetValue;
        }
    }
}
