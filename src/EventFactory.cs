using System;

namespace LibEventManagerCSharp
{
    public abstract class EventFactory<Event, Factory> where Event : LibEventManagerCSharp.Event
    {
        protected internal abstract Factory NewEventFactory(EventRegistry registry);

        protected Event Call(EventRegistry registry, Event e)
        {
            return registry.Call(e);
        }
    }
}
