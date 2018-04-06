using System;
using System.Collections.Generic;

namespace LibEventManagerCSharp
{
    public class EventRegistry
    {
        private volatile List<WeakReference<EventListener>> listeners = new List<WeakReference<EventListener>>();

        private volatile List<WeakReference<EventListener>> addedListeners = new List<WeakReference<EventListener>>();

        private object listenersLock = new object();

        private object addedListenersLock = new object();

        public EventListener<TEvent> Register<TEvent>(Action<TEvent> action) where TEvent : Event
        {
            var listener = new EventListener<TEvent>(action);
            lock (addedListenersLock)
            {
                addedListeners.Add(new WeakReference<EventListener>(listener));
            }
            return listener;
        }

        public void UnregisterAll<TEvent>() where TEvent : Event
        {
            lock (listenersLock)
            {
                listeners.ForEach(listenerRef =>
                {
                    EventListener listener;
                    if (listenerRef.TryGetTarget(out listener) && listener.EventType == typeof(TEvent))
                    {
                        listener.Unregister();
                    }
                });
            }
        }
        
        public TEvent Call<TEvent>(TEvent e) where TEvent : Event
        {
            lock (listenersLock) {
                List<WeakReference<EventListener>> tmpListeners;
                lock (addedListenersLock)
                {
                    tmpListeners = addedListeners;
                    addedListeners = new List<WeakReference<EventListener>>();
                }

                tmpListeners.AddRange(listeners);

                e.OnEventPre();

                var newListeners = new List<WeakReference<EventListener>>();
                foreach (var listenerRef in tmpListeners)
                {
                    EventListener listener;
                    listenerRef.TryGetTarget(out listener);
                    if (listener != null && listener.Registered)
                    {
                        newListeners.Add(listenerRef);
                        if (!e.Cancelled && listener is EventListener<TEvent>)
                        {
                            (listener as EventListener<TEvent>).Action(e);
                        }
                    }
                }

                listeners = newListeners;

                var remove = false;

                e.OnEventPost(ref remove);

                if (remove) UnregisterAll<TEvent>();

                return e;
            }
        }

        
    }
}
