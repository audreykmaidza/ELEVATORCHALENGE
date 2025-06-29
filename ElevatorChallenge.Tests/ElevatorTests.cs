using ElevatorChallenge.Core;
using ElevatorChallenge.Core.Interfaces;
using AutoFixture;
using Xunit;

namespace ElevatorChallenge.Tests
{
    public class ElevatorTests
    {
        private readonly Fixture _fixture;

        public ElevatorTests()
        {
            _fixture = new Fixture();

            // Customize AutoFixture to create IElevator instances by constructing Elevator objects
            // with positive capacity and max floors (to avoid invalid test data).
            _fixture.Customize<IElevator>(c => c.FromFactory(() => 
                new Elevator(
                    _fixture.Create<int>(), 
                    Math.Abs(_fixture.Create<int>()) + 1, 
                    Math.Abs(_fixture.Create<int>()) + 1
                )));
        }

        [Fact]
        public void Constructor_ValidParameters_SetsProperties()
        {
            // Arrange - generate valid parameters for the constructor
            var id = _fixture.Create<int>();
            var capacity = _fixture.Create<int>() % 100 + 1; // Ensure capacity is positive and reasonable
            var maxFloors = _fixture.Create<int>() % 50 + 1; // Ensure maxFloors is positive and reasonable

            // Act - instantiate an Elevator with valid parameters
            var elevator = new Elevator(id, capacity, maxFloors);

            // Assert - verify the properties and initial state are correctly set
            Assert.Equal(id, elevator.Id);
            Assert.Equal(nameof(Elevator), elevator.ElevatorType);

            var status = elevator.GetStatus();

            // Elevator should start on floor 1, with no direction and no passengers, and not moving
            Assert.Equal(1, status.CurrentFloor);
            Assert.Equal("None", status.Direction);
            Assert.False(status.IsMoving);
            Assert.Equal(0, status.PassengerCount);
        }

        [Fact]
        public void Constructor_NonPositiveCapacity_ThrowsArgumentException()
        {
            // Arrange - create invalid capacity (zero or negative)
            var id = _fixture.Create<int>();
            var maxFloors = _fixture.Create<int>() % 50 + 1; // valid max floors
            var invalidCapcity = _fixture.Create<int>() % 10 * -1; // negative or zero capacity

            // Act & Assert - constructor should throw exception for invalid capacity
            var exception = Assert.Throws<ArgumentException>(() => new Elevator(id, invalidCapcity, maxFloors));
            Assert.Equal("Capacity must be a positive integer.", exception.Message);
        }

        [Fact]
        public void Constructor_NonPositiveMaxFloors_ThrowsArgumentException()
        {
            // Arrange - create invalid max floors (zero or negative)
            var id = _fixture.Create<int>();
            var capacity = _fixture.Create<int>() % 100 + 1; // valid capacity
            var invalidMaxFloors = _fixture.Create<int>() % 10 * -1; // negative or zero max floors

            // Act & Assert - constructor should throw exception for invalid max floors
            var exception = Assert.Throws<ArgumentException>(() => new Elevator(id, capacity, invalidMaxFloors));
            Assert.Equal("Max floors must be a positive integer.", exception.Message);
        }

        [Fact]
        public void MoveToFloor_ValidFloorAboveCurrent_SetsFloorAndDirectionUp()
        {
            // Arrange - create elevator and pick a floor strictly above the current floor (initially 1)
            var elevator = _fixture.Create<Elevator>();
            var status = elevator.GetStatus();

            // Simplify to target a floor 2 above current floor (to ensure valid upward movement)
            var targetFloor = status.CurrentFloor + 2;

            // Act - move elevator to the higher floor
            elevator.MoveToFloor(targetFloor);

            // Assert - elevator floor and direction reflect the upward movement
            status = elevator.GetStatus();
            Assert.Equal(targetFloor, status.CurrentFloor);
            Assert.Equal("Up", status.Direction);
            Assert.False(status.IsMoving); // Elevator should have completed movement
        }

