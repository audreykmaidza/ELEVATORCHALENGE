namespace ElevatorChallenge.Core.Interfaces
{
    /// <summary>
    /// Defines the contract for an elevator dispatcher responsible for selecting and dispatching elevators.
    /// </summary>
    /// <remarks>
    /// Implementations of this interface are responsible for determining which elevator should respond to a given floor request.
    /// </remarks>
    public interface IElevatorDispatcher
    {
        /// <summary>
        /// Dispatches the most appropriate elevator to handle the specified floor request.
        /// </summary>
        /// <param name="request">The floor request containing the target floor and elevator type.</param>
        /// <returns>An <see cref="IElevator"/> instance representing the dispatched elevator.</returns>
        IElevator DispatchElevator(FloorRequest request);
    }
}
