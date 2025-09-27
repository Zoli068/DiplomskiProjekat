using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Utilities
{
    public class ByteBuffer
    {
        private readonly List<byte> _buffer = new List<byte>();

        /// <summary>
        /// Appends data to the buffer.
        /// </summary>
        public void Append(byte[] data)
        {
            if (data == null || data.Length == 0) return;
            _buffer.AddRange(data);
        }

        /// <summary>
        /// Returns a copy of values from the buffer without removing them.
        /// </summary>
        public byte[] GetValues(int startIndex, int count)
        {
            if (startIndex < 0 || count < 0 || startIndex + count > _buffer.Count)
                throw new ArgumentOutOfRangeException();

            return _buffer.Skip(startIndex).Take(count).ToArray();
        }

        /// <summary>
        /// Removes a number of bytes from the buffer starting at startIndex.
        /// </summary>
        public void RemoveBytes(int startIndex, int count)
        {
            if (startIndex < 0 || count < 0 || startIndex + count > _buffer.Count)
                throw new ArgumentOutOfRangeException();

            _buffer.RemoveRange(startIndex, count);
        }

        /// <summary>
        /// Returns the total number of bytes in the buffer.
        /// </summary>
        public int Length => _buffer.Count;
    }
}