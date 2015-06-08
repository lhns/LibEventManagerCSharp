using System;

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
            this.monitor = monitor == null ? null : new WeakReference(monitor);
        }
    }
}
