using Microsoft.Xna.Framework;
using XenAspects;

namespace XenScript
{
    /// <summary>
    /// This condition evaluates to true when the observed subject is null
    /// </summary>
    public class ConditionSubjectIsNull : Condition
    {
        protected Getter<object> _subjectValueGetter;

        public ConditionSubjectIsNull( Getter<object> subject )
        {
            _subjectValueGetter = subject;
        }

        public override bool Evaluate( ref GameTime gt )
        {
            return ( _subjectValueGetter() == null );
        }
    }

    /// <summary>
    /// This condition evaluates to true when the observed subject is not null
    /// </summary>
    public class ConditionSubjectIsNotNull : Condition
    {
        protected Getter<object> _subjectValueGetter;

        public ConditionSubjectIsNotNull( Getter<object> subject )
        {
            _subjectValueGetter = subject;
        }

        public override bool Evaluate( ref GameTime gt )
        {
            return ( _subjectValueGetter() != null );
        }
    }
}
