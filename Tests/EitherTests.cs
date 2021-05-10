using System;
using System.Threading.Tasks;
using Xunit;

namespace FunctionalCsharp.Tests
{
    public class EitherTests
    {
        [Fact]
        public void TestOne()
        {
            var either = Either<int, string>.Success(10);

            var result = either.Match(success => success.ToString(), error => error);

            Assert.True(either.IsSuccess);
            Assert.Equal("10", result);
        }

        [Fact]
        public void TestTwo()
        {
            var either = Either<int, string>.Error("Not a 10");

            var result = either.Match(success => success.ToString(), error => error);

            Assert.True(either.IsError);
            Assert.Equal("Not a 10", result);
        }

        [Fact]
        public void TestThree()
        {
            int AddTwo(int x) => x + 2;

            int Square(int x) => x * x;

            var either = Either<int, string>.Success(1);

            either = either.Map(AddTwo).Map(Square);

            var result = either.Match(success => success, error => 0);

            Assert.Equal(9, result);
        }

        [Fact]
        public void TestFour()
        {
            int AddTwo(int x) => x + 2;

            Either<int, string> Square(int x) => Either<int, string>.Success(x * x);

            var either = Either<int, string>.Success(1);

            either = either.Map(AddTwo).Bind(Square);

            var result = either.Match(success => success, error => 0);

            Assert.Equal(9, result);
        }

        [Fact]
        public void TestFive()
        {
            int AddTwo(int x) => x + 2;

            Either<int, string> Square(int x) => Either<int, string>.Success(x * x);

            var either = Either<int, string>.Error("Failure");

            either = either.Map(AddTwo).Bind(Square);

            var result = either.Match(success => "OK", error => error);

            Assert.Equal("Failure", result);
        }

        [Fact]
        public void TestSix()
        {
            int AddTwo(int x) => x + 2;

            Either<int, string> Square(int x) => Either<int, string>.Success(x * x);

            var either = Either<int, string>.Success(1);

            either = either.Map(AddTwo).Bind(Square).Bind(_ => Either<int, string>.Error("WHAT?"));

            var result = either.Match(success => "OK", error => error);

            Assert.Equal("WHAT?", result);
        }

        [Fact]
        public void TestSeven()
        {
            int AddTwo(int x) => x + 2;

            Either<int, string> Square(int x) => Either<int, string>.Success(x * x);

            var either = Either<int, string>.Error("oh-oh");

            either = either.Map(AddTwo).Bind(Square).MapError(_ => "WHAT?");

            var result = either.Match(success => "OK", error => error);

            Assert.Equal("WHAT?", result);
        }

        [Fact]
        public async Task UpdateUserRecord()
        {
            User SomeLogic(User user) => user;

            var userStore = new UserStore();

            var result =
                await userStore.FindUser(0).Map(SomeLogic);

        }
    }
}