        [Fact]
        public void MoveToFloor_ValidFloorBelowCurrent_SetsFloorAndDirectionDown()
        {
            // Arrange - move elevator first to a known floor above 1
            var elevator = _fixture.Create<Elevator>();
            elevator.MoveToFloor(5); // move up to floor 5

            // Pick a floor below current floor (between 1 and 4)
            var targetFloor = _fixture.Create<int>() % 4 + 1;

            // Act - move elevator down
            elevator.MoveToFloor(targetFloor);

            // Assert - floor and direction indicate downward movement
            var status = elevator.GetStatus();
            Assert.Equal(targetFloor, status.CurrentFloor);
            Assert.Equal("Down", status.Direction);
            Assert.False(status.IsMoving);
        }

        [Fact]
        public void MoveToFloor_SameFloor_SetsDirectionNone()
        {
            // Arrange - get current floor
            var elevator = _fixture.Create<IElevator>();
            var status = elevator.GetStatus();
            var currentFloor = status.CurrentFloor;

            // Act - move elevator to the same floor
            elevator.MoveToFloor(currentFloor);

            // Assert - direction should be 'None' and floor unchanged
            status = elevator.GetStatus();
            Assert.Equal(currentFloor, status.CurrentFloor);
            Assert.Equal("None", status.Direction);
            Assert.False(status.IsMoving);
        }

        [Fact]
        public void MoveToFloor_FloorBelowOne_ThrowsArgumentException()
        {
            // Arrange - invalid floor below minimum (floor 1)
            var elevator = _fixture.Create<Elevator>();
            var invalidFloor = 0;

            // Act & Assert - moving to invalid floor should throw exception
            var exception = Assert.Throws<ArgumentException>(() => elevator.MoveToFloor(invalidFloor));
            Assert.Contains("Floor must be between 1 and", exception.Message);
        }

        [Fact]
        public void MoveToFloor_FloorAboveMaxFloors_ThrowsArgumentException()
        {
            // Arrange - create elevator with known max floors
            var maxFloors = _fixture.Create<int>() % 50 + 1; // ensure positive max floors
            var elevator = new Elevator(_fixture.Create<int>(), _fixture.Create<int>() % 100 + 1, maxFloors);
            var invalidFloor = maxFloors + 1; // floor beyond max floors

            // Act & Assert - should throw when moving beyond max floors
            var exception = Assert.Throws<ArgumentException>(() => elevator.MoveToFloor(invalidFloor));
            Assert.Contains($"Floor must be between 1 and {maxFloors}", exception.Message);
        }

        [Fact]
        public void AddPassengers_ValidCountWithCapacity_IncreasesPassengerCount()
        {
            // Arrange - create elevator with known capacity
            var capacity = _fixture.Create<int>() % 100 + 1;
            var elevator = new Elevator(_fixture.Create<int>(), capacity, _fixture.Create<int>() % 50 + 1);
            var passengersToAdd = _fixture.Create<int>() % (capacity / 2) + 1; // less than capacity

            // Act - add passengers
            elevator.AddPassengers(passengersToAdd);

            // Assert - passenger count reflects addition
            var status = elevator.GetStatus();
            Assert.Equal(passengersToAdd, status.PassengerCount);
        }

        [Fact]
        public void AddPassengers_ExceedingPassengerCapacity_ThrowsInvalidOperationException()
        {
            // Arrange - elevator with capacity
            var capacity = _fixture.Create<int>() % 100 + 1;
            var elevator = new Elevator(_fixture.Create<int>(), capacity, _fixture.Create<int>() % 50 + 1);
            var passengersToAdd = capacity + 1; // exceeding capacity

            // Act & Assert - adding too many passengers throws exception
            var exception = Assert.Throws<InvalidOperationException>(() => elevator.AddPassengers(passengersToAdd));
            Assert.Equal("Cannot add passengers: exceeds capacity.", exception.Message);
        }

