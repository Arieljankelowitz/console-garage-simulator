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
            if (i_RemainingBatteryLife > i_maxBatteryLife)
            {
                throw new ValueOutOfRangeException("Exceeded the Maximum battery");
            }
            m_RemainingBatteryLife = i_RemainingBatteryLife;

        }

        public void FillUp(float i_AmountOfCharge)
        {
            if ((i_AmountOfCharge + m_RemainingBatteryLife) > r_MaxBatteryLife)
            {
                throw new ArgumentOutOfRangeException("you trying to charge to much");
            }
            m_RemainingBatteryLife += i_AmountOfCharge;
        }
        private string convertToHoursAndMinutes(float i_Hours)
        {
            int hours = (int)i_Hours;
            int minutes = (int)((i_Hours - hours) * 60);
            return string.Format("{0} hours and {1} minutes", hours, minutes);
        }
        public override string ToString()
        {
            return string.Format(
                "Electric Engine:\n    Remaining Battery Life: {0}\n    Max Battery Life: {1}",
                convertToHoursAndMinutes(m_RemainingBatteryLife),
                convertToHoursAndMinutes(r_MaxBatteryLife));
        }
    }

}
