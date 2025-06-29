# Elevator Simulation Console App

A C# console application simulating elevator movement in a multi-floor building, built with clean architecture, SOLID principles, appropriate design patterns, unit testing with Test Driven Development (TDD) using XUnit, NSubstitute, and AutoFixture. Supports multiple elevator types and efficient dispatching.

## Features

- Real-time elevator status display (floor, direction, load)
- Interactive control: request elevators and specify passenger count
- Support for multiple elevators and floors
- Intelligent elevator dispatching logic
- Passenger limit enforcement
- Extensible architecture for future elevator types
- Unit tested logic for reliability
- Clean, modular and maintainable C# codebase

## How It Works

- User inputs a floor and number of passengers.

- The system dispatches the nearest available elevator.

- The elevator moves, passengers board, and it arrives at the requested floor.

- Elevator status updates in real-time.

## Setup
1. **Prerequisites**:
    - .NET 9 SDK
    - Git
    - Visual Studio Code (VS Code)


2. **Clone Repository**:
   ```bash
    git clone https://github.com/audreykmaidza/ELEVATORCHALENGE.git


    cd ElevatorChallenge

   **Restore and Build**
    dotnet restore
    dotnet build

    **Run Tests**
    dotnet test

    **Run Application**
    dotnet run --project ElevatorChallenge.Infrastructure/ElevatorChallenge.Infrastructure.csproj
