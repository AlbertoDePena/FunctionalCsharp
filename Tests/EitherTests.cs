using System;
using System.Threading.Tasks;
using Xunit;

namespace FunctionalCsharp.Tests
{
    public class EitherTests
    {
        [Fact]
        public void SuccessIsSuccess()
        {
            Assert.True(Either<int, string>.Success(1).IsSuccess);
        }

        [Fact]
        public void ErrorIsError()
        {
            Assert.True(Either<int, string>.Error("oops").IsError);
        }
    }
}
