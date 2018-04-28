using System;
using Xunit;
using ships_api.Services;

namespace ships_api_tests
{
    public class NavigationServiceTests
    {
        [Fact]
        public void CanNavigateForward()
        {
            var sut = new NavigationService(5, 3);
            var response = sut.ProcessShipInstructions("1 1 E", "F");

            Assert.Equal("2 1 E", response);
        }

        [Fact]
        public void CanNavigateLeft()
        {
            var sut = new NavigationService(5, 3);
            var response = sut.ProcessShipInstructions("1 1 E", "L");

            Assert.Equal("1 1 N", response);
        }

        [Fact]
        public void CanNavigateRight()
        {
            var sut = new NavigationService(5, 3);
            var response = sut.ProcessShipInstructions("1 1 E", "R");

            Assert.Equal("1 1 S", response);
        }

        [Fact]
        public void CanNavigateFirstFullCycle()
        {
            var sut = new NavigationService(5, 3);
            var response = sut.ProcessShipInstructions("1 1 E", "RFRFRFRF");

            Assert.Equal("1 1 E", response);
        }
    }
}
