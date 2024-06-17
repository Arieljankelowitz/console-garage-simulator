using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal class Truck : Vehicle
    {
        private bool m_ContainsToxins;
        private readonly float r_CargoTankVolume;
        private const int k_NumOfWheels = 12;
        private const int k_MaxAirPressure = 28;
        private const eFuelType k_TruckFuelType = eFuelType.Soler;

        public Truck(bool i_ContainsToxins, float i_CargoTankVolume, eEngineType i_EngineType, string i_LicenseNumber,
                                        string i_ModelName, string i_Owner, string i_PhoneNumber, List<(string manufacturerName, float currentAirPressure)> i_WheelDataList, object i_Engine)
                                : base(i_EngineType, i_LicenseNumber, i_ModelName, i_Owner, i_PhoneNumber, i_WheelDataList, k_NumOfWheels, k_MaxAirPressure, i_Engine)
        {
            m_ContainsToxins = i_ContainsToxins;
            r_CargoTankVolume = i_CargoTankVolume;
        }

        internal override void FillUp(float i_AmountToFill)
        {

            (Engine as FuelEngine)?.FillUp(i_AmountToFill, k_TruckFuelType);

            CalculateRemainingEnergy();
        }
 
        public override string ToString()
        {
            string baseInfo = base.ToString();
            string output = string.Format(@"
    Truck Specs
        Contains Toxins: {0}
        Cargo Tank Volume: {1} liters
{2}", m_ContainsToxins, r_CargoTankVolume, baseInfo);

            return output;
        }

    }


}
