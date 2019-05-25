using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Moq;
using FluentAssertions;
using Namrly.Services;

namespace Namrly.Services.Tests
{
    public class NamrlyServiceTests
    {
        [Fact]
        public async void GetRandomNames_GetsRandomNames()
        {
            // arrange
            var mock = new Mock<IRandomWordService>();
            mock.Setup(m => m.GetRandomWords(1)).ReturnsAsync(new List<string>() { "RandomWord1" });

            INamrlyService namrlyService = new NamrlyService(mock.Object);

            // act
            var words = await namrlyService.GetRandomStartupNames();

            // assert
            words.Should().NotBeNull();
            words.Should().BeOfType<List<string>>();
            words.Should().NotContain("RandomWord1");
            words.Count.Should().Be(1);
        }

        [Fact]
        public async void GetRandomNames_GetsSpecifiedNumberNames()
        {
            // arrange
            var mock = new Mock<IRandomWordService>();
            mock.Setup(m => m.GetRandomWords(2))
            .ReturnsAsync(new List<string>() { "RandomWord1", "RandomWord2"});

            INamrlyService namrlyService = new NamrlyService(mock.Object);

            // act
            var words = await namrlyService.GetRandomStartupNames(2);

            // assert
            words.Should().NotBeNull();
            words.Should().BeOfType<List<string>>();
            words.Count.Should().Be(2);
        }

        [Fact]
        public async void GetRelatedName_GetsRelatedName()
        {
            // arrange
            var mock = new Mock<IRandomWordService>();
            mock
            .Setup(m => m.GetSynonyms("biscuit"))
            .ReturnsAsync(new List<string>() {"cracker", "bun", "pretzel", "cookie"});
            
            INamrlyService namrlyService = new NamrlyService(mock.Object);

            // act
            var words = await namrlyService.GetRelatedStartupNames("biscuit");

            // assert
            words.Should().NotBeNull();
            words.Should().BeOfType<List<string>>();
            words.Count.Should().Be(1);
            words.Should().NotContain("biscuits");
        }
    }
}
