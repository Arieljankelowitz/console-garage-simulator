using System.Collections.Generic;

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
        private readonly eEngineType r_EngineType;
        private eVehicleStatus m_VehicleStaus;


        public string ModelName { get { return r_ModelName; } }
        public string LicenseNumber { get { return r_LicenseNumber; } }
        public string Owner { get { return m_Owner; } set { m_Owner = value; } }
        public string PhoneNumber { get { return m_PhoneNumber; } set { m_PhoneNumber = value; } }
        public float RemainingEnergy { get { return m_RemainingEnergy; } set { m_RemainingEnergy = value; } }
        public eVehicleStatus VehicleStatus { get { return m_VehicleStaus; } set { m_VehicleStaus = value; } }
        public eEngineType EngineType { get { return r_EngineType; } }

        public Vehicle()
        {

        }

        internal void PumpTires()
        {
            foreach (Wheel wheel in r_Wheels)
            {
                wheel.PumpTire();
            }
        }

        internal abstract void FillUp();

    }
}
