using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

using Namrly.Services;
using Moq;
using FluentAssertions;

namespace Namrly.Services.Tests
{
    public class NamrlyServiceTests
    {
        [Fact]
        public async void GetRandomName_GetsRandomName()
        {
            // arrange
            var mock = new Mock<IRandomWordService>();
            mock.Setup(m => m.GetRandomWords(1)).ReturnsAsync(new List<string>() { "RandomWord1" });

            INamrlyService namrlyService = new NamrlyService(mock.Object);

            // act
            var word = await namrlyService.GetRandomName();

            // assert
            word.Should().NotBe(null);
            word.Should().NotBe("RandomWord1");
        }
    }
}
