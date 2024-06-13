using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal class Car : Vehicle
    {
        private readonly int r_NumberOfDoors;
        private eColor m_CarColor;
        private Engine r_Engine;

        public Car(eColor i_Color, int i_NumOfDoors, eEngineType i_EngineType, string i_LicenseNumber,
                                        string i_ModelName, string i_Owner, string i_PhoneNumber)
            : base(i_EngineType, i_LicenseNumber, i_ModelName, i_Owner, i_PhoneNumber, 
                  Utils.InitializeWheels(new Wheel("China", 33, 33), 4))
        {
            m_CarColor = i_Color;
            r_NumberOfDoors = i_NumOfDoors;
        }
        internal override void FillUp()
        {

        }

        public override string ToString()
        {
            string output = string.Format(@"
    Car Specs
        Color: {0},
        Number of Doors: {1}
", m_CarColor, r_NumberOfDoors); 

            return output;
        }
    }
}
