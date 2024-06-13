using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex_03
{
    internal class ConnsoleUtil
    {
        public static string ChooseOption(string i_message, string[] i_Options, int i_OptionsPerLine = 4, int i_StartX = 0)
        {
            int i_SpacingPerLine = i_Options.Max(option => option.Length) + 2;
            int startY = Console.CursorTop + 2;

            int currentSelectedOption = 0;

            ConsoleKey key;
            Console.CursorVisible = false;
            Console.WriteLine(i_message);

            do
            {
                for (int i = 0; i < i_Options.Length; i++)
                {
                    Console.SetCursorPosition(i_StartX + (i % i_OptionsPerLine) * i_SpacingPerLine, startY + i / i_OptionsPerLine);

                    if (i == currentSelectedOption)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }

                    Console.WriteLine(i_Options[i]);
                    Console.ResetColor();
                }

                key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.LeftArrow:
                        if (currentSelectedOption % i_OptionsPerLine > 0)
                            currentSelectedOption--;
                        break;
                    case ConsoleKey.RightArrow:
                        if (currentSelectedOption % i_OptionsPerLine < i_OptionsPerLine - 1)
                            currentSelectedOption++;
                        break;
                    case ConsoleKey.UpArrow:
                        if (currentSelectedOption >= i_OptionsPerLine)
                            currentSelectedOption -= i_OptionsPerLine;
                        break;
                    case ConsoleKey.DownArrow:
                        if (currentSelectedOption + i_OptionsPerLine < i_Options.Length)
                            currentSelectedOption += i_OptionsPerLine;
                        break;
                }
            } while (key != ConsoleKey.Enter);

            Console.CursorVisible = true;

            return i_Options[currentSelectedOption];
        }

        internal static (float maxBatteryLife, float currentBatteryLife) NewElectric()
        {
            (float maxBatteryLife, float currentBatteryLife) electricProperties = (0.0f, 0.0f);

            bool validInput = false;
            while (!validInput)
            {
                Console.WriteLine("Please enter the Max battery life in hours (Separated by a ':' Eg 2 hours and 24 min is 2:24): ");
                string maxTime = Console.ReadLine();

                try
                {
                    electricProperties.maxBatteryLife = convertTimeToHours(maxTime);
                    validInput = true; 
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            validInput = false; 
            while (!validInput)
            {
                Console.WriteLine("Please enter the current battery life in hours (Separated by a ':' Eg 2 hours and 24 min is 2:24): ");
                string currentTime = Console.ReadLine();

                try
                {
                    electricProperties.currentBatteryLife = convertTimeToHours(currentTime);
                    validInput = true; 
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            return electricProperties;
        }



        private static float convertTimeToHours(string i_TimeInHours)
        {
            var parts = i_TimeInHours.Split(':');

            if (parts.Length != 2)
            {
                throw new FormatException("Invalid format. The correct format is 'hours:minutes'.");
            }
            if (!int.TryParse(parts[0], out int out_Hours))
            {
                throw new FormatException("Invalid format for hours. Please enter a valid integer.");
            }

            if (!int.TryParse(parts[1], out int out_Minutes))
            {
                throw new FormatException("Invalid format for minutes. Please enter a valid integer.");
            }

            if (out_Minutes < 0 || out_Minutes >= 60)
            {
                throw new FormatException("Minutes should be between 0 and 59.");
            }
            float timeInMin = out_Hours + (out_Minutes / 60f);
            return timeInMin;
        }


        internal static (string fuelType, float maxFuel, float currentFuel) NewFuel()
        {
            (string fuelType, float maxFuel, float currentFuel) engineData = ("", 0, 0);
            string message = "Select the correct type of fuel for your car";
            string[] fuelTypes = { "Soler", "Octane95", "Octane96", "Octane98" };

            engineData.fuelType = ChooseOption(message, fuelTypes);

            bool validInput = false;
            while (!validInput)
            {
                Console.WriteLine("Please enter the Max fuel capacity in liters: ");
                string maxFuelInput = Console.ReadLine();

                if (float.TryParse(maxFuelInput, out float out_maxFuel))
                {
                    engineData.maxFuel = out_maxFuel;
                    validInput = true;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
            }

            validInput = false;
            while (!validInput)
            {
                Console.WriteLine("Please enter the Current fuel amount in liters: ");
                string currentFuelInput = Console.ReadLine();

                if (float.TryParse(currentFuelInput, out float currentFuel))
                {
                    engineData.currentFuel = currentFuel;
                    validInput = true;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
            }

            return engineData;
        }



        internal static (eColor color, int numOfDoors) NewCar() 
        {
            string[] colorOptions = { "Yellow", "White", "Red", "Gray" };
            string colorMessage = "What color is your car? ";
            eColor carColor = ParseEnum<eColor>(ChooseOption(colorMessage, colorOptions));


            string[] doorOptions = { "2", "3", "4", "5" };
            string doorMessage = "How many doors does your car have? ";
            int numberOfDoors = int.Parse(ChooseOption(doorMessage, doorOptions));
                
            return (carColor, numberOfDoors);            
        }

        internal static void NewMotorcycle() { }

        internal static void NewTruck() { }

        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static void BlankSpace()
        {
            Console.WriteLine();
        }
    }
}
