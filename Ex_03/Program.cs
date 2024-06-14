

using Ex_03;
using Ex03.GarageLogic;
using System;
using System.Runtime.InteropServices;


namespace GarageLogic

{
    public class Program
    {
        public static void Main()
        {
            
            run();
        }

        public static void run()
        {
            FrontOffice frontOffice = new FrontOffice();

            frontOffice.Welcome();
            while (frontOffice.BeingServiced) 
            {
               string chosenService = frontOffice.ChooseService();
               Console.Clear();

                switch (chosenService)
                {
                    case "1. Insert a new vehicle":
                        frontOffice.InsertVehicle();
                        break;
                    case "2. Display vehicles":
                        frontOffice.DisplayVehicles();
                        break;
                    case "3. Change vehicle status":
                        frontOffice.ChangeStatus();
                        break;
                    case "4. Inflate vehicle tires":
                        frontOffice.FillTires();
                        break;
                    case "5. Refuel vehicle":
                        frontOffice.Refuel();
                        break;
                    case "6. Recharge vehicle":
                        frontOffice.Recharge();
                        break;
                    case "7. Display vehicle info":
                        frontOffice.DisplayVehicleInfo();
                        break;
                    case "8. Leave Garage":
                        frontOffice.BeingServiced = false;
                        break;
                    default:
                        Console.WriteLine("Invalid Service");
                        break;
                }
            }

            frontOffice.Goodbye();
        }
    }
}
