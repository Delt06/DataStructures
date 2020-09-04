using System;
using System.Collections.Generic;

namespace DataStructures
{
	/// <summary>
	/// Priority Queue ADT.
	/// </summary>
	/// <typeparam name="T">Type of items in queue.</typeparam>
	public interface IPriorityQueue<T> : IEnumerable<T> where T : IComparable<T>
	{
		/// <summary>
		/// Number of items in the queue.
		/// </summary>
		int Count { get; }

		/// <summary>
		/// Add a new element to the queue.
		/// </summary>
		/// <param name="item">Item to add.</param>
		void Enqueue(T item);

		/// <summary>
		/// Remove the last item.
		/// </summary>
		/// <returns>Removed item.</returns>
		T Dequeue();

		/// <summary>
		/// Look at the last item.
		/// </summary>
		/// <returns>The last item.</returns>
		T Peek();
	}
}