        [Fact]
        public void AddPassengers_NegativeCount_ThrowsArgumentException()
        {
            // Arrange - elevator and negative passenger count
            var elevator = _fixture.Create<Elevator>();
            var negativeCount = _fixture.Create<int>() % 10 * -1 - 1;

            // Act & Assert - negative count throws exception
            var exception = Assert.Throws<ArgumentException>(() => elevator.AddPassengers(negativeCount));
            Assert.Equal("Passenger count must be positive.", exception.Message);
        }

        [Fact]
        public void RemovePassengers_ValidPassengerCountWithinCurrent_DecreasesPassengerCount()
        {
            // Arrange - create elevator with capacity and add some passengers
            var capacity = _fixture.Create<int>() % 100 + 1;
            var elevator = new Elevator(_fixture.Create<int>(), capacity, _fixture.Create<int>() % 50 + 1);
            var initialPassengers = _fixture.Create<int>() % (capacity / 2) + 1;

            elevator.AddPassengers(initialPassengers);

            var passengersToRemove = _fixture.Create<int>() % initialPassengers + 1; // less than or equal to current passengers

            // Act - remove passengers
            elevator.RemovePassengers(passengersToRemove);

            // Assert - passenger count decreased accordingly
            var status = elevator.GetStatus();
            Assert.Equal(initialPassengers - passengersToRemove, status.PassengerCount);
        }

        [Fact]
        public void RemovePassengers_MoreThanCurrentPassengers_ThrowsInvalidOperationException()
        {
            // Arrange - elevator with some passengers
            var elevator = _fixture.Create<Elevator>();
            var initialPassengers = _fixture.Create<int>() % 5 + 1;

            elevator.AddPassengers(initialPassengers);

            var passengersToRemove = initialPassengers + 1; // more than current count

            // Act & Assert - removing more passengers than available throws
            var exception = Assert.Throws<InvalidOperationException>(() => elevator.RemovePassengers(passengersToRemove));
            Assert.Equal("Cannot remove passengers: not enough passengers.", exception.Message);
        }

        [Fact]
        public void RemovePassengers_NegativeCount_ThrowsArgumentException()
        {
            // Arrange - elevator and negative removal count
            var elevator = _fixture.Create<Elevator>();
            var negativeCount = _fixture.Create<int>() % 10 * -1 - 1;

            // Act & Assert - negative removal throws exception
            var exception = Assert.Throws<ArgumentException>(() => elevator.RemovePassengers(negativeCount));
            Assert.Equal("Passenger count must be positive.", exception.Message);
        }

        [Fact]
        public void GetStatus_AfterMultipleOperations_ReturnsCorrectStatus()
        {
            // Arrange - elevator with known parameters
            var capacity = _fixture.Create<int>() % 100 + 1;
            var maxFloors = _fixture.Create<int>() % 50 + 1;
            var elevator = new Elevator(_fixture.Create<int>(), capacity, maxFloors);

            var targetFloor = _fixture.Create<int>() % (maxFloors - 1) + 2; // floor above current floor (1)
            var passengersToAdd = _fixture.Create<int>() % (capacity / 2) + 1;

            // Perform actions: move and add passengers
            elevator.MoveToFloor(targetFloor);
            elevator.AddPassengers(passengersToAdd);

            // Act - get status after operations
            var status = elevator.GetStatus();

            // Assert - verify status reflects all operations correctly
            Assert.Equal(targetFloor, status.CurrentFloor);
            Assert.Equal("Up", status.Direction);
            Assert.False(status.IsMoving);
            Assert.Equal(passengersToAdd, status.PassengerCount);
        }

        [Fact]
        public void GetStatus_InitialState_ReturnsDefaultStatus()
        {
            // Arrange - create elevator (initial state)

            var elevator = _fixture.Create<Elevator>();

            // Act - get status immediately after creation
            var status = elevator.GetStatus();

            // Assert - default values should be:
            // floor 1, no direction, not moving, zero passengers
            Assert.Equal(1, status.CurrentFloor);
            Assert.Equal("None", status.Direction);
            Assert.False(status.IsMoving);
            Assert.Equal(0, status.PassengerCount);
        }
    }
}
