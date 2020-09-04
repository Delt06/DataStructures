using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures
{
	public class FixedStack<T> : ICollection, IEnumerable<T>
	{
		#region Private fields

		private readonly T[] _internalArray;

		#endregion

		#region Constructors

		public FixedStack(int maxSize)
		{
			if (maxSize < 0) throw new ArgumentOutOfRangeException(nameof(maxSize), "Maximum size cannot be negative.");
			_internalArray = new T[maxSize];
		}

		public FixedStack(long maxSize)
		{
			if (maxSize < 0L)
				throw new ArgumentOutOfRangeException(nameof(maxSize), "Maximum size cannot be negative.");
			_internalArray = new T[maxSize];
		}

		#endregion

		#region Public properties

		/// <summary>
		/// Stores the current number of elements inside stack.
		/// </summary>
		/// <exception cref="InvalidOperationException"></exception>
		public int Count
		{
			get
			{
				if (LongCount > int.MaxValue)
					throw new InvalidOperationException("Count value does not fit inside Int32 type.");

				return (int) LongCount;
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
		public int MaxSize
		{
			get
			{
				if (_internalArray.LongLength > int.MaxValue)
					throw new InvalidOperationException("Count value does not fit inside Int32 type.");

				return _internalArray.Length;
			}
		}

		/// <summary>
		/// Maximum possible size of the stack.
		/// </summary>
		public long MaxLongSize => _internalArray.LongLength;

		/// <summary>
		/// Identifies whether stack achieved its maximum size/
		/// </summary>
		public bool IsFull => LongCount == _internalArray.LongLength;

		#endregion

		#region Public methods

		/// <summary>
		/// Push an item to the top of the stack.
		/// </summary>
		/// <param name="item">Item to push.</param>
		/// <exception cref="InvalidOperationException"></exception>
		public void Push(T item)
		{
			if (IsFull) throw new InvalidOperationException("Stack is full.");
			_internalArray[LongCount++] = item;
		}

		/// <summary>
		/// Remove an item from the top of the stack and return the removed item.
		/// </summary>
		/// <returns>Removed item.</returns>
		/// <exception cref="InvalidOperationException"></exception>
		public T Pop()
		{
			if (LongCount == 0L) throw new InvalidOperationException("Stack is empty.");
			LongCount--;
			var popped = _internalArray[LongCount];
			_internalArray[LongCount] = default;
			return popped;
		}

		/// <summary>
		/// Look at the top item.
		/// </summary>
		/// <returns>Item on top of the stack.</returns>
		/// <exception cref="InvalidOperationException"></exception>
		public T Peek()
		{
			if (LongCount == 0) throw new InvalidOperationException("Stack is empty.");
			return _internalArray[LongCount - 1];
		}

		/// <summary>
		/// Remove all items from the stack.
		/// </summary>
		public void Clear()
		{
			for (var i = 0L; i < LongCount; i++)
			{
				_internalArray[i] = default;
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
		public void CopyTo(T[] array, int index)
		{
			if (array is null) throw new ArgumentNullException(nameof(array));

			for (var i = 0L; i < LongCount && i + index < array.LongLength; i++)
			{
				array[i + index] = _internalArray[i];
			}
		}

		void ICollection.CopyTo(Array array, int index)
		{
			if (array is null) throw new ArgumentNullException(nameof(array));

			try
			{
				for (var i = 0L; i < LongCount && i + index < array.LongLength; i++)
				{
					array.SetValue(_internalArray[i], i + index);
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
				yield return _internalArray[i];
			}
		}

		bool ICollection.IsSynchronized => true;

		/// <summary>
		/// Thread synchronization root.
		/// </summary>
		public object SyncRoot { get; } = new object();

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		#endregion
	}
}