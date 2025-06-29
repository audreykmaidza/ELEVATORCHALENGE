using ElevatorChallenge.Core.Interfaces;
using ElevatorChallenge.Core;

namespace ElevatorChallenge.Application
{
    /// <summary>
    /// Handles elevator requests and status updates.
    /// </summary>
    public class ElevatorController : IElevatorController
    {
        private readonly IElevatorDispatcher _dispatcher;
        private readonly IBuilding _building;

        /// <summary>
        /// Creates a new controller with a dispatcher and building.
        /// </summary>
        public ElevatorController(IElevatorDispatcher dispatcher, IBuilding building)
        {
            _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
            _building = building ?? throw new ArgumentNullException(nameof(building));
        }

        /// <summary>
        /// Requests an elevator from a floor in a given direction.
        /// </summary>
        /// <param name="fromFloor">The pickup floor.</param>
        /// <param name="passengerCount">Number of waiting passengers.</param>
        /// <param name="direction">"Up" or "Down".</param>
        /// <exception cref="ArgumentException">If inputs are invalid.</exception>
        /// <exception cref="InvalidOperationException">If no elevator can be dispatched.</exception>
        public void RequestElevator(int fromFloor, int passengerCount, string direction)
        {
            if (fromFloor < 1 || fromFloor >= _building.GetNumberOfFloors())
            {
                throw new ArgumentException("Invalid floor.", nameof(fromFloor));
            }

            if (passengerCount < 0)
            {
                throw new ArgumentException("Passenger count cannot be negative.", nameof(passengerCount));
            }

            if (direction != "Up" && direction != "Down")
            {
                throw new ArgumentException("Direction must be 'Up' or 'Down'.", nameof(direction));
            }

            var request = new FloorRequest
            {
                Floor = fromFloor,
                PassengerCount = passengerCount,
                Direction = direction
            };

            var elevator = _dispatcher.DispatchElevator(request);

            elevator.MoveToFloor(fromFloor);
            elevator.AddPassengers(passengerCount);
        }

        /// <summary>
        /// Gets the current status of a specific elevator.
        /// </summary>
        /// <param name="elevatorId">Elevator ID.</param>
        /// <returns>Current status of the elevator.</returns>
        /// <exception cref="ArgumentException">If the elevator ID is invalid or not found.</exception>
        /// <exception cref="InvalidOperationException">If the elevator ID is negative.</exception>
        public ElevatorStatus GetElevatorStatus(int elevatorId)
        {
            if (elevatorId < 0)
            {
                throw new InvalidOperationException("Invalid elevator ID.");
            }

            var elevator = _building.GetElevatorById(elevatorId);

            if (elevator == null)
            {
                throw new ArgumentException($"No elevator found with ID {elevatorId}.");
            }

            return elevator.GetStatus();
        }
    }
}
// This code is part of the Elevator Challenge application, which implements a controller for managing elevator requests and statuses.