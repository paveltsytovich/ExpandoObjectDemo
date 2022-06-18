using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Demo
{
    /// <summary>
    /// Dynamic Enumerable comparer
    /// </summary>
    public class DynamicEnumerableComparer : IEqualityComparer<IEnumerable<object>>
    {
        /// <summary>
        /// Compare two lists
        /// </summary>
        /// <param name="x">First list for compare</param>
        /// <param name="y">Second list for compare</param>
        /// <returns>True if lists is equal otherwise false</returns>
        public bool Equals(IEnumerable<object>? x, IEnumerable<object>? y)
        {
            if(x == null && y == null)
                return false;
            else
                return Enumerable.SequenceEqual(x, y, new DynamicComparer());            
        }
        /// <summary>
        /// Get hash code
        /// </summary>
        /// <param name="source">Instance for hashcode</param>
        /// <returns>Hashcode instance</returns>
        public int GetHashCode([DisallowNull] IEnumerable<object> source)
        {
            return source.Sum(x => x.GetHashCode());
        }        
    }
}
