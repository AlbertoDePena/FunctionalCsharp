using System;
using Xunit;

namespace FunctionalCsharp.Tests
{
    public class MaybeTests
    {
        [Fact]
        public void NothingIsNothing()
        {
            var maybe = Maybe<int>.Nothing();

            Assert.True(maybe.IsNothing);
        }

        [Fact]
        public void SomethingIsSomething()
        {
            var maybe = Maybe<int>.Something(1);

            Assert.True(maybe.IsSomething);
        }

        [Fact]
        public void EmptyMaybeObeysFirstFunctorLaw()
        {
            Func<string, string> id = x => x;
            var maybe = Maybe<string>.Nothing();

            Assert.Equal(maybe, maybe.Map(id));
        }

        [Theory]
        [InlineData("")]
        [InlineData("foo")]
        [InlineData("bar")]
        [InlineData("corge")]
        [InlineData("antidisestablishmentarianism")]
        public void PopulatedMaybeObeysFirstFunctorLaw(string value)
        {
            Func<string, string> id = x => x;
            var maybe = Maybe<string>.Something(value);

            Assert.Equal(maybe, maybe.Map(id));
        }

        [Fact]
        public void EmptyMaybeObeysSecondFunctorLaw()
        {
            Func<string, int> g = s => s.Length;
            Func<int, bool> f = i => i % 2 == 0;
            var maybe = Maybe<string>.Nothing();

            Assert.Equal(maybe.Map(g).Map(f), maybe.Map(s => f(g(s))));
        }

        [Theory]
        [InlineData("", true)]
        [InlineData("foo", false)]
        [InlineData("bar", false)]
        [InlineData("corge", false)]
        [InlineData("antidisestablishmentarianism", true)]
        public void PopulatedMaybeObeysSecondFunctorLaw(string value, bool expected)
        {
            Func<string, int> g = s => s.Length;
            Func<int, bool> f = i => i % 2 == 0;
            var maybe = Maybe<string>.Something(value);

            Assert.Equal(maybe.Map(g).Map(f), maybe.Map(s => f(g(s))));
        }
    }
}
