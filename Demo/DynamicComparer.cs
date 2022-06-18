using Dynamitey;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Demo
{
    /// <summary>
    /// Compare two dynamic instances
    /// </summary>
    public class DynamicComparer : IEqualityComparer<object>
    {
        /// <summary>
        /// Compare two dynamic instances
        /// </summary>
        /// <param name="x">First object</param>
        /// <param name="y">Second object</param>
        /// <returns>True - if object is equal otherwise false</returns>
        public new bool Equals(object? x, object? y)
        {
            IEnumerable<string> listFields = Dynamic.GetMemberNames(x);

            foreach (var memberName in listFields)
            {
                var a = Dynamic.InvokeGet(x, memberName);
                var b = Dynamic.InvokeGet(y, memberName);
                if (!a.Equals(b))
                    return false;
            }
            return true;
        }
        /// <summary>
        /// Get hashcode of instance
        /// </summary>
        /// <param name="obj">Instance</param>
        /// <returns>Hashcode of instance</returns>
        public int GetHashCode([DisallowNull] object obj)
        {
            return obj.GetHashCode();
        }
    }
}