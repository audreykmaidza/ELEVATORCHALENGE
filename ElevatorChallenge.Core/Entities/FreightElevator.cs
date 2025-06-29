using ElevatorChallenge.Core.Interfaces;

namespace ElevatorChallenge.Core
{
    /// <summary>
    /// Represents a freight elevator that carries goods and has restricted floor access.
    /// </summary>
    public class FreightElevator : IElevator
    {
        private readonly int _id;
        private readonly int _weightCapacity;
        private readonly int _maxFloors;
        private readonly List<int> _restrictedFloors = new() { 5 }; // Example: freight elevator cannot access 5th floor
        private int _currentFloor = 1;
        private int _currentWeight;
        private string _direction = "None";
        private bool _isMoving;

        /// <summary>
        /// Initializes a new freight elevator.
        /// </summary>
        public FreightElevator(int id, int weightCapacity, int maxFloors)
        {
            if (weightCapacity <= 0)
                throw new ArgumentException("Weight capacity must be positive.", nameof(weightCapacity));
            if (maxFloors <= 0)
                throw new ArgumentException("Max floors must be positive.", nameof(maxFloors));

            _id = id;
            _weightCapacity = weightCapacity;
            _maxFloors = maxFloors;
        }

        public int Id => _id;

        public string ElevatorType => nameof(FreightElevator);

        /// <summary>
        /// Moves the elevator to the specified floor if allowed.
        /// </summary>
        public void MoveToFloor(int floor)
        {
            if (floor < 1 || floor > _maxFloors)
                throw new ArgumentException($"Floor must be between 1 and {_maxFloors}.", nameof(floor));
            if (_restrictedFloors.Contains(floor))
                throw new InvalidOperationException($"Freight elevator cannot access floor {floor}.");

            _isMoving = true;
            _direction = floor > _currentFloor ? "Up" : floor < _currentFloor ? "Down" : "None";
            _currentFloor = floor;
            _isMoving = false;
        }

        /// <summary>
        /// Adds weight (passengers or goods) to the elevator.
        /// </summary>
        public void AddPassengers(int weight)
        {
            if (weight < 0)
                throw new ArgumentException("Weight cannot be negative.", nameof(weight));
            if (_currentWeight + weight > _weightCapacity)
                throw new InvalidOperationException("Exceeds weight capacity.");

            _currentWeight += weight;
        }

        /// <summary>
        /// Removes weight from the elevator.
        /// </summary>
        public void RemovePassengers(int weight)
        {
            if (weight < 0)
                throw new ArgumentException("Weight cannot be negative.", nameof(weight));
            if (_currentWeight - weight < 0)
                throw new InvalidOperationException("Cannot remove more weight than is present.");

            _currentWeight -= weight;
        }

        /// <summary>
        /// Returns the current status of the elevator.
        /// </summary>
        public ElevatorStatus GetStatus()
        {
            return new ElevatorStatus
            {
                CurrentFloor = _currentFloor,
                Direction = _direction,
                IsMoving = _isMoving,
                PassengerCount = _currentWeight
            };
        }
    }
}
