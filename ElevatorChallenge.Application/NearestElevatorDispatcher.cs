using ElevatorChallenge.Core;
using ElevatorChallenge.Core.Interfaces;

namespace ElevatorChallenge.Application
{
    /// <summary>
    /// Picks the closest elevator for a request.
    /// </summary>
    public class NearestElevatorDispatcher : IElevatorDispatcher
    {
        private readonly IBuilding _building;

        /// <summary>
        /// Sets up the dispatcher with a building.
        /// </summary>
        /// <param name="building">Building with elevators.</param>
        public NearestElevatorDispatcher(IBuilding building)
        {
            _building = building ?? throw new ArgumentNullException(nameof(building));
        }

        /// <summary>
        /// Chooses the best elevator for the given request.
        /// </summary>
        /// <param name="request">Floor and elevator type info.</param>
        /// <returns>The selected elevator.</returns>
        /// <exception cref="InvalidOperationException">If no elevator is available or suitable.</exception>
        public IElevator DispatchElevator(FloorRequest request)
        {
            var elevators = _building.GetElevators();

            if (elevators.Count == 0)
            {
                throw new InvalidOperationException("No elevators available.");
            }

            var suitableElevators = elevators
                .Where(e => CanHandleRequest(e, request)) // Filter capable elevators
                .OrderBy(e => Math.Abs(e.GetStatus().CurrentFloor - request.Floor)) // Nearest first
                .ThenBy(e => e.GetStatus().IsMoving ? 1 : 0) // Prefer idle elevators
                .ToList();

            return suitableElevators.FirstOrDefault() ?? throw new InvalidOperationException("No suitable elevator found.");
        }

        /// <summary>
        /// Checks if the elevator can take the request.
        /// </summary>
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
// This code is part of the Elevator Challenge application, which implements a dispatcher for selecting the nearest elevator based on a floor request.