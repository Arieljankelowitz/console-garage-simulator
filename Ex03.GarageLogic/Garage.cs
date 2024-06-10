using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class Garage
    {
        private readonly Dictionary<string, Vehicle> r_VehiclesInGarage;
        public Garage() 
        { 
            r_VehiclesInGarage = new Dictionary<string, Vehicle>();
        }

        public void InsertVehicle(string i_Name, Vehicle i_Vehicle)
        {

        } 
    }
}
