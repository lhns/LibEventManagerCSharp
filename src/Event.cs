using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibEventManagerCSharp
{
    public class Event
    {
        private Boolean cancelled = false;

        public bool Cancelled
        {
            get {
                return cancelled;
            }
            set
            {
                cancelled = value;
            }
        }

        protected internal virtual void OnEventPre() { }

        protected internal virtual void OnEventPost(ref bool remove) { }
    }
}
