using ElevatorChallenge.Core;
using ElevatorChallenge.Core.Interfaces;
using AutoFixture;
using NSubstitute;
using Xunit;

namespace ElevatorChallenge.Tests
{
    public class BuildingTests
    {
        private readonly IElevator _elevator;
        private readonly Fixture _fixture;

        public BuildingTests()
        {
            _elevator = Substitute.For<IElevator>();
            _fixture = new Fixture();
        }

        [Fact]
        public void AddElevator_ValidElevator_AddsElevatorToBuilding()
        {
            // Add an elevator and check it exists in the building
            var numberOfFloors = _fixture.Create<int>() % 100 + 1;
            var building = new Building(numberOfFloors);

            building.AddElevator(_elevator);

            Assert.Contains(_elevator, building.GetElevators());
        }

        [Fact]
        public void Constructor_NonPositiveNumberOfFloors_ThrowsException()
        {
            // Building floors must be positive
            Assert.Throws<ArgumentException>(() => new Building(0));
        }

        [Fact]
        public void GetNumberOfFloors_ReturnsCorrectNumberOfFloors()
        {
            // Check floors returned match what was set
            var numberOfFloors = _fixture.Create<int>() % 100 + 1;
            var building = new Building(numberOfFloors);

            var result = building.GetNumberOfFloors();

            Assert.Equal(numberOfFloors, result);
        }
    }
}
