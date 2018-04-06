using System;

namespace LibEventManagerCSharp
{
    public class Event
    {
        public bool Cancelled { get; private set; }

        public void Cancel()
        {
            Cancelled = true;
        }

        protected internal virtual void OnEventPre()
        {
        }

        protected internal virtual void OnEventPost(ref bool remove)
        {
        }
    }
}
