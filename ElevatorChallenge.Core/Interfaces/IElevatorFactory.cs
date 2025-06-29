namespace ElevatorChallenge.Core.Interfaces
{
    /// <summary>
    /// Defines a factory for creating elevator instances.
    /// </summary>
    /// <remarks>
    /// This interface supports extensibility for various elevator types (e.g., passenger, freight) and configurations.
    /// </remarks>
    public interface IElevatorFactory
    {
        /// <summary>
        /// Creates an elevator instance with the specified parameters.
        /// </summary>
        /// <param name="type">The type of the elevator (e.g., "Passenger", "Freight").</param>
        /// <param name="id">The unique identifier for the elevator.</param>
        /// <param name="capacity">The maximum passenger capacity of the elevator.</param>
        /// <param name="maxFloors">The number of floors the elevator can service.</param>
        /// <returns>An instance of <see cref="IElevator"/>.</returns>
        /// <remarks>
        /// Implementations may return different elevator classes depending on the type parameter.
        /// </remarks>
        IElevator CreateElevator(string type, int id, int capacity, int maxFloors);
    }
}
