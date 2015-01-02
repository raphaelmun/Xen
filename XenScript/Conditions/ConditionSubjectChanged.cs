using System;
using Microsoft.Xna.Framework;
using XenAspects;

namespace XenScript
{
    /// <summary>
    /// This class is responsible for observing a subject.  When evaluated, it records a value.  
    /// Returns true if the subject and target are different, false otherwise.
    /// </summary>
    /// <typeparam name="ComparisonType">The type of the value to compare</typeparam>
    public class ConditionSubjectChanged<SubjectType> : Condition
        where SubjectType : IComparable, IFormattable, IConvertible, IComparable<SubjectType>, IEquatable<SubjectType>
    {
        protected Getter<SubjectType> _subjectValueGetter;
        protected bool _invokedBefore = false;
        protected SubjectType _lastRecordedValue;

        public ConditionSubjectChanged( Getter<SubjectType> subject )
        {
            _subjectValueGetter = subject;
        }

        public override bool Evaluate( ref GameTime gameTime )
        {
            SubjectType newValue = _subjectValueGetter();
            bool changed = !_lastRecordedValue.Equals( newValue );
            _lastRecordedValue = newValue;

            if( !_invokedBefore )
            {
                _invokedBefore = true;
                return true;
            }
            else
            {
                return changed;
            }
        }
    }
}
