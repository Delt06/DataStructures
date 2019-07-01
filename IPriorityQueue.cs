using System;
using System.Collections.Generic;
using JetBrains.Annotations;

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
        void Enqueue([NotNull] T item);

        /// <summary>
        /// Remove the last item.
        /// </summary>
        /// <returns>Removed item.</returns>
        [NotNull] T Dequeue();

        /// <summary>
        /// Look at the last item.
        /// </summary>
        /// <returns>The last item.</returns>
        [NotNull] T Peek();
    }
}