using System;
using Xunit;

namespace CSharp.Functional.Tests
{
    public class MaybeTests
    {
        [Fact]
        public void NothingIsNothing()
        {
            Assert.True(Maybe<int>.Nothing().IsNothing);
        }

        [Fact]
        public void SomethingIsSomething()
        {
            Assert.True(Maybe<int>.Something(1).IsSomething);
        }

        [Fact]
        public void NothingNotEqualsSomething()
        {
            var m1 = Maybe<int>.Nothing();
            var m2 = Maybe<int>.Something(1);

            Assert.NotEqual(m1, m2);
        }

        [Fact]
        public void SomethingEqualsSomething()
        {
            var m1 = Maybe<int>.Something(1);
            var m2 = Maybe<int>.Something(1);

            Assert.Equal(m1, m2);
        }

        [Fact]
        public void NothingEqualsNothing()
        {
            var m1 = Maybe<int>.Nothing();
            var m2 = Maybe<int>.Nothing();

            Assert.Equal(m1, m2);
        }

        [Fact]
        public void EmptyMaybeObeysFirstFunctorLaw()
        {
            Func<string, string> id = x => x;
            var maybe = Maybe<string>.Nothing();

            Assert.Equal(maybe, maybe.Select(id));
        }

        [Theory]
        [InlineData("")]
        [InlineData("foo")]
        [InlineData("bar")]
        [InlineData("blahblahblah")]
        public void PopulatedMaybeObeysFirstFunctorLaw(string value)
        {
            Func<string, string> id = x => x;
            var maybe = Maybe<string>.Something(value);

            Assert.Equal(maybe, maybe.Select(id));
        }

        [Fact]
        public void EmptyMaybeObeysSecondFunctorLaw()
        {
            Func<string, int> g = s => s.Length;
            Func<int, bool> f = i => i % 2 == 0;
            var maybe = Maybe<string>.Nothing();

            Assert.Equal(maybe.Select(g).Select(f), maybe.Select(s => f(g(s))));
        }

        [Theory]
        [InlineData("")]
        [InlineData("foo")]
        [InlineData("bar")]
        [InlineData("blahblahblah")]
        public void PopulatedMaybeObeysSecondFunctorLaw(string value)
        {
            Func<string, int> g = s => s.Length;
            Func<int, bool> f = i => i % 2 == 0;
            var maybe = Maybe<string>.Something(value);

            Assert.Equal(maybe.Select(g).Select(f), maybe.Select(s => f(g(s))));
        }
    }
}
