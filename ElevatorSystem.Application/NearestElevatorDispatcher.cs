using ElevatorChallenge.Core;
using ElevatorChallenge.Core.Interfaces;

namespace ElevatorChallenge.Application
{
    /// <summary>
    /// Dispatcher class for managing elevator requests.
    /// This implementation finds the nearest elevator based on the request.
    /// It is a placeholder implementation that can be extended in future tasks.
    /// </summary>
    /// <remarks>
    /// This class implements the IElevatorDispatcher interface and provides a method to dispatch elevators based on floor requests.
    /// The logic to find the nearest elevator will be implemented in future tasks.
    /// The constructor initializes the dispatcher with a building instance, which can be used to access elevator information.
    /// </remarks>
    public class NearestElevatorDispatcher : IElevatorDispatcher
    {
        private readonly IBuilding _building;

        /// <summary>
        /// Initializes a new instance of the <see cref="NearestElevatorDispatcher"/> class.
        ///  This constructor is a placeholder and will be expanded in future tasks.
        /// </summary>
        /// <param name="building">The building instance containing elevators and floors.</param>
        /// <remarks>
        /// This constructor initializes the dispatcher with a building instance.
        /// It is used to access the elevators and their statuses for dispatching purposes.
        /// </remarks>
        /// <exception cref="ArgumentNullException">Thrown when the building parameter is null.</exception>
        public NearestElevatorDispatcher(IBuilding building)
        {
            _building = building ?? throw new ArgumentNullException(nameof(building));
        }

        /// <summary>
        /// Dispatches an elevator to handle a floor request.
        /// This method finds the nearest elevator that can handle the request based on the current status of the elevators.
        /// </summary>
        /// <param name="request">The floor request containing the requested floor and elevator type.</param>
        /// <returns>An instance of <see cref="IElevator"/> representing the dispatched elevator.</returns>
        /// <remarks>
        /// This method is used to find and return the nearest elevator that can handle the specified floor request.
        /// It takes a FloorRequest object as a parameter, which contains the details of the request such as the requested floor and the type of elevator needed.
        /// The logic to find the nearest elevator is based on the current status of the elevators in the building.
        /// </remarks>
        /// <exception cref="InvalidOperationException">Thrown when no suitable elevator is found or no elevators are available.</exception>      
        public IElevator DispatchElevator(FloorRequest request)
        {
            var elevators = _building.GetElevators();

            if (elevators.Count == 0)
            {
                throw new InvalidOperationException("No elevators available.");
            }

            var suitableElevators = elevators
                .Where(e => CanHandleRequest(e, request)) // Filter elevators that can handle the request
                .OrderBy(e => Math.Abs(e.GetStatus().CurrentFloor - request.Floor)) // Sort by distance to the requested floor
                .ThenBy(e => e.GetStatus().IsMoving ? 1 : 0) // Prefer stationary elevators
                .ToList();

            return suitableElevators.FirstOrDefault() ?? throw new InvalidOperationException("No suitable elevator found.");
        }

        /// <summary>
        /// Checks if the elevator can handle the floor request.
        /// This method determines if the elevator can handle the request based on its type and passenger count.
        /// </summary>
        /// <param name="elevator">The elevator to check.</param>
        /// <param name="request">The floor request containing the requested floor and passenger count.</param>
        /// <returns>True if the elevator can handle the request; otherwise, false.</returns>
        /// <remarks>
        /// This method checks the elevator's type and the passenger count in the request to determine if it can handle the request.
        /// It is used to filter out elevators that are not suitable for the request based on their type and current status.
        /// </remarks>
        private bool CanHandleRequest(IElevator elevator, FloorRequest request)
        {
            var status = elevator.GetStatus();

            if (elevator.ElevatorType == nameof(Elevator) && request.PassengerCount <= 10)
            {
                return true;
            }

            if (elevator.ElevatorType == nameof(FreightElevator) && request.Floor == 5)
            {
                return false;
            }

            if (elevator.ElevatorType == nameof(FreightElevator) && request.PassengerCount > 100)
            {
                return true;
            }

            return false;
        }
    }
}