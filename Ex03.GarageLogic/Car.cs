using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal class Car : Vehicle
    {
        private readonly int r_NumberOfDoors;
        private eColor m_CarColor;
        private const int k_NumOfWheels = 4;
        private const int k_MaxAirPressure = 31;
        private const eFuelType k_CarFuelType = eFuelType.Octane95;

        public Car(eColor i_Color, int i_NumOfDoors, eEngineType i_EngineType, string i_LicenseNumber,
                      string i_ModelName, string i_Owner, string i_PhoneNumber, List<(string manufacturerName, float currentAirPressure)> i_WheelDataList, object i_Engine)
                      : base(i_EngineType, i_LicenseNumber, i_ModelName, i_Owner, i_PhoneNumber, i_WheelDataList, k_NumOfWheels, k_MaxAirPressure,  i_Engine)
        {
            m_CarColor = i_Color;
            r_NumberOfDoors = i_NumOfDoors;
        }

        internal override void FillUp(float i_AmountToFill)
        {
           if(EngineType is eEngineType.Fuel)
           {
                (Engine as FuelEngine).FillUp(i_AmountToFill, k_CarFuelType);
           }
           else
           {
                (Engine as ElectricEngine).FillUp(i_AmountToFill);
           }

           calculateRemainingEnergy();
        }

        public override string ToString()
        {
            string baseInfo = base.ToString();
            string output = string.Format(@"
    Car Specs
        Color: {0}
        Number of Doors: {1}
{2}", m_CarColor, r_NumberOfDoors, baseInfo);

            return output;
        }
    }
}
