using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    internal abstract class Vehicle
    {
        private readonly string r_ModelName;
        private readonly string r_LicenseNumber;

        private string m_Owner;
        private string m_PhoneNumber;

        private float m_RemainingEnergy;
        private readonly List<Wheel> r_Wheels;
        private readonly object r_Engine;
        private readonly eEngineType r_EngineType;
        private eVehicleStatus m_VehicleStaus;


        public string LicenseNumber { get { return r_LicenseNumber; } }
        public eVehicleStatus VehicleStatus { get { return m_VehicleStaus; } set { m_VehicleStaus = value; } }
        public eEngineType EngineType { get { return r_EngineType; } }
        public object Engine { get { return r_Engine; } }

        public eFuelType FuelType { get { return (r_Engine as FuelEngine).FuelType; } }

        public Vehicle(eEngineType i_EngineType, string i_LicenseNumber, string i_ModelName, string i_Owner, string i_PhoneNumber,
               List<(string manufacturerName, float currentAirPressure)> i_WheelDataList, int i_NumOfWheels, float i_MaxAirPressure, object i_Engine)
        {
            r_ModelName = i_ModelName;
            r_LicenseNumber = i_LicenseNumber;
            r_EngineType = i_EngineType;
            m_Owner = i_Owner;
            m_PhoneNumber = i_PhoneNumber;
            m_VehicleStaus = eVehicleStatus.InRepair;
            r_Engine = i_Engine;
            r_Wheels = new List<Wheel>(i_NumOfWheels);
            AddWheels(i_WheelDataList, i_MaxAirPressure, i_NumOfWheels);
            calculateRemainingEnergy();
        }

        internal void PumpTires()
        {
            foreach (Wheel wheel in r_Wheels)
            {
                wheel.PumpTire();
            }
        }
        internal void AddWheels(List<(string manufacturerName, float currentAirPressure)> i_WheelDataList, float i_MaxAirPressure, int i_NumOfWheelsToAdd)
        {
            if (i_WheelDataList == null)
            {
                throw new ArgumentException("The wheel data list does not contain enough elements.");
            }

            for (int i = 0; i < i_NumOfWheelsToAdd; i++)
            {
                var wheelData = i_WheelDataList.Count > 1 ?  i_WheelDataList[i] : i_WheelDataList[0];

               
                Wheel newWheel = new Wheel(wheelData.manufacturerName, i_MaxAirPressure)
                {
                    CurrentAirPressure = wheelData.currentAirPressure,
                };

                r_Wheels.Add(newWheel);
            }
        }

        internal void calculateRemainingEnergy()
        {
            float currentEnergy;
            float maxEnergy;
            if(r_EngineType is eEngineType.Fuel)
            {
                FuelEngine engine = r_Engine as FuelEngine;
                currentEnergy = engine.CurrentFuel;
                maxEnergy = engine.MaxFuel;
            }
            else
            {
                ElectricEngine engine = r_Engine as ElectricEngine;
                currentEnergy = engine.RemainingBatteryLife;
                maxEnergy = engine.MaxBatteryLife;
            }

            m_RemainingEnergy = currentEnergy / maxEnergy;
        }
        internal abstract void FillUp(float i_AmountToFill);

        public override string ToString()
        {
            StringBuilder wheelsInfo = new StringBuilder();
            foreach (Wheel wheel in r_Wheels)
            {
                wheelsInfo.AppendLine(wheel.ToString());
            }

            string engineInfo = r_Engine != null ? r_Engine.ToString() : "None";

            string output = string.Format(@"
    License Number: {0}
    Model: {1}
    Owner: {2}
    Status: {3}
    Remaining Energy: {4:P2}
    Engine Type: {5}
    Engine: {6}
    Wheels: 
{7}", r_LicenseNumber, r_ModelName, m_Owner, m_VehicleStaus, m_RemainingEnergy, r_EngineType, engineInfo, wheelsInfo);

            return output;
        }

    }
}
