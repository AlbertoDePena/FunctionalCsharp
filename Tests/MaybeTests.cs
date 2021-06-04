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

        [Fact]
        public void ToMaybeForDifferentDataTypes()
        {
            string nullString = null;

            Assert.True(nullString.ToMaybe(string.IsNullOrWhiteSpace).IsNothing);
            Assert.True("".ToMaybe(string.IsNullOrWhiteSpace).IsNothing);
            Assert.True("Functional".ToMaybe(string.IsNullOrWhiteSpace).IsSomething);

            int? nullInt = null;

            Assert.True(nullInt.ToMaybe().IsNothing);
            Assert.True(new Nullable<int>(1).ToMaybe().IsSomething);

            double? nullDouble = null;

            Assert.True(nullDouble.ToMaybe().IsNothing);
            Assert.True(new Nullable<double>(1).ToMaybe().IsSomething);

            DateTime? nullDateTime = null;

            Assert.True(nullDateTime.ToMaybe().IsNothing);
            Assert.True(new Nullable<DateTime>(DateTime.Now).ToMaybe().IsSomething);

            DateTimeOffset? nullDateTimeOffset = null;

            Assert.True(nullDateTimeOffset.ToMaybe().IsNothing);
            Assert.True(new Nullable<DateTimeOffset>(DateTime.Now).ToMaybe().IsSomething);

            bool? nullBool = null;

            Assert.True(nullBool.ToMaybe().IsNothing);
            Assert.True(new Nullable<bool>(true).ToMaybe().IsSomething);
            Assert.True(new Nullable<bool>(false).ToMaybe().IsSomething);

            SomeRecord nullRecord = null;
            SomeRecord record = new SomeRecord();

            Assert.True(nullRecord.ToMaybe().IsNothing);
            Assert.True(record.ToMaybe().IsSomething);
        }

        [Fact]
        public void SelectMany()
        {
            var x = Maybe<int>.Something(1);
            var y = Maybe<int>.Something(1);

            var z =
                from a in x
                from b in y
                select a + b;

            Assert.Equal(2, z.Match(0, x => x));
        }
    }

    public class SomeRecord {}
}
