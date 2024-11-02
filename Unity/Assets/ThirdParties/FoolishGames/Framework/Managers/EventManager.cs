using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventParam
{

}

public class EventManager : SystemBasis<EventManager>
{
    public delegate void EventCallback(EventParam param = null);

    private Queue<KeyValuePair<Events, EventParam>> _msgs;

    private Dictionary<Events, List<CallbackInstance>> _callbacks;

    public IReadOnlyDictionary<Events, List<CallbackInstance>> Callbacks { get { return _callbacks; } }

    public override void Initialize()
    {
        base.Initialize();
        _callbacks = new Dictionary<Events, List<CallbackInstance>>();
        _msgs = new Queue<KeyValuePair<Events, EventParam>>();
    }

    public sealed class CallbackInstance
    {
        public bool Once { get; set; }

        public EventCallback Callback { get; set; }
    }

    public void RegistEvent(Events events, EventCallback callback)
    {
        RegistEvent(events, false, callback);
    }

    public void RegistEvent(Events events, bool once, EventCallback callback)
    {
        if (callback == null) { return; }
        if (!_callbacks.ContainsKey(events))
        {
            _callbacks.Add(events, new List<CallbackInstance>());
        }
        _callbacks[events].Add(new CallbackInstance()
        {
            Callback = callback,
            Once = once,
        });
    }

    public void UnregistEvent(Events events, EventCallback callback)
    {
        if (!_callbacks.ContainsKey(events))
        {
            return;
        }
        var list = _callbacks[events];
        for (int i = list.Count - 1; i >= 0; i--)
        {
            if (list[i].Callback == null || list[i].Callback == callback)
            {
                list.RemoveAt(i);
                continue;
            }
        }
        if (list.Count == 0)
        {
            _callbacks.Remove(events);
        }
    }

    public void Send(Events events, EventParam param = null)
    {
        if (Util.IsRunningOnUnityThread())
        {
            Callback(events, param);
        }
        else
        {
            _msgs.Enqueue(new KeyValuePair<Events, EventParam>(events, param));
        }
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (_msgs.Count > 0)
        {
            var kv = _msgs.Dequeue();
            Callback(kv.Key, kv.Value);
        }
    }

    private void Callback(Events events, EventParam param)
    {
        if (_callbacks.ContainsKey(events))
        {
            var list = _callbacks[events];
            for (int i = 0; i < list.Count; i++)
            {
                var listener = list[i];
                if (listener.Callback != null)
                {
                    listener.Callback.Invoke(param);
                }
                if (listener.Callback == null || listener.Once)
                {
                    list.RemoveAt(i);
                    i--;
                }
            }
            if (list.Count == 0)
            {
                _callbacks.Remove(events);
            }
        }
    }
}
