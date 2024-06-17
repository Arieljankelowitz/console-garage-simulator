using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private readonly Dictionary<string, Vehicle> r_VehiclesInGarage;

        public List<string> VehiclesInGarage
        {
            get
            {
                return new List<string>(r_VehiclesInGarage.Keys);
            }
        } 

        public Garage() 
        { 
            r_VehiclesInGarage = new Dictionary<string, Vehicle>();
        }

        private void insertVehicle(string i_LicenseNumber, Vehicle i_Vehicle)
        {
            r_VehiclesInGarage.Add(i_LicenseNumber, i_Vehicle);
        } 

        public bool IsVehicleInGarage(string i_LicenseNumber)
        {
            bool isVehicleInGarage = r_VehiclesInGarage.ContainsKey(i_LicenseNumber);

            return isVehicleInGarage;
        }

        //Car
        public void CreateNewVehicle(eColor i_Color, int i_NumOfDoors, eEngineType i_EngineType, string i_LicenseNumber,
                             string i_ModelName, string i_Owner, string i_PhoneNumber, List<(string manufacturerName, float currentAirPressure)> i_WheelDataList,
                             float i_CurrentBatteryLife = 0, float i_CurrentFuel = 0)
        {
            object carEngine = null;

            if (i_EngineType is eEngineType.Electric)
            {
                const float k_MaxBatteryLife = 3.5f;
                carEngine = new ElectricEngine(k_MaxBatteryLife, i_CurrentBatteryLife);
            }
            else if(i_EngineType is eEngineType.Fuel)
            {
                const float k_MaxFuel = 45f;
                const eFuelType k_FuelType = eFuelType.Octane95;
               
                carEngine = new FuelEngine(k_FuelType, i_CurrentFuel, k_MaxFuel);
            }

            Vehicle newVehicle = new Car(i_Color, i_NumOfDoors, i_EngineType, i_LicenseNumber, i_ModelName, i_Owner, i_PhoneNumber, i_WheelDataList, carEngine);
            insertVehicle(newVehicle.LicenseNumber, newVehicle);
        }

        //Truck
        public void CreateNewVehicle(bool i_ContainsToxins, float i_CargoTankVolume, eEngineType i_EngineType, string i_LicenseNumber, string i_ModelName, string i_Owner, string i_PhoneNumber,
            List<(string manufacturerName, float currentAirPressure)> i_WheelDataList, float i_CurrentFuel)
        {
            const float k_MaxFuel = 120f;
            const eFuelType k_FuelType = eFuelType.Soler;
            object truckEngine = new FuelEngine(k_FuelType, i_CurrentFuel, k_MaxFuel); ;
            Vehicle newVehicle = new Truck(i_ContainsToxins, i_CargoTankVolume, i_EngineType, i_LicenseNumber, i_ModelName, i_Owner, i_PhoneNumber, i_WheelDataList, truckEngine);
            insertVehicle(newVehicle.LicenseNumber, newVehicle);
        }

        //Motorcycle
        public void CreateNewVehicle(eLicenseType i_LicenseType, int i_EngineVolume, eEngineType i_EngineType, string i_LicenseNumber,
            string i_ModelName, string i_Owner, string i_PhoneNumber, List<(string manufacturerName, float currentAirPressure)> i_WheelDataList, float i_CurrentBatteryLife = 0, float i_CurrentFuel = 0)
        {
            object motorcycleEngine = null;

            if (i_EngineType is eEngineType.Electric)
            {
                const float k_MaxBatteryLife = 2.5f;
                motorcycleEngine = new ElectricEngine(k_MaxBatteryLife, i_CurrentBatteryLife);
            }
            else if (i_EngineType is eEngineType.Fuel)
            {
                const float k_MaxFuel = 5.5f;
                const eFuelType k_FuelType = eFuelType.Octane98;
                motorcycleEngine = new FuelEngine(k_FuelType, i_CurrentFuel, k_MaxFuel);
            }

            Vehicle newVehicle = new Motorcycle(i_LicenseType, i_EngineVolume, i_EngineType, i_LicenseNumber, i_ModelName, i_Owner, i_PhoneNumber, i_WheelDataList, motorcycleEngine);
            insertVehicle(newVehicle.LicenseNumber, newVehicle);
        }

        private Vehicle GetVehicle(string i_LicenseNumber)
        {
            Vehicle vehicle = r_VehiclesInGarage[i_LicenseNumber];

            return vehicle;
        }

        public List<string> FilterVehicles(eVehicleStatus i_Status)
        {
            List<string> listOfFilteredLicenseNumbers = new List<string>();

            foreach (Vehicle vehicle in r_VehiclesInGarage.Values)
            {
                if (vehicle.VehicleStatus == i_Status)
                {
                    listOfFilteredLicenseNumbers.Add(vehicle.LicenseNumber);
                }
            }
            

            return listOfFilteredLicenseNumbers;
        }
        
        public void ChangeStatus(eVehicleStatus i_NewStatus, string i_LicenseNumber)
        {
            r_VehiclesInGarage[i_LicenseNumber].VehicleStatus = i_NewStatus;
        }

        public void PumpTires(string i_LicenseNumber)
        {
            r_VehiclesInGarage[i_LicenseNumber].PumpTires();
        }

        public void Refuel(string i_LicenseNumber, eFuelType i_FuelType, float i_LitersToAdd)
        {
            if (!r_VehiclesInGarage.ContainsKey(i_LicenseNumber))
            {
                throw new ArgumentException("Vehicle not found in the garage.");
            }

            Vehicle vehicle = r_VehiclesInGarage[i_LicenseNumber];

            if (!(vehicle.EngineType is eEngineType.Fuel))
            {
                throw new ArgumentException("This vehicle does not have an fuel engine.");
            }

            if (i_FuelType != vehicle.FuelType)
            {
                throw new ArgumentException("Wrong fuel type");
            }

            vehicle.FillUp(i_LitersToAdd);

            
        }

        public void ReCharge(string i_LicenseNumber, int i_MinutestoCharge)
        {
            
            if (!r_VehiclesInGarage.ContainsKey(i_LicenseNumber))
            {
                throw new ArgumentException("Vehicle not found in the garage.");
            }

            Vehicle vehicle = r_VehiclesInGarage[i_LicenseNumber];

            if (!(vehicle.EngineType is eEngineType.Electric))
            {
                throw new ArgumentException("This vehicle does not have an electric engine.");
            }

            float chargeToAdd = (float)i_MinutestoCharge / 60;

            vehicle.FillUp(chargeToAdd);
            
        }

        public List<Wheel> CreateListOfWheels(List<(string manufacturerName, float currentAirPressure)> wheelDataList)
        {
            List<Wheel> wheels = new List<Wheel>();

            foreach (var (manufacturerName, currentAirPressure) in wheelDataList)
            {
                Wheel wheel = new Wheel(manufacturerName, currentAirPressure);
                wheels.Add(wheel);
            }

            return wheels;
        }

        public string DisplayVehicle(string i_LicenseNumber)
        {
            if (r_VehiclesInGarage.TryGetValue(i_LicenseNumber, out Vehicle vehicle))
            {
                return vehicle.ToString();
            }
            else
            {
                return "Vehicle not found.";
            }
        }


        
    }
}
