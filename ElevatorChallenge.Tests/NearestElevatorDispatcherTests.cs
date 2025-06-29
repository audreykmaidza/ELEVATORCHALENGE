using ElevatorChallenge.Core;
using ElevatorChallenge.Core.Interfaces;
using ElevatorChallenge.Application;
using AutoFixture;
using NSubstitute;
using Xunit;

namespace ElevatorChallenge.Tests
{
    /// <summary>
    /// Tests for NearestElevatorDispatcher functionality.
    /// </summary>
    public class NearestElevatorDispatcherTests
    {
        private readonly IFixture _fixture;
        private readonly IBuilding _building;
        private readonly IElevator _standardElevator;
        private readonly IElevator _freightElevator;
        private readonly NearestElevatorDispatcher _dispatcher;

        public NearestElevatorDispatcherTests()
        {
            _fixture = new Fixture();
            _building = Substitute.For<IBuilding>();
            _dispatcher = new NearestElevatorDispatcher(_building);
            _standardElevator = Substitute.For<IElevator>();
            _freightElevator = Substitute.For<IElevator>();
        }

        /// <summary>
        /// Throws when no elevators are available to dispatch.
        /// </summary>
        [Fact]
        public void DispatchElevator_NoElevators_ThrowsException()
        {
            _building.GetElevators().Returns(new List<IElevator>());
            var request = _fixture.Create<FloorRequest>();

            Assert.Throws<InvalidOperationException>(() => _dispatcher.DispatchElevator(request));
        }

        /// <summary>
        /// Prefers freight elevator for heavy loads.
        /// </summary>
        [Fact]
        public void DispatchElevator_HeavyLoad_PrefersFreight()
        {
            _standardElevator.ElevatorType.Returns(nameof(Elevator));
            _freightElevator.ElevatorType.Returns(nameof(FreightElevator));

            _standardElevator.GetStatus().Returns(new ElevatorStatus { CurrentFloor = 3 });
            _freightElevator.GetStatus().Returns(new ElevatorStatus { CurrentFloor = 5 });

            _building.GetElevators().Returns(new List<IElevator> { _standardElevator, _freightElevator });

            var request = new FloorRequest { Floor = 4, PassengerCount = 150 };

            var result = _dispatcher.DispatchElevator(request);

            Assert.Equal(_freightElevator, result);
        }
    }
}
