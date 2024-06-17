using System;
using System.Collections.Generic;
using Ex03.GarageLogic;

namespace Ex_03
{
    internal class FrontOffice
    {
        private readonly Garage r_Garage;
        public bool BeingServiced { get; set; }
        public FrontOffice()
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

            List<string> vehicleLicenses;


            try
            {
                eVehicleStatus filterStatus = ConnsoleUtil.ParseEnum<eVehicleStatus>(filter.Replace(" ", ""));
                vehicleLicenses = r_Garage.FilterVehicles(filterStatus);
            }
            catch (Exception ex)
            {
                vehicleLicenses = r_Garage.VehiclesInGarage;
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

            const string k_SelectTheNewStatus = "select the new status";
            string[] statusOptions = { "In Repair", "Repaired", "Payed For" };
            string updatedStatusString = ConnsoleUtil.ChooseOption(k_SelectTheNewStatus, statusOptions).Replace(" ", "");
            eVehicleStatus updatedStatus = ConnsoleUtil.ParseEnum<eVehicleStatus>(updatedStatusString);

            r_Garage.ChangeStatus(updatedStatus, vehicleLicense);

            Console.Clear();
            Console.WriteLine("Status Updated.");
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
            catch(Exception ex)
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
            
            
            try
            {
                string litersGivenToAdd = Console.ReadLine();
                float.TryParse(litersGivenToAdd, out float litersToAdd);
                r_Garage.Refuel(vehicleLicense, chosenFuel, litersToAdd);
            }
            catch(Exception ex)
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
                    r_Garage.CreateNewVehicle(carColor, carDoors, eEngineType.Electric, i_LicenseNumber, vehicleModel, ownerName, phoneNumber,
                      wheelDataList, currentBatteryLife);

                    vehicleRegistered();
                }
                else
                {

                    float currentFuel = ConnsoleUtil.CurrentFuelAmount();

                    r_Garage.CreateNewVehicle(carColor, carDoors, eEngineType.Fuel, i_LicenseNumber, vehicleModel, ownerName, phoneNumber,
                        wheelDataList, i_CurrentFuel: currentFuel);

                    vehicleRegistered();
                }
            }
            else if (i_VehicleType.Contains("Motorcycle"))
            {
                numOfWheelsToAdd = 2;
                wheelDataList = collectWheelData(numOfWheelsToAdd);

                (eLicenseType LicenseType, int EngineVolume) motorcycleInfo = ConnsoleUtil.NewMotorcycle();
                if (i_VehicleType.Contains("Electric"))
                {
                    float currentBatteryLife = ConnsoleUtil.NewElectric();
                    r_Garage.CreateNewVehicle(motorcycleInfo.LicenseType, motorcycleInfo.EngineVolume, eEngineType.Electric, i_LicenseNumber,
            vehicleModel, ownerName, phoneNumber, wheelDataList, currentBatteryLife);

                    vehicleRegistered();
                }
                else
                {
                    float currentFuel = ConnsoleUtil.CurrentFuelAmount();
                    r_Garage.CreateNewVehicle(motorcycleInfo.LicenseType, motorcycleInfo.EngineVolume, eEngineType.Fuel, i_LicenseNumber, vehicleModel, ownerName, phoneNumber,
                         wheelDataList, i_CurrentFuel: currentFuel);

                    vehicleRegistered();
                }
            }
            else if (i_VehicleType.Contains("Truck"))
            {
                numOfWheelsToAdd = 12;
                wheelDataList = collectWheelData(numOfWheelsToAdd);

                (bool containsDangerousMaterials, float cargoTankVolume) truckInfo = ConnsoleUtil.NewTruck();
                float currentFuel = ConnsoleUtil.CurrentFuelAmount();
                r_Garage.CreateNewVehicle(truckInfo.containsDangerousMaterials, truckInfo.cargoTankVolume, eEngineType.Fuel, i_LicenseNumber, vehicleModel, ownerName, phoneNumber,
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
