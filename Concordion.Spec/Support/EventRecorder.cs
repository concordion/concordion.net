using System;
using System.Collections.Generic;
using System.Linq;
using org.concordion.api.listener;

namespace Concordion.Spec.Support
{
    public class EventRecorder : AssertEqualsListener, ThrowableCaughtListener
    {
        private readonly List<Object> m_Events;

        public EventRecorder()
	    {
            this.m_Events = new List<Object>();
	    }

        public Object GetLast(Type eventType)
        {
            Object lastMatch = null;
            foreach (var anEvent in this.m_Events.Where(eventType.IsInstanceOfType))
            {
                lastMatch = anEvent;
            }
            return lastMatch;
        }

        public void throwableCaught(ThrowableCaughtEvent caughtEvent)
        {
            this.m_Events.Add(caughtEvent);
        }

        #region IAssertEqualsListener Members

        public void successReported(AssertSuccessEvent successEvent)
        {
            this.m_Events.Add(successEvent);
        }

        public void failureReported(AssertFailureEvent failureEvent)
        {
            this.m_Events.Add(failureEvent);
        }

        #endregion
    }
}
