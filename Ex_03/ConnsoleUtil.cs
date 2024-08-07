﻿using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ex_03
{
    internal class ConnsoleUtil
    {
        public static string ChooseOption(string i_Message, string[] i_Options, int i_OptionsPerLine = 4, int i_StartX = 0)
        {
            int spacingPerLine = i_Options.Max(i_Option => i_Option.Length) + 2;
            int startY = Console.CursorTop + 2;

            int currentSelectedOption = 0;

            ConsoleKey key;
            Console.CursorVisible = false;
            Console.WriteLine(i_Message);

            do
            {
                for (int i = 0; i < i_Options.Length; i++)
                {
                    Console.SetCursorPosition(i_StartX + (i % i_OptionsPerLine) * spacingPerLine, startY + i / i_OptionsPerLine);

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

        internal static float NewElectric()
        {

            float currentBatteryLife = 0f;
            bool validInput = false;
            while (!validInput)
            {
                Console.WriteLine("Please enter the current battery life in hours (Separated by a ':' Eg 2 hours and 24 min is 2:24): ");
                string currentTime = Console.ReadLine();

                try
                {
                    currentBatteryLife = convertTimeToHours(currentTime);
                    validInput = true; 
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            return currentBatteryLife;
        }



        private static float convertTimeToHours(string i_TimeInHours)
        {
            var parts = i_TimeInHours.Split(':');

            if (parts.Length != 2)
            {
                throw new FormatException("Invalid format. The correct format is 'hours:minutes'.");
            }
            if (!int.TryParse(parts[0], out int hours))
            {
                throw new FormatException("Invalid format for hours. Please enter a valid integer.");
            }

            if (!int.TryParse(parts[1], out int minutes))
            {
                throw new FormatException("Invalid format for minutes. Please enter a valid integer.");
            }

            if (minutes < 0 || minutes >= 60)
            {
                throw new FormatException("Minutes should be between 0 and 59.");
            }
            float timeInMin = hours + minutes / 60f;
            return timeInMin;
        }


        internal static float CurrentFuelAmount()
        {

            float currentFuel = 0f;
            bool validInput = false;
            while (!validInput)
            {
                Console.WriteLine("Please enter the Current fuel amount in liters: ");
                string currentFuelInput = Console.ReadLine();

                if (float.TryParse(currentFuelInput, out float currentFuelParsed))
                {
                    currentFuel = currentFuelParsed;
                    validInput = true;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
            }

            return currentFuel;
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

        internal static ( eLicenseType LicenseType, int EngineVolume) NewMotorcycle()
        {
            string[] licenseTypeOptions = { "A", "A1", "AA", "B1" };
            const string k_LicenseTypeOfYourMotorcycle = "What is the license type of your motorcycle? ";
            eLicenseType licenseType = ParseEnum<eLicenseType>(ChooseOption(k_LicenseTypeOfYourMotorcycle, licenseTypeOptions));

            const string k_TheEngineVolumeOfYourMotorcycle = "What is the engine volume of your motorcycle? ";
            Console.WriteLine(k_TheEngineVolumeOfYourMotorcycle);
            int engineVolume;
            while (!int.TryParse(Console.ReadLine(), out engineVolume) || engineVolume <= 0)
            {
                Console.WriteLine("Please enter a valid positive integer for the engine volume.");
            }

            return (licenseType, engineVolume);
        }

        internal static (bool containsDangerousMaterials, float cargoTankVolume) NewTruck()
        {
            
            string[] dangerousMaterialsOptions = { "yes", "no" };
            string dangerousMaterialsMessage = "Does the truck contain dangerous materials? ";
            bool containsDangerousMaterials = ChooseOption(dangerousMaterialsMessage, dangerousMaterialsOptions, 2) == "yes";
            

            string cargoTankVolumeMessage = "What is the cargo tank volume of your truck? ";
            Console.WriteLine(cargoTankVolumeMessage);
            float cargoTankVolume;
            while (!float.TryParse(Console.ReadLine(), out cargoTankVolume) || cargoTankVolume <= 0)
            {
                Console.WriteLine("Please enter a valid positive float value for the cargo tank volume.");
            }

            return (containsDangerousMaterials, cargoTankVolume);
        }



        internal static (string manufacturerName, float currentAirPressure) CollectUniformWheelData()
        {
            while (true)
            {
                try
                {
                    Console.Write("Enter manufacturer name for the wheels: ");
                    string manufacturerName = Console.ReadLine();

                    Console.Write("Enter current air pressure for the wheels: ");
                    if (!float.TryParse(Console.ReadLine(), out float currentAirPressure))
                    {
                        throw new ArgumentException("Invalid input for air pressure. Please enter a valid number.");
                    }

                    return (manufacturerName, currentAirPressure);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}. Please try again.");
                }
            }
        }

       internal static List<(string manufacturerName, float currentAirPressure)> CollectIndividualWheelData(int i_NumOfWheelsToAdd)
        {
            while (true)
            {
                try
                {

                    var wheelDataList = new List<(string, float)>();

                    for (int i = 0; i < i_NumOfWheelsToAdd; i++)
                    {
                        Console.WriteLine($"Enter details for wheel {i + 1}:");

                        Console.Write("Manufacturer name: ");
                        string manufacturerName = Console.ReadLine();

                        Console.Write("Current air pressure: ");
                        if (!float.TryParse(Console.ReadLine(), out float currentAirPressure))
                        {
                            throw new ArgumentException("Invalid input for air pressure. Please enter a valid number.");
                        }

                        wheelDataList.Add((manufacturerName, currentAirPressure));
                    }

                    return wheelDataList;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}. Please try again.");
                }
            }
        }

        public static T ParseEnum<T>(string i_Value)
        {
            return (T)Enum.Parse(typeof(T), i_Value, true);
        }

        public static void BlankSpace()
        {
            Console.WriteLine();
        }
    }
}
