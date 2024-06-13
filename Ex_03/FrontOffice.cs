using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Policy;
using Ex03.GarageLogic;

namespace Ex_03
{
    internal class FrontOffice
    {
        private Garage m_Garage;
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
                "4. Inflate vehicle tires", "5. Refuel vehicle", "6. Recharge vehicle", "7. Display vehicle info"};
            string message = "Please select a service.";

            string chosenOption = ConnsoleUtil.ChooseOption(message, serviceOptions, 1);

            return chosenOption;

        }

        internal void InsertVehicle()
        {
            string[] supportedVehicles = { "Fuel-Based Motorcycle", "Electric Motorcycle", "Fuel-Based Car", 
                "Electric Car", "Fuel-Based Truck" };
            string message = "Choose your vehicle";
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
            string vehicleLicesne = Console.ReadLine();


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
                    ConnsoleUtil.NewElectric();
                    m_Garage.CreateNewVehicle();
                } 
                else
                {
                    ConnsoleUtil.NewFuel();
                    m_Garage.CreateNewVehicle();
                }
            }
            else if(i_VehicleType.Contains("Motorcycle"))
            {
                ConnsoleUtil.NewMotorcycle();
                if (i_VehicleType.Contains("Electric"))
                {
                    ConnsoleUtil.NewElectric();
                    m_Garage.CreateNewVehicle();
                }
                else
                {
                    ConnsoleUtil.NewFuel();
                    m_Garage.CreateNewVehicle();
                }
            }
            else if(i_VehicleType.Contains("Truck"))
            {
                ConnsoleUtil.NewTruck();
                if (i_VehicleType.Contains("Electric"))
                {
                    ConnsoleUtil.NewElectric();
                    m_Garage.CreateNewVehicle();
                }
                else
                {
                    ConnsoleUtil.NewFuel();
                    m_Garage.CreateNewVehicle();
                }
            }
        }
    }
}
