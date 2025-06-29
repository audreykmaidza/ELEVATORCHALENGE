namespace ElevatorChallenge.Core
{   
    /// <summary>
    /// Represents a request for an elevator at a specific floor.
    /// Includes floor number, passenger count, and travel direction.
    /// </summary>
    public class FloorRequest
    {
        /// <summary>
        /// The floor where the elevator is requested.
        /// </summary>
        public int Floor { get; set; }

        /// <summary>
        /// Number of passengers waiting on the floor.
        /// </summary>
        public int PassengerCount { get; set; }

        /// <summary>
        /// Desired direction of travel ("Up" or "Down").
        /// </summary>
        public string Direction { get; set; } = "Up";
    }    
}
