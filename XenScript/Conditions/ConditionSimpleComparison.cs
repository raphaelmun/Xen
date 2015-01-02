using System;
using Microsoft.Xna.Framework;
using XenAspects;

namespace XenScript
{
    /// <summary>
    /// This class is responsible for comparing a subject and target value.  
    /// Returns true if the subject and target are equal, false otherwise.
    /// </summary>
    /// <typeparam name="ComparisonType">The type of the value to compare</typeparam>
    public class ConditionSubjectEqualsTarget<ComparisonType> : ConditionSingleNumericComparison<ComparisonType>
        where ComparisonType : IComparable, IFormattable, IConvertible, IComparable<ComparisonType>, IEquatable<ComparisonType>
    {
        public ConditionSubjectEqualsTarget( ComparisonType targetValue, Getter<ComparisonType> subject )
            : base( targetValue, subject )
        {
        }

        public override bool Evaluate( ref GameTime gameTime )
        {
            return ( _subjectValueGetter().CompareTo( _targetValue ) == 0 ); 
        }
    }

    /// <summary>
    /// This class is responsible for comparing a subject and target value.  
    /// Returns true if the subject is greater than the target, false otherwise.
    /// </summary>
    /// <typeparam name="ComparisonType">The type of the value to compare</typeparam>
    public class ConditionSubjectGreaterThanTarget<ComparisonType> : ConditionSingleNumericComparison<ComparisonType>
        where ComparisonType : IComparable, IFormattable, IConvertible, IComparable<ComparisonType>, IEquatable<ComparisonType>
    {
        public ConditionSubjectGreaterThanTarget( ComparisonType targetValue, Getter<ComparisonType> subject )
            : base( targetValue, subject )
        {
        }

        public override bool Evaluate( ref GameTime gameTime )
        {
            return ( _subjectValueGetter().CompareTo( _targetValue ) > 0 );
        }
    }

    /// <summary>
    /// This class is responsible for comparing a subject and target value.  
    /// Returns true if the subject is less than the target, false otherwise.
    /// </summary>
    /// <typeparam name="ComparisonType">The type of the value to compare</typeparam>
    public class ConditionSubjectLessThanTarget<ComparisonType> : ConditionSingleNumericComparison<ComparisonType>
        where ComparisonType : IComparable, IFormattable, IConvertible, IComparable<ComparisonType>, IEquatable<ComparisonType>
    {
        public ConditionSubjectLessThanTarget( ComparisonType targetValue, Getter<ComparisonType> subject )
            : base( targetValue, subject )
        {
        }

        public override bool Evaluate( ref GameTime gameTime )
        {
            return ( _subjectValueGetter().CompareTo( _targetValue ) < 0 );
        }
    }
}
