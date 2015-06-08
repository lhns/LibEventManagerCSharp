using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibEventManagerCSharp
{
    internal class EventListener
    {
        public readonly Action<Event> action;
        public readonly Type type;

        private WeakReference monitor;

        public bool IsAlive
        {
            get { return action != null && type != null && (monitor == null || monitor.IsAlive); }
        }

        public EventListener(Action<Event> action, Type type, Object monitor)
        {
            this.action = action;
            this.type = type;
            monitor = monitor == null ? null : new WeakReference(monitor);
        }
    }
}
