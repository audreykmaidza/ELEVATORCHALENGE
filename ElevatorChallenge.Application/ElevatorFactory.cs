using ElevatorChallenge.Core;
using ElevatorChallenge.Core.Interfaces;

namespace ElevatorChallenge.Application
{
    /// <summary>
    /// Builds elevator instances based on type.
    /// </summary>
    public class ElevatorFactory : IElevatorFactory
    {
        /// <summary>
        /// Creates a new elevator based on the given type.
        /// </summary>
        /// <param name="type">Elevator type (e.g., Elevator, FreightElevator).</param>
        /// <param name="id">Unique elevator ID.</param>
        /// <param name="capacity">Max passenger/load capacity.</param>
        /// <param name="maxFloors">Max number of floors the elevator can serve.</param>
        /// <returns>An elevator instance.</returns>
        /// <exception cref="ArgumentException">If the elevator type is unknown.</exception>
        public IElevator CreateElevator(string type, int id, int capacity, int maxFloors)
        {
            return type switch
            {
                nameof(Elevator) => new Elevator(id, capacity, maxFloors),
                nameof(FreightElevator) => new FreightElevator(id, capacity, maxFloors),
                _ => throw new ArgumentException($"Unknown elevator type: {type}", nameof(type))
            };
        }
    }
}
