using System;
using System.Threading.Tasks;
using Xunit;

namespace FunctionalCsharp.Tests
{
    public class TaskExtensionsTests
    {
        [Theory]
        [InlineData("")]
        [InlineData("foo")]
        [InlineData("bar")]
        [InlineData("blahblahblah")]
        public async Task PopulatedTaskObeysFirstFunctorLaw(string value)
        {
            Func<string, string> id = x => x;
            var task = Task.FromResult(value);

            Assert.Equal(await task, await task.Select(id));
        }

        [Theory]
        [InlineData("")]
        [InlineData("foo")]
        [InlineData("bar")]
        [InlineData("blahblahblah")]
        public async Task PopulatedTaskObeysSecondFunctorLaw(string value)
        {
            Func<string, int> g = s => s.Length;
            Func<int, bool> f = i => i % 2 == 0;
            var task = Task.FromResult(value);

            Assert.Equal(await task.Select(g).Select(f), await task.Select(s => f(g(s))));
        }
    }
}