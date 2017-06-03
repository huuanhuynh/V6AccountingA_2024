using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace V6Soft.Common.Utils
{
    /// <summary>
    ///     A wrapper for IEnumerable interface with Dispose method
    ///     and Disposed event.
    /// </summary>
    public sealed class DisposableEnumerable<T> : IEnumerable<T>, IDisposable
    {
        private IEnumerable<T> m_InnerSequence;

        public event EventHandler Disposed;


        public DisposableEnumerable(IEnumerable<T> original)
        {
            m_InnerSequence = original ?? Enumerable.Empty<T>();
        }


        public IEnumerator<T> GetEnumerator()
        {
            return m_InnerSequence.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_InnerSequence.GetEnumerator();
        }

        public void Dispose()
        {
            m_InnerSequence = null;
            OnDisposed();
        }


        private void OnDisposed()
        {
            EventHandler handler = Disposed;
            if (handler != null)
            {
                handler(this, null);
            }
        }

    }
}
