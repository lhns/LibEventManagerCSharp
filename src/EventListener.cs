using System;

namespace LibEventManagerCSharp
{
    public abstract class EventListener
    {
        public Type EventType { get; }

        public bool Registered { get; private set; } = true;

        public void Unregister()
        {
            Registered = false;
        }
    }

    public class EventListener<TEvent>: EventListener where TEvent : Event
    {
        public Action<TEvent> Action { get; }

        public EventListener(Action<TEvent> action)
        {
            this.Action = action;
        }
        
    }
}
