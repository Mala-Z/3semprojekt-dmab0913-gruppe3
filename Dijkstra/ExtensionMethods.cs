using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dijkstra
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Adds or Updates the dictionary to include the destination and its associated cost and complete path (and param arrays make paths easier to work with)
        /// </summary>
        public static void Set<T>(this Dictionary<T, KeyValuePair<double, LinkedList<Path<T>>>> Dictionary, T destination, double Cost, params Path<T>[] paths)
        {
            var CompletePath = paths == null ? new LinkedList<Path<T>>() : new LinkedList<Path<T>>(paths);
            Dictionary[destination] = new KeyValuePair<double, LinkedList<Path<T>>>(Cost, CompletePath);
        }
    }
}
