using ElevatorChallenge.Core.Interfaces;

namespace ElevatorChallenge.Core
{
    /// <summary>
    /// A basic placeholder implementation of the <see cref="IElevator"/> interface.
    /// This class can be extended to implement specific elevator behavior in future tasks.
    /// </summary>
    public class PlaceholderElevator : IElevator
    {
        /// <summary>
        /// Gets the unique identifier for the elevator.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Gets the type of the elevator (e.g., "Passenger", "Freight").
        /// </summary>
        public string ElevatorType { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaceholderElevator"/> class.
        /// </summary>
        /// <param name="id">The elevator's unique identifier.</param>
        /// <param name="type">The type of elevator.</param>
        public PlaceholderElevator(int id, string type)
        {
            Id = id;
            ElevatorType = type;
        }

        /// <inheritdoc/>
        public void MoveToFloor(int floor)
        {
            throw new NotImplementedException("MoveToFloor is not implemented yet.");
        }

        /// <inheritdoc/>
        public void AddPassengers(int count)
        {
            throw new NotImplementedException("AddPassengers is not implemented yet.");
        }

        /// <inheritdoc/>
        public void RemovePassengers(int count)
        {
            throw new NotImplementedException("RemovePassengers is not implemented yet.");
        }

        /// <inheritdoc/>
        public ElevatorStatus GetStatus()
        {
            throw new NotImplementedException("GetStatus is not implemented yet.");
        }
    }
}
