using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class Utils
    {
        internal static List<Wheel> InitializeWheels(Wheel i_Tire, int i_NumOfTires)
        {
            List<Wheel> wheelsList = new List<Wheel>();
            for (int i = 0; i < i_NumOfTires; i++)
            {
                wheelsList.Add(i_Tire);
            }

            return wheelsList;
        }
    }
}
