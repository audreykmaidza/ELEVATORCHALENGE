namespace ElevatorChallenge.Core
{
    /// <summary>
    /// Represents the current status of an elevator.
    /// </summary>
    public class ElevatorStatus
    {
        /// <summary>
        /// The floor the elevator is currently on.
        /// </summary>
        public int CurrentFloor { get; init; }

        /// <summary>
        /// The direction the elevator is moving ("Up", "Down", or "None").
        /// </summary>
        public string Direction { get; init; } = "None";

        /// <summary>
        /// Whether the elevator is currently moving.
        /// </summary>
        public bool IsMoving { get; init; }

        /// <summary>
        /// Number of passengers currently in the elevator.
        /// </summary>
        public int PassengerCount { get; init; }
    }
}
