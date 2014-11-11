﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dijkstra
{
    /// <summary>
    /// Calculates the best route between various paths, using Dijkstra's algorithm
    /// </summary>
    /// <remarks>
    /// Copied the algorithm's implementation from <see cref="http://www.codeproject.com/Articles/22647/Dijkstra-Shortest-Route-Calculation-Object-Oriente"/>.
    /// Implementation was adjusted to support Generics, and make heavier use of LINQ
    /// </remarks>
    public static class Engine
    {
        public static LinkedList<Path<T>> CalculateShortestPathBetween<T>(T source, T destination, IEnumerable<Path<T>> paths)
        {
            return CalculateFrom(source, paths)[destination];
        }
        public static Dictionary<T, LinkedList<Path<T>>> CalculateShortestFrom<T>(T source, IEnumerable<Path<T>> paths)
        {
            return CalculateFrom(source, paths);
        }

        private static Dictionary<T, LinkedList<Path<T>>> CalculateFrom<T>(T source, IEnumerable<Path<T>> paths)
        {
            // validate the paths
            if (paths.Any(p => p.Source.Equals(p.Destination)))
                throw new ArgumentException("No path can have the same source and destination");



            // keep track of the shortest paths identified thus far
            Dictionary<T, KeyValuePair<double, LinkedList<Path<T>>>> shortestPaths = new Dictionary<T, KeyValuePair<double, LinkedList<Path<T>>>>();
            // keep track of the locations which have been completely processed
            List<T> locationsProcessed = new List<T>();

            // include all possible steps, with Int.MaxValue cost
            paths.SelectMany(p => new T[] { p.Source, p.Destination })              // union source and destinations
                    .Distinct()                                                        // remove duplicates
                    .ToList()                                                          // ToList exposes ForEach
                    .ForEach(s => shortestPaths.Set(s, Double.MaxValue, null));         // add to ShortestPaths with MaxValue cost

            // update cost for self-to-self as 0; no path
            shortestPaths.Set(source, 0, null);

            // keep this cached
            var locationCount = shortestPaths.Keys.Count;

            while (locationsProcessed.Count < locationCount)
            {

                T _locationToProcess = default(T);

                //Search for the nearest location that isn't handled
                foreach (T _location in shortestPaths.OrderBy(p => p.Value.Key).Select(p => p.Key).ToList())
                {
                    if (!locationsProcessed.Contains(_location))
                    {
                        if (shortestPaths[_location].Key == Double.MaxValue)
                            return shortestPaths.ToDictionary(k => k.Key, v => v.Value.Value); //ShortestPaths[destination].Value;

                        _locationToProcess = _location;
                        break;
                    }
                } // foreach

                var _selectedPaths = paths.Where(p => p.Source.Equals(_locationToProcess));
                foreach (Path<T> path in _selectedPaths)
                {
                    if (shortestPaths[path.Destination].Key > path.Cost + shortestPaths[path.Source].Key)
                    {
                        shortestPaths.Set(
                            path.Destination,
                            path.Cost + shortestPaths[path.Source].Key,
                            shortestPaths[path.Source].Value.Union(new Path<T>[] { path }).ToArray());
                    }
                } // foreach

                //Add the location to the list of processed locations
                locationsProcessed.Add(_locationToProcess);

            } // while

            return shortestPaths.ToDictionary(k => k.Key, v => v.Value.Value);
            //return ShortestPaths[destination].Value;
        }
    }
}