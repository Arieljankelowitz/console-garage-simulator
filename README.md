# Garage Management System

## Description

This project is a Garage Management System developed in C#. It demonstrates the integration of classes, inheritance, and polymorphism within .NET and C#. The system manages a vehicle garage that handles five types of vehicles and provides several functionalities for vehicle management.

## Objectives

- Integration of Classes, Inheritance, and Polymorphism
- Working with Arrays/Collections/Data Structures
- Using Enums
- Development and use of external DLL (Assembly)
- Working with multiple projects
- Handling Exceptions

## Features

- Manage multiple types of vehicles: Fuel-Based Motorcycle, Electric Motorcycle, Fuel-Based Car, Electric Car, and Fuel-Based Truck.
- Store and manipulate vehicle properties such as model name, license number, remaining energy percentage, wheel details, and more.
- Perform operations like inflating tires, refueling, and recharging electric vehicles.
- Manage vehicle status within the garage (In Repair, Repaired, Paid for).
- User-friendly console interface for interacting with the system.

## Getting Started

### Prerequisites

- .NET Framework
- C# compiler (e.g., Visual Studio)

### Installation

1. Clone the repository:
    ```bash
    git clone https://github.com/yourusername/garage-management-system.git
    ```
2. Open the solution in your C# development environment.

### Running the System

1. Build the solution in your development environment.
2. Run the executable for the `Ex03.ConsoleUI` project.

## How to Use

1. **Insert a new vehicle:** Select the vehicle type and input the license number. Provide the necessary details based on the vehicle type.
2. **Display list of vehicles:** View the list of license numbers currently in the garage, with filtering options based on vehicle status.
3. **Change vehicle status:** Update the status of a specific vehicle by providing the license number and new status.
4. **Inflate tires:** Inflate the tires of a vehicle to their maximum pressure by providing the license number.
5. **Refuel vehicle:** Refuel a fuel-based vehicle by providing the license number, fuel type, and amount to fill.
6. **Charge vehicle:** Charge an electric vehicle by providing the license number and number of minutes to charge.
7. **Display vehicle information:** View detailed information about a specific vehicle, including license number, model name, owner details, tire specifications, fuel/battery status, and more.

## Project Structure

The solution consists of two projects:

1. **Ex03.GarageLogic:** Contains the object model and the logical layer of the system. This project compiles to a DLL file and does not interact with the user.
2. **Ex03.ConsoleUI:** Implements the user interface for the garage management system. This project references the `Ex03.GarageLogic` DLL to use its functionality.

## Design Considerations

- Separation of user interface and logical layers.
- Use of enums for vehicle properties and statuses.
- Handling exceptions such as `FormatException`, `ArgumentException`, and custom `ValueOutOfRangeException`.
- Use of collections like `List<T>` and `Dictionary<K,V>`.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Acknowledgements

- Inspired by object-oriented programming exercises in C#.
- Thanks to contributors and the community for their support and feedback.

