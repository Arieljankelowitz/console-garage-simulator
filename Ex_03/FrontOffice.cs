using System;
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
            int litersToAdd = int.Parse(Console.ReadLine()); //make this a util method so we can error check it
            
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
        }

        internal void Goodbye()
        {
            Console.Clear();
            Console.WriteLine("Thank you for coming to the Garage, have a good day! (press any key to quit)");
            Console.ReadLine();
        }

        private void registerNewVehicle(string i_VehicleType, string i_LicenseNumber)
        {
            Console.WriteLine("Thank you for choosing our Garage, lets start registering");
            Console.WriteLine("Please enter your name: ");
            string ownerName = Console.ReadLine();

            Console.WriteLine("Please enter your phone number: ");
            string phoneNumber = Console.ReadLine();

            Console.WriteLine("Please enter the {0}'s model: ", i_VehicleType);
            string vehicleModel = Console.ReadLine();

            if(i_VehicleType.Contains("Car"))
            {
                (eColor carColor, int carDoors) = ConnsoleUtil.NewCar();

                if(i_VehicleType.Contains("Electric"))
                {
                    (float maxBatteryLife, float currentBatteryLife) = ConnsoleUtil.NewElectric();
                    /*m_Garage.CreateNewVehicle();*/
                } 
                else
                {
                    ConnsoleUtil.NewFuel();
                    m_Garage.CreateNewVehicle(carColor, carDoors, eEngineType.Fuel, i_LicenseNumber, vehicleModel, ownerName, phoneNumber);
                    Console.Clear();
                    Console.WriteLine("Vehicle Reigstered!");
                    ConnsoleUtil.BlankSpace();
                }
            }
            else if(i_VehicleType.Contains("Motorcycle"))
            {
                ConnsoleUtil.NewMotorcycle();
                if (i_VehicleType.Contains("Electric"))
                {
                    (float maxBatteryLife, float currentBatteryLife) = ConnsoleUtil.NewElectric();
                   /* m_Garage.CreateNewVehicle();*/
                }
                else
                {
                    ConnsoleUtil.NewFuel();
                    /*m_Garage.CreateNewVehicle();*/
                }
            }
            else if(i_VehicleType.Contains("Truck"))
            {
                ConnsoleUtil.NewTruck();
                if (i_VehicleType.Contains("Electric"))
                {
                    (float maxBatteryLife, float currentBatteryLife) = ConnsoleUtil.NewElectric();
                   /* m_Garage.CreateNewVehicle();*/
                }
                else
                {
                    ConnsoleUtil.NewFuel();
                    /*m_Garage.CreateNewVehicle();*/
                }
            }
        }
    }
}
