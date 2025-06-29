using ElevatorChallenge.Core.Interfaces;

namespace ElevatorChallenge.Core
{
    /// <summary>
    /// Represents a building with elevators and multiple floors.
    /// </summary>
    public class Building : IBuilding
    {
        private readonly int _numberOfFloors;
        private readonly IList<IElevator> _elevators = new List<IElevator>();

        /// <summary>
        /// Creates a building with a given number of floors.
        /// </summary>
        /// <param name="numberOfFloors">Total number of floors.</param>
        /// <exception cref="ArgumentException">If number of floors is not positive.</exception>
        public Building(int numberOfFloors)
        {
            if (numberOfFloors <= 0)
            {
                throw new ArgumentException("Number of floors must be positive.", nameof(numberOfFloors));
            }

            _numberOfFloors = numberOfFloors;
        }

        /// <summary>
        /// Adds an elevator to the building.
        /// </summary>
        public void AddElevator(IElevator elevator)
        {
            _elevators.Add(elevator);
        }

        /// <summary>
        /// Gets an elevator by its ID.
        /// </summary>
        /// <param name="elevatorId">Elevator's unique ID.</param>
        /// <returns>The matching elevator.</returns>
        /// <exception cref="ArgumentException">If ID is negative.</exception>
        /// <exception cref="InvalidOperationException">If no elevator is found.</exception>
        public IElevator GetElevatorById(int elevatorId)
        {
            if (elevatorId < 0)
            {
                throw new ArgumentException("Elevator ID cannot be negative.", nameof(elevatorId));
            }

            return _elevators.FirstOrDefault(e => e.Id == elevatorId)
                ?? throw new InvalidOperationException($"Elevator with ID {elevatorId} not found.");
        }

        /// <summary>
        /// Returns a read-only list of elevators.
        /// </summary>
        public IReadOnlyList<IElevator> GetElevators()
        {
            return _elevators.AsReadOnly();
        }

        /// <summary>
        /// Returns the number of floors in the building.
        /// </summary>
        public int GetNumberOfFloors()
        {
            return _numberOfFloors;
        }
    }
}
