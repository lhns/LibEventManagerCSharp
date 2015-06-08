using System;
using System.Collections.Generic;

namespace LibEventManagerCSharp
{
    public class EventRegistry
    {
        private List<EventListener> listeners = new List<EventListener>();

        public void Register<Event>(Action<Event> listener, Object monitor = null) where Event : LibEventManagerCSharp.Event
        {
            listeners.Add(new EventListener(e => listener((Event)e), typeof(Event), monitor));
        }

        public Factory Call<Event, Factory>(EventFactory<Event, Factory> eventFactory) where Event : LibEventManagerCSharp.Event
        {
            return eventFactory.NewEventFactory(this);
        }

        internal Event Call<Event>(Event e) where Event : LibEventManagerCSharp.Event
        {
            List<EventListener> listenersCopy = new List<EventListener>();
            listenersCopy.AddRange(listeners);

            e.OnEventPre();

            foreach (EventListener listener in listenersCopy)
            {
                if (!listener.IsAlive)
                {
                    listeners.Remove(listener);
                }
                else if (listener.type == e.GetType())
                {
                    listener.action(e);
                    if (e.Cancelled) break;
                }
            }

            bool remove = false;

            e.OnEventPost(ref remove);

            if (remove) Remove<Event>();

            return e;
        }

        public void Remove<Event>() where Event : LibEventManagerCSharp.Event
        {
            listeners.RemoveAll(listener => listener.type == typeof(Event));
        }
    }
}
