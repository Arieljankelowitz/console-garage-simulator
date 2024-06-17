using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class FuelEngine
    {
        private readonly eFuelType r_FuelType;
        private float m_CurrentFuel;
        private readonly float r_MaxFuel;

        public eFuelType FuelType
        {
            get { return r_FuelType; }

        }
        public float MaxFuel { get { return r_MaxFuel; } }
        public float CurrentFuel
        {
            get { return m_CurrentFuel; }
            set { m_CurrentFuel = value; }
        }

        public FuelEngine(eFuelType i_FuelType, float i_CurrentFuel, float i_MaxFuel)
        {
            r_FuelType = i_FuelType;
            if(i_CurrentFuel > i_MaxFuel)
            {
                throw new ValueOutOfRangeException("Exceeded the Maximum Fuel");
            }
            m_CurrentFuel = i_CurrentFuel;
            r_MaxFuel = i_MaxFuel;
        }

        public void FillUp(float i_FuelAmount, eFuelType i_FuelType)
        {
            if (r_FuelType != FuelType)
            {
                throw new InvalidOperationException("Fuel don't match.");
            }
            if (m_CurrentFuel == r_MaxFuel)
            {
                throw new InvalidOperationException("Fuel tank is already full.");
            }

            if (i_FuelAmount <= 0)
            {
                throw new ArgumentException("Invalid amount of fuel.");
            }

            m_CurrentFuel += i_FuelAmount;

            if (m_CurrentFuel > r_MaxFuel)
            {
                m_CurrentFuel = r_MaxFuel;
            }

        }

        public override string ToString()
        {
            return string.Format(
                "Fuel Engine:\n    Fuel Type: {0}\n    Current Fuel: {1} liters\n    Max Fuel Capacity: {2} liters",
                r_FuelType, m_CurrentFuel, r_MaxFuel);
        }
    }
}
