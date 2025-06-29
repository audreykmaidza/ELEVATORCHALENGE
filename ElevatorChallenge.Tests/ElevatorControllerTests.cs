using ElevatorChallenge.Application;
using ElevatorChallenge.Core;
using ElevatorChallenge.Core.Interfaces;
using AutoFixture;
using NSubstitute;
using Xunit;

namespace ElevatorChallenge.Tests
{
    public class ElevatorControllerTests
    {
        private readonly IElevatorController _controller;
        private readonly IElevatorDispatcher _dispatcher;
        private readonly IBuilding _building;
        private readonly IElevator _elevator;
        private readonly Fixture _fixture;

        public ElevatorControllerTests()
        {
            _dispatcher = Substitute.For<IElevatorDispatcher>();
            _building = Substitute.For<IBuilding>();
            _controller = new ElevatorController(_dispatcher, _building);
            _elevator = Substitute.For<IElevator>();
            _fixture = new Fixture();
        }

        [Fact]
        public void RequestElevator_ValidRequest_DispatchesElevator()
        {
            // Should dispatch elevator and move it to floor with passengers
            _building.GetNumberOfFloors().Returns(10);
            _dispatcher.DispatchElevator(Arg.Any<FloorRequest>()).Returns(_elevator);

            _controller.RequestElevator(3, 2, "Up");

            _elevator.Received().MoveToFloor(3);
            _elevator.Received().AddPassengers(2);
        }

        [Fact]
        public void RequestElevator_InvalidFloor_ThrowsException()
        {
            // Should throw if floor requested is out of building range
            _building.GetNumberOfFloors().Returns(10);

            Assert.Throws<ArgumentException>(() => _controller.RequestElevator(11, 2, "Up"));
        }

        [Fact]
        public void GetElevatorStatus_InvalidId_ThrowsException()
        {
            // Should throw if elevator ID is invalid
            int invalidId = -Math.Abs(_fixture.Create<int>());

            Assert.Throws<InvalidOperationException>(() => _controller.GetElevatorStatus(invalidId));
        }

        [Fact]
        public void GetElevatorStatus_ValidId_ReturnsStatus()
        {
            // Should return status for a valid elevator ID
            var elevatorId = _fixture.Create<int>();
            _elevator.Id.Returns(elevatorId);

            var expectedStatus = _fixture.Create<ElevatorStatus>();
            _elevator.GetStatus().Returns(expectedStatus);
            _building.AddElevator(_elevator);
            _building.GetElevatorById(elevatorId).Returns(_elevator);

            var status = _controller.GetElevatorStatus(elevatorId);

            Assert.Equal(expectedStatus, status);
        }

        [Fact]
        public void RequestElevator_NegativePassengers_ThrowsException()
        {
            // Should throw if passenger count is negative
            _building.GetNumberOfFloors().Returns(10);
            _dispatcher.DispatchElevator(Arg.Any<FloorRequest>()).Returns(_elevator);

            Assert.Throws<ArgumentException>(() => _controller.RequestElevator(3, -1, "Up"));
        }
    }
}
