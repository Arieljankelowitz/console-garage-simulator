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
            set { CurrentAirPressure = value; }
        }

        public float MaxAirPressure
        {
            get { return r_MaxAirPressure; }


        }
        public Wheel(string i_ManufacturName, float i_MaxAirPressure, float i_CurrentAirPressure)
        {

            r_ManufacturName = i_ManufacturName;
            // maybe to do a and exception check here
            r_MaxAirPressure = i_MaxAirPressure;

            if (i_CurrentAirPressure > r_MaxAirPressure)
            {
                //need to check if this is the correct exception and how to handle them
                throw new ArgumentException("Current air pressure cannot exceed maximum air pressure.");
            }
            m_CurrentAirPressure = i_CurrentAirPressure;
        }

        internal void PumpTire()
        {
            m_CurrentAirPressure = r_MaxAirPressure;
        }

        public override string ToString()
        {
            return string.Format("Manufacturer Name: {0}, Current Air Pressure: {1}, Max Air Pressure: {2}",
                r_ManufacturName, m_CurrentAirPressure, r_MaxAirPressure);
        }
    }
}
