﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class Truck : Vehicle
    {
        private bool m_ContainsToxins;
        private readonly float r_CargoTankVolume;
        public Truck(bool i_ContainsToxins, float i_CargoTankVolume, eEngineType i_EngineType, string i_LicenseNumber,
                                        string i_ModelName, string i_Owner, string i_PhoneNumber)
            : base(i_EngineType, i_LicenseNumber, i_ModelName, i_Owner, i_PhoneNumber, 
                   Utils.InitializeWheels(new Wheel("China", 28, 28), 12))
        {
            m_ContainsToxins = i_ContainsToxins;
            r_CargoTankVolume = i_CargoTankVolume;
            r_CargoTankVolume = i_CargoTankVolume;
        }

        internal override void FillUp()
        {
           
        }

    

    }
}