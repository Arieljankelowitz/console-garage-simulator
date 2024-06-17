﻿using System;
using System.Collections.Generic;
using Ex03.GarageLogic;

namespace Ex_03
{
    internal class FrontOffice
    {
        private readonly Garage r_Garage;
        internal bool BeingServiced { get; set; }
        internal FrontOffice()
        {
            r_Garage = new Garage();
            BeingServiced = true;
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
            const string k_PleaseSelectAService = "Please select a service.";
            const int k_OptionsPerLine = 1;
            string chosenService = ConnsoleUtil.ChooseOption(k_PleaseSelectAService, serviceOptions, k_OptionsPerLine);

            return chosenService;
        }

        internal void InsertVehicle()
        {
            string vehicleChosen = chooseAVehicle();
            string vehicleLicenseNum = enterLicenseNumber(vehicleChosen);

            if (r_Garage.IsVehicleInGarage(vehicleLicenseNum))
            {
                vehicleAlreadyRegistered(vehicleLicenseNum);
            }
            else
            {
                registerNewVehicle(vehicleChosen, vehicleLicenseNum);
            }
        }


        internal void DisplayVehicles()
        {
            const string k_FilterVehiclesBasedOn = "Filter Vehicles based on: ";
            string[] filterOptions = { "All", "In Repair", "Repaired", "Payed For" };
            string filter = ConnsoleUtil.ChooseOption(k_FilterVehiclesBasedOn, filterOptions);

            List<string> vehicleLicenses = filterVehiclesVehicleLicenses(filter);

            foreach (string vehicleLicense in vehicleLicenses)
            {
                Console.WriteLine(vehicleLicense);
            }
        }

        private List<string> filterVehiclesVehicleLicenses(string i_Filter)
        {
            List<string> vehicleLicenses;
            try
            {
                eVehicleStatus filterStatus = ConnsoleUtil.ParseEnum<eVehicleStatus>(i_Filter.Replace(" ", ""));
                vehicleLicenses = r_Garage.FilterVehicles(filterStatus);
            }
            catch (Exception ex)
            {
                vehicleLicenses = r_Garage.VehiclesInGarage;
            }

            return vehicleLicenses;
        }

        internal void ChangeStatus()
        {
            try
            {
                Console.WriteLine("Please enter the license number of the vehicle you'd like to update: ");
                string vehicleLicense = Console.ReadLine();

                const string k_SelectTheNewStatus = "select the new status";
                string[] statusOptions = { "In Repair", "Repaired", "Payed For" };
                string updatedStatusString =
                    ConnsoleUtil.ChooseOption(k_SelectTheNewStatus, statusOptions).Replace(" ", "");
                eVehicleStatus updatedStatus = ConnsoleUtil.ParseEnum<eVehicleStatus>(updatedStatusString);

                r_Garage.ChangeStatus(updatedStatus, vehicleLicense);

                Console.Clear();
                Console.WriteLine("Status Updated.");
            }
            catch(Exception ex) 
            {
                Console.WriteLine("Invalid license number. Couldn't update status.");
            }
        }

        internal void FillTires()
        {
            Console.WriteLine("Please enter the license number of the vehicle you'd like to pump: ");
            string vehicleLicense = Console.ReadLine();

            try
            {
                r_Garage.PumpTires(vehicleLicense);
                Console.WriteLine("Successfully filled tires");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid License Number, unable to fill up tires.");
            }
        }

