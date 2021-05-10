using System;
using Xunit;

namespace FunctionalCsharp.Tests
{
    public class MaybeTests
    {
        [Fact]
        public void TestOne()
        {
            var maybe = Maybe<int>.Nothing();

            Assert.True(maybe.IsNothing);
        }

        [Fact]
        public void TestTwo()
        {
            var maybe = Maybe<int>.Something(10);

            Assert.True(maybe.IsSomething);
        }
    }
}
