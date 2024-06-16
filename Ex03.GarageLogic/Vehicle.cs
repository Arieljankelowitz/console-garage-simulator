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


        public string ModelName { get { return r_ModelName; } }
        public string LicenseNumber { get { return r_LicenseNumber; } }
        public string Owner { get { return m_Owner; } set { m_Owner = value; } }
        public string PhoneNumber { get { return m_PhoneNumber; } set { m_PhoneNumber = value; } }
        public float RemainingEnergy { get { return m_RemainingEnergy; } set { m_RemainingEnergy = value; } }
        public eVehicleStatus VehicleStatus { get { return m_VehicleStaus; } set { m_VehicleStaus = value; } }
        public eEngineType EngineType { get { return r_EngineType; } }
        public object Engine { get { return r_Engine; } }

        public Vehicle(eEngineType i_EngineType, string i_LicenseNumber, string i_ModelName, string i_Owner, string i_PhoneNumber,
               List<(string manufacturerName, float currentAirPressure)> i_WheelDataList, int i_NumOfWheels, float i_MaxAirPressure, object i_Engine)
        {
            r_ModelName = i_ModelName;
            r_LicenseNumber = i_LicenseNumber;
            r_EngineType = i_EngineType;
            m_Owner = i_Owner;
            m_PhoneNumber = i_PhoneNumber;
            m_VehicleStaus = eVehicleStatus.InRepair;
            m_RemainingEnergy = 100;
            r_Engine = i_Engine;
            r_Wheels = new List<Wheel>(i_NumOfWheels);
            AddWheels(i_WheelDataList, i_MaxAirPressure, i_NumOfWheels);
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

                try
                {
                    Wheel newWheel = new Wheel(wheelData.manufacturerName, i_MaxAirPressure)
                    {
                        CurrentAirPressure = wheelData.currentAirPressure,
                    };

                    r_Wheels.Add(newWheel);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                
                
            }
        }


        internal abstract void FillUp();

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
    Remaining Energy: {4}%
    Engine Type: {5}
    Engine: {6}
    Wheels: 
{7}", r_LicenseNumber, r_ModelName, m_Owner, m_VehicleStaus, m_RemainingEnergy, r_EngineType, engineInfo, wheelsInfo);

            return output;
        }

    }
}
