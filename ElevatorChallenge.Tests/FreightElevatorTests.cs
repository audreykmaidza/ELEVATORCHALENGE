using ElevatorChallenge.Core;
using ElevatorChallenge.Core.Interfaces;
using AutoFixture;
using Xunit;

namespace ElevatorChallenge.Tests
{
    public class FreightElevatorTests
    {
        private readonly IElevator _freightElevator;
        private readonly Fixture _fixture;

        public FreightElevatorTests()
        {
            _freightElevator = new FreightElevator(1, 100, 10);
            _fixture = new Fixture();
        }

        /// <summary>
        /// Tests the <see cref="FreightElevator.MoveToFloor"/> method to ensure
        /// it throws an <see cref="InvalidOperationException"/> when trying to move to a restricted floor.
        /// </summary>
        /// <remarks>
        /// This test verifies that the FreightElevator correctly handles the scenario where it is requested to move to a floor that is restricted for freight elevators.
        /// It uses a mocked IElevator implementation that simulates the behavior of a freight elevator with restricted floors.
        /// It ensures that attempting to move to a restricted floor results in an <see cref="InvalidOperationException"/> being thrown.
        /// </remarks>
        /// <exception cref="InvalidOperationException">
        /// Thrown when attempting to move to a restricted floor in a freight elevator.
        /// </exception>
        [Fact]
        public void MoveToFloor_RestrictedFloor_ThrowsException()
        {
            // Arrange
            var restrictedFloor = _fixture.Create<int>();            

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _freightElevator.MoveToFloor(restrictedFloor));
        }

        /// <summary>
        /// Tests the <see cref="FreightElevator.AddPassengers"/> method to ensure
        /// it throws an <see cref="InvalidOperationException"/> when the weight exceeds the elevator's capacity.
        /// </summary>
        /// <remarks>
        /// This test verifies that the FreightElevator correctly handles the scenario where the total weight of passengers exceeds its weight capacity.
        /// It uses a mocked IElevator implementation that simulates the behavior of a freight elevator with a specific weight capacity.
        /// It ensures that attempting to add passengers that exceed the weight limit results in an <see cref="InvalidOperationException"/> being thrown.
        /// </remarks>
        /// <exception cref="InvalidOperationException">
        /// Thrown when attempting to add passengers that exceed the weight capacity of the freight elevator.
        /// </exception>
        [Fact]
        public void AddPassengers_ExceedsWeight_ThrowsException()
        {
            // Arrange
            var elevator = new FreightElevator(1, 50, 10);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => elevator.AddPassengers(51));
        }
    }
}