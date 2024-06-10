﻿using System;
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

        public Motorcycle(eLicenseType i_LicenseType, int i_EngineVolume, eEngineType i_EngineType, string i_LicenseNumber,
                                        string i_ModelName, string i_Owner, string i_PhoneNumber)
            : base(i_EngineType, i_LicenseNumber, i_ModelName, i_Owner, i_PhoneNumber, 
                                Utils.InitializeWheels(new Wheel("China", 31, 31), 2))
        {
            r_EngineVolume = i_EngineVolume;
            r_LicenseType = i_LicenseType;
        }

        internal override void FillUp()
        {

        }
    }
}