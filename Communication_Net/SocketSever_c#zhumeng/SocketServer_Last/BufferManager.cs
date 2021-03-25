using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketSever
{
    // This class creates a single large buffer which can be divided up
    // and assigned to SocketAsyncEventArgs objects for use with each
    // socket I/O operation. 
    // This enables bufffers to be easily reused and guards against
    // fragmenting heap memory.
    //
    // The operations exposed on the BufferManager class are not thread safe.
    internal class BufferManager
    {

        //buffer缓冲区大小
        private int _numBytes;
        //缓冲区
        private byte[] _buffer;
        private Stack<int> _freeIndexPool;
        private int _currentIndex;
        private int _bufferSize;

        public BufferManager(int totalBytes, int bufferSize)
        {
            _numBytes = totalBytes;
            _currentIndex = 0;
            _bufferSize = bufferSize;
            _freeIndexPool = new Stack<int>();
        }

        /// <summary>
        /// 给buffer分配缓冲区
        /// </summary>
        public void InitBuffer()
        {
            _buffer = new byte[_numBytes];
        }

        /// <summary>
        ///  将buffer添加到args的IO缓冲区中，并设置offset
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public bool SetBuffer(SocketAsyncEventArgs args)
        {
            if (_freeIndexPool.Count > 0)
            {
                args.SetBuffer(_buffer, _freeIndexPool.Pop(), _bufferSize);
            }
            else
            {
                if ((_numBytes - _bufferSize) < _currentIndex)
                {
                    return false;
                }
                args.SetBuffer(_buffer, _currentIndex, _bufferSize);
                _currentIndex += _bufferSize;
            }
            return true;
        }

        /// <summary>
        /// 将buffer从args的IO缓冲区中释放
        /// </summary>
        /// <param name="args"></param>
        public void FreeBuffer(SocketAsyncEventArgs args)
        {
            _freeIndexPool.Push(args.Offset);
            args.SetBuffer(null, 0, 0);
        }

        /// <summary>
        /// 释放全部buffer缓存
        /// </summary>
        public void FreeAllBuffer()
        {
            _freeIndexPool.Clear();
            _currentIndex = 0;
            _buffer = null;
        }
    }
}

