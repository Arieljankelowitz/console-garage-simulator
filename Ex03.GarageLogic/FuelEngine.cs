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
        private readonly FuelType r_FuelType;
        private float m_CurrentFuel;
        private readonly float r_MaxFuel;

        public FuelType FuelType
        {
            get { return r_FuelType; }

        }
        public float MaxFuel { get { return r_MaxFuel; } }
        public float CurrentFuel
        {
            get { return m_CurrentFuel; }
            set { m_CurrentFuel = value; }
        }

        public FuelEngine(FuelType i_FuelType, float i_CurrentFuel, float i_MaxFuel)
        {
            r_FuelType = i_FuelType;
            m_CurrentFuel = i_CurrentFuel;
            r_MaxFuel = i_MaxFuel;
        }

        public void FillUp(float i_FuelAmount, FuelType i_FuelType)
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
    }
}
