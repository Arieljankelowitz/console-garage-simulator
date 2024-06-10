using System;


namespace Ex03.GarageLogic
{
    internal class ElectricEngine
    {
        public float m_RemainingBatteryLife;
        public readonly float r_MaxBatteryLife;

        public float MaxBatteryLife { get { return r_MaxBatteryLife; } }
        public float RemainingBatteryLife
        {
            get { return m_RemainingBatteryLife; }
            set { m_RemainingBatteryLife = value; }
        }

        public ElectricEngine(float i_maxBatteryLife, float i_RemainingBatteryLife)
        {
            r_MaxBatteryLife = i_maxBatteryLife;
            m_RemainingBatteryLife = i_RemainingBatteryLife;

        }

        public void FillUp(float i_AmountOfCharge)
        {
            if((i_AmountOfCharge + m_RemainingBatteryLife) > r_MaxBatteryLife)
            {
                throw new ArgumentOutOfRangeException("you trying to charge to much");
            }
            m_RemainingBatteryLife += i_AmountOfCharge;
        }
    }

}
