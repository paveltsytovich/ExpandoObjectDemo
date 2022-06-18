using Dynamitey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Demo
{
    /// <summary>
    /// Facade for compare two lists with dynamic instances
    /// </summary>
    public static class AssertDynamic
    {
        /// <summary>
        /// Compare two lists with dynamic instances
        /// </summary>
        /// <param name="expected">Expected list</param>
        /// <param name="actual">Actual list</param>
        public static void Equal(IEnumerable<dynamic> expected, IEnumerable<dynamic> actual)
        {
           Assert.Equal(expected, actual,new DynamicEnumerableComparer());                  
        }       
    }
}
