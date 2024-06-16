﻿using System;
using System.Collections.Generic;
using Ex03.GarageLogic;

namespace Ex_03
{
    internal class FrontOffice
    {
        private Garage m_Garage;
        public bool BeingServiced { get; set; } = true;
        public FrontOffice()
        {
            m_Garage = new Garage();
        }
        internal void Welcome()
        {
            string welcomeMsg = "Welcome to the Garage!";
            Console.WriteLine(welcomeMsg);

        }

        internal string ChooseService()
        {
            string[] serviceOptions = {"1. Insert a new vehicle", "2. Display vehicles", "3. Change vehicle status",
                "4. Inflate vehicle tires", "5. Refuel vehicle", "6. Recharge vehicle", "7. Display vehicle info", "8. Leave Garage"};
            string message = "Please select a service.";

            string chosenService = ConnsoleUtil.ChooseOption(message, serviceOptions, 1);

            return chosenService;

        }

        internal void InsertVehicle()
        {
            string[] supportedVehicles = { "Fuel-Based Motorcycle", "Electric Motorcycle", "Fuel-Based Car", 
                "Electric Car", "Fuel-Based Truck" };
            string message = "Choose your vehicle: " +
                "";
            string vehicleChosen = ConnsoleUtil.ChooseOption(message, supportedVehicles, 2);

            Console.WriteLine("Please enter the License Number of the {0}: ", vehicleChosen);
            string vehicleLicenseNum = Console.ReadLine();

            if (m_Garage.IsVehicleInGarage(vehicleLicenseNum))
            {
                Console.WriteLine("Vehicle already registered, changing status to 'In Repair'");
                eVehicleStatus updatedStatus = eVehicleStatus.InRepair;
                m_Garage.ChangeStatus(updatedStatus, vehicleLicenseNum);
            } 
            else
            {
                registerNewVehicle(vehicleChosen, vehicleLicenseNum);
            }
        }

        internal void DisplayVehicles()
        {
            string displayMessage = "Filter Vehicles based on: ";
            string[] filterOptions = { "All", "In Repair", "Repaired", "Payed For" };
            string filter = ConnsoleUtil.ChooseOption(displayMessage, filterOptions);

            List<string> vehicleLicenses;


            try
            {
                eVehicleStatus filterStatus = ConnsoleUtil.ParseEnum<eVehicleStatus>(filter.Replace(" ", ""));
                vehicleLicenses = m_Garage.FilterVehicles(filterStatus);
            }
            catch (Exception ex)
            {
                vehicleLicenses = m_Garage.VehiclesInGarage;
            }

            foreach (string vehicleLicense in vehicleLicenses) 
            {
                Console.WriteLine(vehicleLicense);
            }
        }

        internal void ChangeStatus()
        {
            Console.WriteLine("Please enter the license number of the vehicle you'd like to update: ");
            string vehicleLicense = Console.ReadLine();

            string statusMessage = "select the new status";
            string[] statusOptions = { "In Repair", "Repaired", "Payed For" };
            string updatedStatusString = ConnsoleUtil.ChooseOption(statusMessage, statusOptions).Replace(" ", "");
            eVehicleStatus updatedStatus = ConnsoleUtil.ParseEnum<eVehicleStatus>(updatedStatusString);

            m_Garage.ChangeStatus(updatedStatus, vehicleLicense);

            Console.Clear();
            Console.WriteLine("Status Updated.");
        }

        internal void FillTires()
        {
            Console.WriteLine("Please enter the license number of the vehicle you'd like to pump: ");
            string vehicleLicense = Console.ReadLine();

            try
            {
                m_Garage.PumpTires(vehicleLicense);
                Console.WriteLine("Succesfully filled tires");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Invalid License Number, unable to fill up tires.");
            }
        }


        internal void Refuel()
        {
            Console.WriteLine("Please enter the license number of the vehicle you'd like to fill up: ");
            string vehicleLicense = Console.ReadLine();

            string fuelMessage = "select the new status";
            string[] fuelOptions = { " Soler", "Octane95", "Octane96", "Octane98" };
            string chosenFuelString = ConnsoleUtil.ChooseOption(fuelMessage, fuelOptions).Replace(" ", "");
            eFuelType chosenFuel = ConnsoleUtil.ParseEnum<eFuelType>(chosenFuelString);
            Console.WriteLine("How many ltrs to add?");
            float litersToAdd = float.Parse(Console.ReadLine()); 
            
            try
            {
                m_Garage.Refuel(vehicleLicense, chosenFuel, litersToAdd);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Invalid.");
            }
        }

        internal void Recharge()
        {
            Console.WriteLine("Please enter the license number of the vehicle you'd like charge: ");
            string vehicleLicense = Console.ReadLine();
            Console.WriteLine("How many minutes would you like to charge?");
            int minutesToCharge = int.Parse(Console.ReadLine());
            try
            {
                m_Garage.ReCharge(vehicleLicense, minutesToCharge);
                Console.WriteLine("Succesfully put to charge");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid");
            }
        }

        internal void DisplayVehicleInfo()
        {
            Console.WriteLine("Please enter the license number of the vehicle you'd like display: ");
            string vehicleLicense = Console.ReadLine();

            string vehicleInfo = m_Garage.DisplayVehicle(vehicleLicense);
            Console.WriteLine(vehicleInfo);
            Console.WriteLine("Press any key to go back to main menu...");
            Console.ReadLine();
        }

        internal void Goodbye()
        {
            Console.Clear();
            Console.WriteLine("Thank you for coming to the Garage, have a good day! (press any key to quit)");
            Console.ReadLine();
        }

