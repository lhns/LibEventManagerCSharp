using System;
using System.Collections.Generic;

namespace LibEventManagerCSharp
{
    public class EventRegistry
    {
        private readonly List<EventListener> _listeners = new List<EventListener>();

        public void Register<TEvent>(Action<TEvent> listener, object monitor = null) where TEvent : Event
        {
            _listeners.Add(new EventListener(e => listener((TEvent) e), typeof(TEvent), monitor));
        }

        public TEvent Call<TEvent>(TEvent e) where TEvent : Event
        {
            var listenersCopy = new List<EventListener>();
            listenersCopy.AddRange(_listeners);

            e.OnEventPre();

            foreach (var listener in listenersCopy)
            {
                if (!listener.IsAlive)
                {
                    _listeners.Remove(listener);
                }
                else if (listener.Type == e.GetType())
                {
                    listener.Action(e);
                    if (e.Cancelled) break;
                }
            }

            var remove = false;

            e.OnEventPost(ref remove);

            if (remove) Remove<TEvent>();

            return e;
        }

        public void Remove<TEvent>() where TEvent : Event
        {
            _listeners.RemoveAll(listener => listener.Type == typeof(TEvent));
        }
    }
}
