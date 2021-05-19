using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FunctionalCsharp.Tests
{
    public class EnumerableExtensionsTests
    {
        [Theory]
        [InlineData("")]
        [InlineData("foo")]
        [InlineData("bar")]
        [InlineData("blahblahblah")]
        public void SingletonReturnsOneItem(string value)
        {
            var enumerable = Enumerable.Singleton(value);

            Assert.True(enumerable.Count() == 1);
        }

        [Fact]
        public void OrEmptyIfNullIsNotNull()
        {
            IEnumerable<int> list = null;
            
            var enumerable = list.OrEmptyIfNull();

            Assert.NotNull(enumerable);
        }

        [Fact]
        public void OrEmptyIfNullIsEmpty()
        {
            IEnumerable<int> list = null;
            
            var enumerable = list.OrEmptyIfNull();

            Assert.Empty(enumerable);
        }
    }
}