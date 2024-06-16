using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class Motorcycle : Vehicle
    {
        private readonly int r_EngineVolume;
        private Object r_Engine;
        private readonly eLicenseType r_LicenseType;
        private const int k_NumOfWheels = 2;
        private const int k_MaxAirPressure = 33;

        public Motorcycle(eLicenseType i_LicenseType, int i_EngineVolume, eEngineType i_EngineType, string i_LicenseNumber,
                                        string i_ModelName, string i_Owner, string i_PhoneNumber, List<(string manufacturerName, float currentAirPressure)> i_WheelDataList, object i_Engine)
                                : base(i_EngineType, i_LicenseNumber, i_ModelName, i_Owner, i_PhoneNumber, i_WheelDataList, k_NumOfWheels, k_MaxAirPressure, i_Engine)
        {
            
            r_EngineVolume = i_EngineVolume;
            r_LicenseType = i_LicenseType;
        }

        internal override void FillUp(float i_AmountToFill)
        {

        }

        public override string ToString()
        {
            string baseInfo = base.ToString();
            string output = string.Format(@"
    Motorcycle Specs
        License Type: {0}
        Engine Volume: {1}cc
{2}", r_LicenseType, r_EngineVolume, baseInfo);

            return output;
        }
    }
}
