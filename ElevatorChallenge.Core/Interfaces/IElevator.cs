namespace ElevatorChallenge.Core.Interfaces
{
    /// <summary>
    /// Defines the contract for an elevator.
    /// Provides methods and properties for elevator identification, movement, passenger handling, and status reporting.
    /// </summary>
    public interface IElevator
    {
        /// <summary>
        /// Gets the unique identifier for the elevator.
        /// </summary>
        int Id { get; }

        /// <summary>
        /// Gets the type of the elevator (e.g., "Passenger", "Freight").
        /// </summary>
        string ElevatorType { get; }

        /// <summary>
        /// Moves the elevator to the specified floor.
        /// </summary>
        /// <param name="floor">The floor number to move to.</param>
        /// <exception cref="ArgumentException">Thrown when the floor number is invalid.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the elevator cannot move to the specified floor.</exception>
        void MoveToFloor(int floor);

        /// <summary>
        /// Adds passengers (or load) to the elevator.
        /// </summary>
        /// <param name="count">The number of passengers or weight to add.</param>
        /// <exception cref="ArgumentException">Thrown when the count is negative.</exception>
        /// <exception cref="InvalidOperationException">Thrown when adding the count exceeds capacity.</exception>
        void AddPassengers(int count);

        /// <summary>
        /// Removes passengers (or load) from the elevator.
        /// </summary>
        /// <param name="count">The number of passengers or weight to remove.</param>
        /// <exception cref="ArgumentException">Thrown when the count is negative.</exception>
        /// <exception cref="InvalidOperationException">Thrown when removing more than the current load.</exception>
        void RemovePassengers(int count);

        /// <summary>
        /// Gets the current operational status of the elevator.
        /// </summary>
        /// <returns>An <see cref="ElevatorStatus"/> object describing the current state of the elevator.</returns>
        ElevatorStatus GetStatus();
    }
}
