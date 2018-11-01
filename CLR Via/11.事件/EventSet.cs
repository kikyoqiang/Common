using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLR_Via
{
    public sealed class EventKey
    {

    }
    public class EventSet
    {
        private readonly object locker = new object();
        private readonly Dictionary<EventKey, Delegate> m_events = new Dictionary<EventKey, Delegate>();
        public void Add(EventKey key, Delegate handler)
        {
            lock (locker)
            {
                Delegate d;
                m_events.TryGetValue(key, out d);
                m_events[key] = Delegate.Combine(d, handler);
            }
        }
        public void Remove(EventKey key, Delegate handler)
        {
            lock (locker)
            {
                Delegate d;
                if (m_events.TryGetValue(key, out d))
                    d = Delegate.Remove(d, handler);
                if (d != null)
                    m_events[key] = d;
                else
                    m_events.Remove(key);
            }
        }
        public void Raise(EventKey key, object sender, EventArgs e)
        {
            Delegate d;
            lock (locker)
            {
                m_events.TryGetValue(key, out d);
            }
            if (d != null)
            {
                d.DynamicInvoke(new object[] { sender, e });
            }
        }
    }

    public class FooEventArgs : EventArgs
    {

    }
    public class TypeWithLotsOfEvents
    {
        private readonly EventSet m_eventSet = new EventSet();
        private static readonly EventKey s_fooEventArgs = new EventKey();
        protected EventSet EventSet { get { return m_eventSet; } }
        public event EventHandler<FooEventArgs> Foo
        {
            add { m_eventSet.Add(s_fooEventArgs, value); }
            remove { m_eventSet.Remove(s_fooEventArgs, value); }
        }
        protected virtual void OnFoo(FooEventArgs e)
        {
            m_eventSet.Raise(s_fooEventArgs, this, e);
        }
        public void SimilateFoo()
        {
            OnFoo(new FooEventArgs() { });
        }
    }
}
