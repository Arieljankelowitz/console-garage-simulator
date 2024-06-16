using System;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private readonly string r_ManufacturName;

        private float m_CurrentAirPressure;


        private readonly float r_MaxAirPressure;

        public string ManufacturName
        {
            get { return r_ManufacturName; }

        }

        public float CurrentAirPressure
        {
            get { return m_CurrentAirPressure; }
            set 
            {
                if (value > r_MaxAirPressure)
                {
                    throw new ArgumentException("Current air pressure cannot exceed maximum air pressure.");
                }

                m_CurrentAirPressure = value; 
            }
        }

        public float MaxAirPressure
        {
            get { return r_MaxAirPressure; }


        }
        public Wheel(string i_ManufacturName, float i_MaxAirPressure)
        {

            r_ManufacturName = i_ManufacturName;
            // maybe to do a and exception check here
            r_MaxAirPressure = i_MaxAirPressure;

        }

        internal void PumpTire()
        {
            m_CurrentAirPressure = r_MaxAirPressure;
        }


        public override string ToString()
        {
            return string.Format("Manufacturer: {0}, Max Air Pressure: {1}psi, Current Air Pressure: {2}psi", r_ManufacturName, r_MaxAirPressure, m_CurrentAirPressure);
        }
    }
}


