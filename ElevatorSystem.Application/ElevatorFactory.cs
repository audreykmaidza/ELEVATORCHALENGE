using ElevatorChallenge.Core;
using ElevatorChallenge.Core.Interfaces;

namespace ElevatorChallenge.Application
{
    /// <summary>
    /// Factory class for creating elevator instances.
    /// This is a placeholder implementation that creates a basic elevator.
    /// It can be extended to create different types of elevators in the future.
    /// </summary>
    public class ElevatorFactory : IElevatorFactory
    {
        /// <summary>
        /// Creates an elevator instance based on the specified type.
        /// This is a placeholder implementation that creates a basic elevator.
        /// It can be extended to create different types of elevators in the future.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <param name="capacity"></param>
        /// <param name="maxFloors"></param>
        /// <returns>An instance of IElevator.</returns>
        /// <remarks>
        /// This method currently creates a PlaceholderElevator instance.
        /// It can be modified to create different types of elevators based on the 'type' parameter.
        /// </remarks>      
        public IElevator CreateElevator(string type, int id, int capacity, int maxFloors)
        {
            return type switch
            {
                nameof(Elevator) => new Elevator(id,capacity,maxFloors),
                nameof(FreightElevator) => new FreightElevator(id,capacity,maxFloors),
                _ => throw new ArgumentException($"Unknown elevator type: {type}", nameof(type))
            };
        }
    }
}