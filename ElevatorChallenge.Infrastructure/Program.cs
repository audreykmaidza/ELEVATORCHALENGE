using ElevatorChallenge.Application;
using ElevatorChallenge.Core;
using ElevatorChallenge.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ElevatorChallenge.Infrastructure
{
    /// <summary>
    /// Main entry point for the Elevator application.
    /// This program sets up the dependency injection container and initializes the building with elevators.
    /// </summary>
    /// <remarks>
    /// This is a placeholder for the main loop and will be expanded in future tasks.
    /// </remarks>
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Elevator Simulation Starting...");

            // Configure Dependency Injection
            var services = new ServiceCollection();

            // Register services
            services.AddSingleton<IBuilding, Building>(sp => new Building(10)); // Building with 10 floors
            services.AddSingleton<IElevatorDispatcher, NearestElevatorDispatcher>();
            services.AddSingleton<IElevatorController, ElevatorController>();
            services.AddSingleton<IElevatorFactory, ElevatorFactory>();

            // Register two elevator instances (Passenger and Freight)
            services.AddSingleton<IElevator>(sp =>
                sp.GetRequiredService<IElevatorFactory>().CreateElevator(nameof(Elevator), 1, 10, 10)); // Passenger elevator
                
            services.AddSingleton<IElevator>(sp =>
                sp.GetRequiredService<IElevatorFactory>().CreateElevator(nameof(FreightElevator), 2, 100, 10)); // Freight elevator

            // Build service provider
            var serviceProvider = services.BuildServiceProvider();

            // Resolve and initialize building
            var building = serviceProvider.GetRequiredService<IBuilding>();
            var elevators = serviceProvider.GetServices<IElevator>();

            // Resolve controller
            var controller = serviceProvider.GetRequiredService<IElevatorController>();

            // Add elevators to the building
            foreach (var elevator in elevators)
            {
                building.AddElevator(elevator);
            }
            
            Console.WriteLine("Elevator Simulation Initialized with DI");

            // Main loop for user interaction
            while (true)
            {
                try
                {
                    Console.WriteLine("\nElevator Simulation");
                    Console.WriteLine("1. Request Elevator");
                    Console.WriteLine("2. View Elevator Status");
                    Console.WriteLine("3. Exit");
                    Console.Write("Select an option: ");

                    var choice = Console.ReadLine();
                    if (choice == "3") break;

                    switch (choice)
                    {
                        case "1":
                            {
                                Console.Write("Enter floor (1-10): ");
                                
                                if (!int.TryParse(Console.ReadLine(), out var floor))
                                {
                                    throw new ArgumentException("Invalid floor.");
                                }

                                Console.Write("Enter passenger count: ");

                                if (!int.TryParse(Console.ReadLine(), out var passengers))
                                {
                                    throw new ArgumentException("Invalid passenger count.");
                                }

                                Console.Write("Enter direction (Up/Down): ");

                                var direction = Console.ReadLine() ?? "Up";

                                if (string.IsNullOrWhiteSpace(direction))
                                {
                                    throw new ArgumentException("Direction cannot be empty.");
                                }

                                controller.RequestElevator(floor, passengers, direction);

                                Console.WriteLine("Elevator dispatched.");
                            }
                            break;

                        case "2":
                            {
                                foreach (var elevator in building.GetElevators())
                                {
                                    var status = controller.GetElevatorStatus(elevator.Id);
                                    Console.WriteLine($"Elevator {elevator.Id} ({elevator.ElevatorType}): Floor {status.CurrentFloor}, {status.Direction}, {(status.IsMoving ? "Moving" : "Idle")}, Passengers: {status.PassengerCount}");
                                }
                            }
                            break;
                        default:
                            {
                                Console.WriteLine("Invalid option.");
                            }
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}