﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private readonly Dictionary<string, Vehicle> r_VehiclesInGarage;

        public List<string> VehiclesInGarage { get { return new List<string>(r_VehiclesInGarage.Keys); } } 

        public Garage() 
        { 
            r_VehiclesInGarage = new Dictionary<string, Vehicle>();
        }

        private void InsertVehicle(string i_LicenseNumber, Vehicle i_Vehicle)
        {
            r_VehiclesInGarage.Add(i_LicenseNumber, i_Vehicle);
        } 

        public bool IsVehicleInGarage(string i_LicenseNumber)
        {
            bool isVehicleInGarage = r_VehiclesInGarage.ContainsKey(i_LicenseNumber);

            return isVehicleInGarage;
        }

        public void CreateNewVehicle(eColor i_Color, int i_NumOfDoors, eEngineType i_EngineType, string i_LicenseNumber, string i_ModelName, string i_Owner, string i_PhoneNumber, 
            float i_MaxBatteryLife = 0, float i_CurrentBatteryLife = 0, eFuelType i_FuelType = eFuelType.Octane96, float i_CurrentFuel = 0, float i_MaxFuel = 0)
        {
            object carEngine = null;

            if (i_EngineType is eEngineType.Electric)
            {
                carEngine = new ElectricEngine(i_MaxBatteryLife, i_CurrentBatteryLife);
            }
            else if(i_EngineType is eEngineType.Fuel)
            {
                carEngine = new FuelEngine(i_FuelType, i_CurrentFuel, i_MaxFuel);
            }

            Vehicle newVehicle = new Car(i_Color, i_NumOfDoors, i_EngineType, i_LicenseNumber, i_ModelName, i_Owner, i_PhoneNumber, carEngine);
            InsertVehicle(newVehicle.LicenseNumber, newVehicle);
        }

        public void CreateNewVehicle(bool i_ContainsToxins, float i_CargoTankVolume, eEngineType i_EngineType, string i_LicenseNumber, string i_ModelName, string i_Owner, string i_PhoneNumber)
        {
            Vehicle newVehicle = new Truck(i_ContainsToxins, i_CargoTankVolume, i_EngineType, i_LicenseNumber, i_ModelName, i_Owner, i_PhoneNumber);

            InsertVehicle(newVehicle.LicenseNumber, newVehicle);
        }

        public void CreateNewVehicle(eLicenseType i_LicenseType, int i_EngineVolume, eEngineType i_EngineType, string i_LicenseNumber, string i_ModelName, string i_Owner, string i_PhoneNumber)
        {
            Vehicle newVehicle = new Motorcycle(i_LicenseType, i_EngineVolume, i_EngineType, i_LicenseNumber, i_ModelName, i_Owner, i_PhoneNumber);

            InsertVehicle(newVehicle.LicenseNumber, newVehicle);
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

        }

        public void ReCharge(string i_LicenseNumber, int i_MinutestoCharge)
        {

        }

        public string DisplayVehicle(string i_LicenseNumber)
        {
            Vehicle vehicle = GetVehicle(i_LicenseNumber);

            return vehicle.ToString(); 
        }
    }
}
