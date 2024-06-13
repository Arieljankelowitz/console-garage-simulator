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

        public Garage() 
        { 
            r_VehiclesInGarage = new Dictionary<string, Vehicle>();
        }

        internal void InsertVehicle(string i_LicenseNumber, Vehicle i_Vehicle)
        {
            r_VehiclesInGarage.Add(i_LicenseNumber, i_Vehicle);
        } 

       internal bool IsVehicleInGarage(string i_LicenseNumber)
        {
            bool isVehicleInGarage = r_VehiclesInGarage.ContainsKey(i_LicenseNumber);

            return isVehicleInGarage;
        }

        public void CreateNewVehicle()
        {

        }

        internal Vehicle GetVehicle(string i_LicenseNumber)
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
    }
}
