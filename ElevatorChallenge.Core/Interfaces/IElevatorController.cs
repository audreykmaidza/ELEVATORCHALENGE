namespace ElevatorChallenge.Core.Interfaces
{
    /// <summary>
    /// Defines the contract for an elevator controller.
    /// Provides methods for requesting elevator service and retrieving elevator status.
    /// </summary>
    public interface IElevatorController
    {
        /// <summary>
        /// Requests an elevator to pick up passengers from a specified floor.
        /// </summary>
        /// <param name="fromFloor">The floor where passengers are waiting.</param>
        /// <param name="passengerCount">The number of passengers requesting service.</param>
        /// <param name="direction">The desired direction of travel (e.g., "up" or "down").</param>
        void RequestElevator(int fromFloor, int passengerCount, string direction);

        /// <summary>
        /// Retrieves the current status of a specific elevator.
        /// </summary>
        /// <param name="elevatorId">The unique identifier of the elevator.</param>
        /// <returns>An <see cref="ElevatorStatus"/> object representing the elevator's current status.</returns>
        ElevatorStatus GetElevatorStatus(int elevatorId);
    }
}
