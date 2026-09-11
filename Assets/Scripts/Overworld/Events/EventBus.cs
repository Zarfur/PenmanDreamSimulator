

using System;
using System.Collections.Generic;

public class EventBus
{

    public static readonly EventBus Instance = new EventBus();
    private readonly Dictionary<Type, Delegate> events = new Dictionary<Type, Delegate>();

    public void Subscribe<T>(Action<T> handler)
    {
        var type = typeof(T);
        if(events.TryGetValue(type, out var existing))
        {
            events[type] = Delegate.Combine(existing, handler);
        }
        else
        {
            events[type] = handler;
        }
    }

    public void Unsubscribe<T>(Action<T> handler)
    {
        var type = typeof(T);

        if(!events.TryGetValue(type, out var existing)) return;

        var combined = Delegate.Remove(existing, handler);
        if (combined == null) events.Remove(type);
        else events[type] = combined;
    }

    public void Publish<T>(T evt)
    {
        var type = typeof(T);
        if(events.TryGetValue(type, out var existing))
        {
            (existing as Action<T>)?.Invoke(evt);
        }
    }

    public void Clear()
    {
        events.Clear();
    }
}
