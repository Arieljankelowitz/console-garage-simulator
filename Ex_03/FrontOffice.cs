using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex_03
{
    internal class FrontOffice
    {
        internal void Welcome()
        {
            string welcomeMsg = "Welcome to the Garage!";
            Console.WriteLine(welcomeMsg);

        }

        internal string ChooseService()
        {
            string[] serviceOptions = {"1. Insert a new vehicle", "2. Display vehicles", "3. Change vehicle status",
                "4. Inflate vehicle tires", "5. Refuel vehicle", "6. Recharge vehicle", "7. Display vehicle info"};
            string message = "Please select a service.";

            string chosenOption = ConnsoleUtil.ChooseOption(message, serviceOptions, 1);

            return chosenOption;

        }
    }
}
