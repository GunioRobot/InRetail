using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Extensions;

namespace Tests.InRetail.Procurement.AssertHelpers
{
    public static class EnumerableEx
    {
        public static void ShouldContainEqualElements<T>(this IEnumerable<T> actual, IEnumerable<T> expected)
        {
            Assert.NotNull(actual);
            Assert.Equal(expected.Count(), actual.Count());
            foreach (var t in actual)
            {
                expected.ShouldContain(t);
            }
        }
    }
}