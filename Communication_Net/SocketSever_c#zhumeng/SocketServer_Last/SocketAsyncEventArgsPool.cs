using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketSever
{

    // Represents a collection of reusable SocketAsyncEventArgs objects.
    internal class SocketAsyncEventArgsPool
    {

        private Stack<SocketAsyncEventArgs> _pool;

        // Initializes the object pool to the specified size
        //
        // The "capacity" parameter is the maximum number of
        // SocketAsyncEventArgs objects the pool can hold
        public SocketAsyncEventArgsPool(int capacity)
        {
            _pool = new Stack<SocketAsyncEventArgs>(capacity);
        }

        // Add a SocketAsyncEventArg instance to the pool
        //
        //The "item" parameter is the SocketAsyncEventArgs instance
        // to add to the pool
        public void Push(SocketAsyncEventArgs item)
        {
            if (item == null) { throw new ArgumentNullException("Items added to a SocketAsyncEventArgsPool cannot be null"); }
            lock (_pool)
            {
                _pool.Push(item);
            }
        }

        // Removes a SocketAsyncEventArgs instance from the pool
        // and returns the object removed from the pool
        public SocketAsyncEventArgs Pop()
        {
            lock (_pool)
            {
                return _pool.Pop();
            }
        }

        /// <summary>
        /// 清空栈中元素
        /// </summary>
        public void Clear()
        {
            lock (_pool)
            {
                _pool.Clear();
            }
        }

        // The number of SocketAsyncEventArgs instances in the pool
        public int Count
        {
            get { return _pool.Count; }
        }

    }

}
