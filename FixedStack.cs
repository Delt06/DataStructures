using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace DataStructures
{
    public class FixedStack<T> : ICollection, IEnumerable<T>
    {
        #region Private fields

        [NotNull, ItemCanBeNull] public readonly T[] InternalArray;

        #endregion

        #region Constructors

        public FixedStack(int maxSize)
        {
            if (maxSize < 0) throw new ArgumentOutOfRangeException(nameof(maxSize), "Maximum size cannot be negative.");
            InternalArray = new T[maxSize];
        }

        public FixedStack(long maxSize)
        {
            if (maxSize < 0L) throw new ArgumentOutOfRangeException(nameof(maxSize), "Maximum size cannot be negative.");
            InternalArray = new T[maxSize];
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Stores the current number of elements inside stack.
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public int Count {
            get {
                if (LongCount > int.MaxValue)
                {
                    throw new InvalidOperationException("Count value does not fit inside Int32 type.");
                }

                return (int)LongCount;
            }
        }

        /// <summary>
        /// Stores the current number of elements inside stack.
        /// </summary>
        public long LongCount { get; private set; }

        /// <summary>
        /// Maximum possible size of the stack.
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public int MaxSize {
            get {
                if (InternalArray.LongLength > int.MaxValue)
                {
                    throw new InvalidOperationException("Count value does not fit inside Int32 type.");
                }

                return InternalArray.Length;
            }
        }

        /// <summary>
        /// Maximum possible size of the stack.
        /// </summary>
        public long MaxLongSize => InternalArray.LongLength;

        /// <summary>
        /// Identifies whether stack achieved its maximum size/
        /// </summary>
        public bool IsFull => LongCount == InternalArray.LongLength;

        #endregion

        #region Public methods

        /// <summary>
        /// Push an item to the top of the stack.
        /// </summary>
        /// <param name="item">Item to push.</param>
        /// <exception cref="InvalidOperationException"></exception>
        public void Push([CanBeNull] T item)
        {
            if (IsFull) throw new InvalidOperationException("Stack is full.");
            InternalArray[LongCount++] = item;
        }

        /// <summary>
        /// Remove an item from the top of the stack and return the removed item.
        /// </summary>
        /// <returns>Removed item.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        [CanBeNull]
        public T Pop()
        {
            if (LongCount == 0L) throw new InvalidOperationException("Stack is empty.");
            LongCount--;
            var popped = InternalArray[LongCount];
            InternalArray[LongCount] = default;
            return popped;
        }

        /// <summary>
        /// Look at the top item.
        /// </summary>
        /// <returns>Item on top of the stack.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        [CanBeNull]
        public T Peek()
        {
            if (LongCount == 0) throw new InvalidOperationException("Stack is empty.");
            return InternalArray[LongCount - 1];
        }

        /// <summary>
        /// Remove all items from the stack.
        /// </summary>
        public void Clear()
        {
            for (var i = 0L; i < LongCount; i++)
            {
                InternalArray[i] = default;
            }

            LongCount = 0;
        }

        #endregion

        #region ICollection members

        /// <summary>
        /// Copy items from stack to the given array.
        /// </summary>
        /// <param name="array">Array to copy items to.</param>
        /// <param name="index">Start index.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void CopyTo([NotNull, ItemCanBeNull] T[] array, int index)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));

            for (var i = 0L; i < LongCount && i + index < array.LongLength; i++)
            {
                array[i + index] = InternalArray[i];
            }
        }

        void ICollection.CopyTo([ItemCanBeNull] Array array, int index)
        {
            if (array is null) throw new ArgumentNullException(nameof(array));

            try
            {
                for (var i = 0L; i < LongCount && i + index < array.LongLength; i++)
                {
                    array.SetValue(InternalArray[i], i + index);
                }
            }
            catch (ArgumentException)
            {
                throw new ArgumentException("The given array has more than one dimension.");
            }
            catch (InvalidCastException)
            {
                throw new InvalidCastException($"{typeof(T)} is not compatible with type of the given array.");
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (var i = LongCount - 1; i >= 0L; i--)
            {
                yield return InternalArray[i];
            }
        }

        bool ICollection.IsSynchronized => true;

        /// <summary>
        /// Thread synchronization root.
        /// </summary>
        public object SyncRoot { get; } = new object();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}