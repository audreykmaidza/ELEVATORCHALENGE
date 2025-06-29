namespace ElevatorChallenge.Core.Interfaces
{
    /// <summary>
    /// Interface representing a building that contains elevators.
    /// Defines methods for managing floors and elevators within a building.
    /// </summary>
    public interface IBuilding
    {
        /// <summary>
        /// Adds an elevator to the building.
        /// </summary>
        /// <param name="elevator">The elevator instance to add.</param>
        /// <remarks>
        /// Used to register a new elevator into the building's collection.
        /// </remarks>
        /// <exception cref="ArgumentNullException">Thrown when the elevator parameter is null.</exception>
        void AddElevator(IElevator elevator);

        /// <summary>
        /// Retrieves a read-only list of elevators in the building.
        /// </summary>
        /// <returns>A read-only list of elevators.</returns>
        IReadOnlyList<IElevator> GetElevators();

        /// <summary>
        /// Gets the total number of floors in the building.
        /// </summary>
        /// <returns>The total number of floors in the building.</returns>
        int GetNumberOfFloors();

        /// <summary>
        /// Retrieves an elevator by its unique identifier.
        /// </summary>
        /// <param name="elevatorId">The unique identifier of the elevator.</param>
        /// <returns>An instance of <see cref="IElevator"/> representing the elevator with the specified ID.</returns>
        /// <remarks>
        /// Used to retrieve an elevator from the building's collection by its ID.
        /// </remarks>
        /// <exception cref="ArgumentException">Thrown when the elevator ID is invalid.</exception>
        /// <exception cref="InvalidOperationException">Thrown when no elevator with the specified ID exists.</exception>
        IElevator GetElevatorById(int elevatorId);
    }
}
