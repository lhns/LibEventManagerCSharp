using System;

namespace LibEventManagerCSharp
{
    internal class EventListener
    {
        public Action<Event> Action { get; }
        public Type Type { get; }
        private readonly WeakReference _monitor;

        public bool IsAlive => Action != null && Type != null && (_monitor == null || _monitor.IsAlive);

        public EventListener(Action<Event> action, Type type, Object monitor)
        {
            this.Action = action;
            this.Type = type;
            this._monitor = monitor == null ? null : new WeakReference(monitor);
        }
    }
}