        private List<(string manufacturerName, float currentAirPressure)> collectWheelData(int i_NumOfWheelsToAdd)
        {
            List<(string manufacturerName, float currentAirPressure)> wheelDataList = new List<(string, float)>();

            try
            {
                string[] options = { "Uniformly (all wheels the same)", "Individually (specify details for each wheel)" };
                string choice = ConnsoleUtil.ChooseOption("Do you want to add wheels uniformly or individually?", options);

                if (choice == "Uniformly (all wheels the same)")
                {
                    (string manufacturerName, float currentAirPressure) = ConnsoleUtil.CollectUniformWheelData();

                    for (int i = 0; i < i_NumOfWheelsToAdd; i++)
                    {
                        wheelDataList.Add((manufacturerName, currentAirPressure));
                    }
                }
                else
                {
                    wheelDataList = ConnsoleUtil.CollectIndividualWheelData(i_NumOfWheelsToAdd);
                }

                Console.WriteLine("Wheel data collected successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error collecting wheel data: {ex.Message}");
            }

            return wheelDataList;
        }


        private void registerNewVehicle(string i_VehicleType, string i_LicenseNumber)
        {
            List<(string manufacturerName, float currentAirPressure)> wheelDataList;
            int numOfWheelsToAdd;

            string ownerName, phoneNumber, vehicleModel;
            getOwnerDetails(i_VehicleType, out ownerName, out phoneNumber, out vehicleModel);

            Console.Clear();

            if (i_VehicleType.Contains("Car"))
            {
                numOfWheelsToAdd = 4;
                (eColor carColor, int carDoors) = ConnsoleUtil.NewCar();
                Console.Clear();
                wheelDataList = collectWheelData(numOfWheelsToAdd);
                Console.Clear();

                if (i_VehicleType.Contains("Electric"))
                {
                    float currentBatteryLife = ConnsoleUtil.NewElectric();
                    m_Garage.CreateNewVehicle(carColor, carDoors, eEngineType.Electric, i_LicenseNumber, vehicleModel, ownerName, phoneNumber,
                      wheelDataList, currentBatteryLife);

                    vehicleRegistered();
                }
                else
                {

                    float currentFuel = ConnsoleUtil.CurrentFuelAmount();

                    m_Garage.CreateNewVehicle(carColor, carDoors, eEngineType.Fuel, i_LicenseNumber, vehicleModel, ownerName, phoneNumber,
                        wheelDataList, i_CurrentFuel: currentFuel);

                    vehicleRegistered();
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine("Please provide a valid input.");
                    
                }
              
             
            }
            else if (i_VehicleType.Contains("Motorcycle"))
            {
                numOfWheelsToAdd = 2;
                wheelDataList = collectWheelData(numOfWheelsToAdd);

                try
                {
                    (eLicenseType LicenseType, int EngineVolume) motorcycleInfo = ConnsoleUtil.NewMotorcycle();
                    bool vehicleCreated = false;

                    while (!vehicleCreated)
                    {
                        try
                        {
                            if (i_VehicleType.Contains("Electric"))
                            {
                                float currentBatteryLife = ConnsoleUtil.NewElectric();
                                m_Garage.CreateNewVehicle(motorcycleInfo.LicenseType, motorcycleInfo.EngineVolume, eEngineType.Electric, i_LicenseNumber,
                                                          vehicleModel, ownerName, phoneNumber, wheelDataList, currentBatteryLife);

                                vehicleCreated = true;
                            }
                            else
                            {
                                float currentFuel = ConnsoleUtil.CurrentFuelAmount();
                                m_Garage.CreateNewVehicle(motorcycleInfo.LicenseType, motorcycleInfo.EngineVolume, eEngineType.Fuel, i_LicenseNumber,
                                                          vehicleModel, ownerName, phoneNumber, wheelDataList, i_CurrentFuel: currentFuel);

                                vehicleCreated = true;
                            }

                            vehicleRegistered();
                        }
                        catch (ValueOutOfRangeException ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                            Console.WriteLine("Please provide a valid input.");
                            // Optionally, you can add additional logic here to re-prompt the user for input
                        }
                     
                       
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine("Please provide a valid input.");
                    
                }

            }
            else if (i_VehicleType.Contains("Truck"))
            {
                numOfWheelsToAdd = 12;
                wheelDataList = collectWheelData(numOfWheelsToAdd);

                (bool containsDangerousMaterials, float cargoTankVolume) truckInfo = ConnsoleUtil.NewTruck();
                float currentFuel = ConnsoleUtil.CurrentFuelAmount();
                m_Garage.CreateNewVehicle(truckInfo.containsDangerousMaterials, truckInfo.cargoTankVolume, eEngineType.Fuel, i_LicenseNumber, vehicleModel, ownerName, phoneNumber,
                        wheelDataList, i_CurrentFuel: currentFuel);

                vehicleRegistered();
            }
        }

        private static void getOwnerDetails(string i_VehicleType, out string o_OwnerName, out string o_PhoneNumber, out string o_VehicleModel)
        {
            Console.WriteLine("Thank you for choosing our Garage, lets start registering");
            Console.WriteLine("Please enter your name: ");
            o_OwnerName = Console.ReadLine();
            Console.WriteLine("Please enter your phone number: ");
            o_PhoneNumber = Console.ReadLine();
            Console.WriteLine("Please enter the {0}'s model: ", i_VehicleType);
            o_VehicleModel = Console.ReadLine();
        }


        private static void vehicleRegistered()
        {
            Console.Clear();
            Console.WriteLine("Vehicle Reigstered!");
            ConnsoleUtil.BlankSpace();
        }
    }
}