        internal void Refuel()
        {
            Console.WriteLine("Please enter the license number of the vehicle you'd like to fill up: ");
            string vehicleLicense = Console.ReadLine();

            const string k_SelectTheNewStatus = "select the new status";
            string[] fuelOptions = { " Soler", "Octane95", "Octane96", "Octane98" };
            string chosenFuelString = ConnsoleUtil.ChooseOption(k_SelectTheNewStatus, fuelOptions).Replace(" ", "");
            eFuelType chosenFuel = ConnsoleUtil.ParseEnum<eFuelType>(chosenFuelString);
            Console.WriteLine("How many litres to add?");
            string litersGivenToAdd = Console.ReadLine();

            try
            {
                float.TryParse(litersGivenToAdd, out float litersToAdd);
                r_Garage.Refuel(vehicleLicense, chosenFuel, litersToAdd);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        internal void Recharge()
        {
            Console.WriteLine("Please enter the license number of the vehicle you'd like charge: ");
            string vehicleLicense = Console.ReadLine();
            Console.WriteLine("How many minutes would you like to charge?");
            
            try
            {
                string minutesGivenToCharge = Console.ReadLine();
                int.TryParse(minutesGivenToCharge, out int minutesToCharge);
                r_Garage.ReCharge(vehicleLicense, minutesToCharge);
                Console.WriteLine("Successfully put to charge");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        internal void DisplayVehicleInfo()
        {
            Console.WriteLine("Please enter the license number of the vehicle you'd like display: ");
            string vehicleLicense = Console.ReadLine();

            string vehicleInfo = r_Garage.DisplayVehicle(vehicleLicense);
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
                const string k_WheelsUniformlyOrIndividually = "Do you want to add wheels uniformly or individually?";
                string choice = ConnsoleUtil.ChooseOption(k_WheelsUniformlyOrIndividually, options);

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
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error collecting wheel data: {ex.Message}");
            }

            return wheelDataList;
        }

        private void registerNewVehicle(string i_VehicleType, string i_LicenseNumber)
        {
            var (ownerName, phoneNumber, vehicleModel) = collectOwnerDetails(i_VehicleType);

            if (i_VehicleType.Contains("Car"))
            {
                handleCarRegistration(i_VehicleType, i_LicenseNumber, ownerName, phoneNumber, vehicleModel);
            }
            else if (i_VehicleType.Contains("Motorcycle"))
            {
                handleMotorcycleRegistration(i_VehicleType, i_LicenseNumber, ownerName, phoneNumber, vehicleModel);
            }
            else if (i_VehicleType.Contains("Truck"))
            {
                handleTruckRegistration(i_LicenseNumber, ownerName, phoneNumber, vehicleModel);
            }
            else
            {
                Console.WriteLine("Unknown vehicle type.");
            }
        }

        private void handleCarRegistration(string i_VehicleType, string i_LicenseNumber, string i_OwnerName, string i_PhoneNumber, string i_VehicleModel)
        {
            int numOfWheelsToAdd = 4;
            (eColor carColor, int carDoors) = ConnsoleUtil.NewCar();
            Console.Clear();

            bool isCreated = false;
            while (!isCreated)
            {
                try
                {
                    List<(string manufacturerName, float currentAirPressure)> wheelDataList = collectWheelData(numOfWheelsToAdd);
                    
                    if (i_VehicleType.Contains("Electric"))
                    {
                       
                        float currentBatteryLife = ConnsoleUtil.NewElectric();
                        r_Garage.CreateNewVehicle(carColor, carDoors, eEngineType.Electric, i_LicenseNumber, i_VehicleModel, i_OwnerName, i_PhoneNumber,
                                                  wheelDataList, currentBatteryLife);
                    }
                    else
                    {
                        float currentFuel = ConnsoleUtil.CurrentFuelAmount();
                        r_Garage.CreateNewVehicle(carColor, carDoors, eEngineType.Fuel, i_LicenseNumber, i_VehicleModel, i_OwnerName, i_PhoneNumber,
                                                  wheelDataList, i_CurrentFuel: currentFuel);
                    }

                    vehicleRegistered();
                    isCreated = true;
                }
                catch (Exception ex)
                {
                    HandleRegistrationException(ex);
                }
            }
        }

        private void handleMotorcycleRegistration(string i_VehicleType, string i_LicenseNumber, string i_OwnerName, string i_PhoneNumber, string i_VehicleModel)
        {
            int numOfWheelsToAdd = 2;
           
            Console.Clear();
            (eLicenseType LicenseType, int EngineVolume) motorcycleInfo = ConnsoleUtil.NewMotorcycle();

            bool isCreated = false;
            while (!isCreated)
            {
                try
                {
                    List<(string manufacturerName, float currentAirPressure)> wheelDataList = collectWheelData(numOfWheelsToAdd);
                   
                    if (i_VehicleType.Contains("Electric"))
                    {
                        float currentBatteryLife = ConnsoleUtil.NewElectric();
                        r_Garage.CreateNewVehicle(motorcycleInfo.LicenseType, motorcycleInfo.EngineVolume, eEngineType.Electric, i_LicenseNumber,
                                                  i_VehicleModel, i_OwnerName, i_PhoneNumber, wheelDataList, currentBatteryLife);
                    }
                    else
                    {
                        float currentFuel = ConnsoleUtil.CurrentFuelAmount();
                        r_Garage.CreateNewVehicle(motorcycleInfo.LicenseType, motorcycleInfo.EngineVolume, eEngineType.Fuel, i_LicenseNumber,
                                                  i_VehicleModel, i_OwnerName, i_PhoneNumber, wheelDataList, i_CurrentFuel: currentFuel);
                    }

                    vehicleRegistered();
                    isCreated = true;
                }
                catch (Exception ex)
                {
                    HandleRegistrationException(ex);
                }
            }
        }

        private void handleTruckRegistration(string i_LicenseNumber, string i_OwnerName, string i_PhoneNumber, string i_VehicleModel)
        {
            int numOfWheelsToAdd = 12;
           
            Console.Clear();
            (bool containsDangerousMaterials, float cargoTankVolume) truckInfo = ConnsoleUtil.NewTruck();
            bool isCreated = false;
            while (!isCreated)
            {
                try
                {
                    List<(string manufacturerName, float currentAirPressure)> wheelDataList = collectWheelData(numOfWheelsToAdd);
                    float currentFuel = ConnsoleUtil.CurrentFuelAmount();
                    r_Garage.CreateNewVehicle(truckInfo.containsDangerousMaterials, truckInfo.cargoTankVolume, eEngineType.Fuel, i_LicenseNumber, i_VehicleModel, i_OwnerName, i_PhoneNumber,
                                              wheelDataList, i_CurrentFuel: currentFuel);

                    vehicleRegistered();
                    isCreated = true;
                }
                catch (Exception ex)
                {
                    HandleRegistrationException(ex);
                }
            }
        }

        internal void HandleRegistrationException(Exception i_Exception)
        {
            if (i_Exception is ValueOutOfRangeException || i_Exception is ArgumentException)
            {
                Console.WriteLine($"Error: {i_Exception.Message}");
                Console.WriteLine("Please provide a valid input.");
               
            }
            else
            {
                Console.WriteLine($"An unexpected error occurred: {i_Exception.Message}");
                Console.WriteLine("Please try again later.");
            }
        }

        private (string ownerName, string phoneNumber, string vehicleModel) collectOwnerDetails(string i_VehicleType)
        {
            Console.WriteLine("Thank you for choosing our Garage, let's start registering");
            Console.WriteLine("Please enter your name: ");
            string ownerName = Console.ReadLine();

            Console.WriteLine("Please enter your phone number: ");
            string phoneNumber = Console.ReadLine();

            Console.WriteLine($"Please enter the {i_VehicleType}'s model: ");
            string vehicleModel = Console.ReadLine();
            Console.Clear();

            return (ownerName, phoneNumber, vehicleModel);
        }

        private static void vehicleRegistered()
        {
            Console.Clear();
            Console.WriteLine("Vehicle Registered!");
            ConnsoleUtil.BlankSpace();
        }

        private void vehicleAlreadyRegistered(string i_VehicleLicenseNum)
        {
            Console.WriteLine("Vehicle already registered, changing status to 'In Repair'");
            eVehicleStatus updatedStatus = eVehicleStatus.InRepair;
            r_Garage.ChangeStatus(updatedStatus, i_VehicleLicenseNum);
        }

        private static string enterLicenseNumber(string i_VehicleChosen)
        {
            Console.WriteLine("Please enter the License Number of the {0}: ", i_VehicleChosen);
            string vehicleLicenseNum = Console.ReadLine();

            return vehicleLicenseNum;
        }

        private static string chooseAVehicle()
        {
            string[] supportedVehicles = { "Fuel-Based Motorcycle", "Electric Motorcycle", "Fuel-Based Car",
                "Electric Car", "Fuel-Based Truck" };
            const string k_ChooseYourVehicle = "Choose your vehicle: ";
            const int k_OptionsPerLine = 2;
            string vehicleChosen = ConnsoleUtil.ChooseOption(k_ChooseYourVehicle, supportedVehicles, k_OptionsPerLine);

            return vehicleChosen;
        }
    }
}
