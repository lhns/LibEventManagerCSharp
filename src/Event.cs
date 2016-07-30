using System;

namespace LibEventManagerCSharp
{
    public class Event
    {
        public bool Cancelled { get; set; } = false;

        protected internal virtual void OnEventPre()
        {
        }

        protected internal virtual void OnEventPost(ref bool remove)
        {
        }
    }
}
