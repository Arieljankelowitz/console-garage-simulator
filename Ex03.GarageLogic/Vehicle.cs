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

        public Vehicle(eEngineType i_EngineType, string i_LicenseNumber, string i_ModelName, string i_Owner, string i_PhoneNumber, List<Wheel> i_Wheels)
        {
            r_ModelName = i_ModelName;
            r_LicenseNumber = i_LicenseNumber;
            r_EngineType = i_EngineType;
            m_Owner = i_Owner;
            m_PhoneNumber = i_PhoneNumber;
            m_VehicleStaus = eVehicleStatus.InRepair;
            m_RemainingEnergy = 100;
            r_Wheels = i_Wheels;

        }

        internal void PumpTires()
        {
            foreach (Wheel wheel in r_Wheels)
            {
                wheel.PumpTire();
            }
        }

        internal abstract void FillUp();

        public override string ToString()
        {

            string output = string.Format(@"
    License Number: {0}
    Model: {1}
    Owner: {2}
    Status: {3}

    Specs:
        Tires: {4}, {5}psi (current: {6}psi)
        Engine Type: {7}
");

            return output;
        }

    }
}
