using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures
{
	/// <summary>
	/// Binary heap having minimum element at the root position.
	/// </summary>
	/// <typeparam name="T">Type of items in heap.</typeparam>
	public class MinHeap<T> : IPriorityQueue<T> where T : IComparable<T>
	{
		#region Contructors

		/// <summary>
		/// Create a new Min Heap specifying initial capacity.
		/// </summary>
		/// <param name="capacity">Initial size of the internal array.</param>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public MinHeap(int capacity)
		{
			if (capacity <= 0) throw new ArgumentOutOfRangeException(nameof(capacity));
			_list = new List<T>(capacity);
		}

		/// <summary>
		/// Create a new Min Heap.
		/// </summary>
		public MinHeap() => _list = new List<T>();

		#endregion

		#region Private fields

		/// <summary>
		/// List containing all items in the heap.
		/// </summary>
		private readonly List<T> _list;

		#endregion

		#region Private properties

		/// <summary>
		/// Get the index of the parent of a particular node.
		/// </summary>
		/// <param name="index">Index of a node.</param>
		/// <returns>Index of the parent.</returns>
		private static int GetParent(int index) => (index - 1) / 2;

		/// <summary>
		/// Get the index of the left child of a particular node.
		/// </summary>
		/// <param name="index">Index of a node.</param>
		/// <returns>Index of the left child.</returns>
		private static int GetLeftChild(int index) => 2 * index + 1;

		/// <summary>
		/// Get the index of the right child of a particular node.
		/// </summary>
		/// <param name="index">Index of a node.</param>
		/// <returns>Index of the right child.</returns>
		private static int GetRightChild(int index) => 2 * index + 2;

		/// <summary>
		/// Identifies whether an index can be used to access items in heap.
		/// </summary>
		/// <param name="index">Item index.</param>
		/// <returns>True if index is valid, false otherwise.</returns>
		private bool IsValidIndex(int index) => 0 <= index && index < _list.Count;

		#endregion

		#region Public properties

		/// <summary>
		/// Size of the internal array.
		/// </summary>
		public int Capacity => _list.Capacity;

		#endregion

		#region Public methods

		/// <summary>
		/// Add a new item to the heap.
		/// </summary>
		/// <param name="item">Item to add.</param>
		/// <exception cref="ArgumentNullException"></exception>
		public void Add(T item)
		{
			if (item == null) throw new ArgumentNullException(nameof(item));

			_list.Add(item);

			var currentIndex = _list.Count - 1;
			var parentIndex = GetParent(currentIndex);
			while (currentIndex > 0 && _list[currentIndex].CompareTo(_list[parentIndex]) < 0)
			{
				var temp = _list[currentIndex];
				_list[currentIndex] = _list[parentIndex];
				_list[parentIndex] = temp;
				currentIndex = parentIndex;
				parentIndex = GetParent(parentIndex);
			}
		}

		/// <summary>
		/// Remove the least item from the heap.
		/// </summary>
		/// <returns>Removed item.</returns>
		/// <exception cref="InvalidOperationException"></exception>
		public T RemoveMin()
		{
			if (_list.Count == 0) throw new InvalidOperationException();

			var result = _list[0];
			_list[0] = _list[_list.Count - 1];
			_list.RemoveAt(_list.Count - 1);
			var currentIndex = 0;
			var leftIndex = GetLeftChild(0);
			var rightIndex = GetRightChild(0);

			while (IsValidIndex(leftIndex) || IsValidIndex(rightIndex))
			{
				var minIndex = currentIndex;
				if (IsValidIndex(leftIndex) && _list[leftIndex].CompareTo(_list[minIndex]) < 0) minIndex = leftIndex;
				if (IsValidIndex(rightIndex) && _list[rightIndex].CompareTo(_list[minIndex]) < 0) minIndex = rightIndex;

				if (minIndex == currentIndex) break;

				var temp = _list[currentIndex];
				_list[currentIndex] = _list[minIndex];
				_list[minIndex] = temp;

				currentIndex = minIndex;
				leftIndex = GetLeftChild(currentIndex);
				rightIndex = GetRightChild(currentIndex);
			}

			return result;
		}

		/// <summary>
		/// Look at the minimum element.
		/// </summary>
		/// <returns>Minimum element</returns>
		/// <exception cref="InvalidOperationException"></exception>
		public T Min()
		{
			if (_list.Count == 0) throw new InvalidOperationException();
			return _list[0];
		}

		#endregion

		#region IPriorityQueue

		/// <summary>
		/// Number of items in the heap.
		/// </summary>
		public int Count => _list.Count;

		/// <summary>
		/// Add a new element to the heap.
		/// </summary>
		/// <param name="item">Item to add.</param>
		void IPriorityQueue<T>.Enqueue(T item)
		{
			if (item == null) throw new ArgumentNullException(nameof(item));
			Add(item);
		}

		/// <summary>
		/// Remove the least item.
		/// </summary>
		/// <returns>Removed item.</returns>
		T IPriorityQueue<T>.Dequeue()
		{
			if (Count == 0) throw new InvalidOperationException();
			return RemoveMin();
		}

		/// <summary>
		/// Look at the least item.
		/// </summary>
		/// <returns>The least item.</returns>
		T IPriorityQueue<T>.Peek()
		{
			if (_list.Count == 0) throw new InvalidOperationException();
			return _list[0];
		}

		#endregion

		#region IEnumerable

		public IEnumerator<T> GetEnumerator() => _list.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		#endregion
	}
}