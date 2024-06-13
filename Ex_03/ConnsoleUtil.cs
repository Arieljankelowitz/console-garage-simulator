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
        public static string ChooseOption(string i_message, string[] i_Options, int i_OptionsPerLine = 4, int i_SpacingPerLine = 8, int i_StartX = 0)
        {
            
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

        internal (float maxBatteryLife, float currentBatteryLife) NewElectric()
        {
            (float maxBatteryLife, float currentBatteryLife) electricProperties = (0.0f, 0.0f);

            Console.WriteLine("Please enter the Max battery life in hours (Separated by a ':' Eg 2 hours and 24 min is 2:24): ");
            string maxTime = Console.ReadLine();

            try
            {
                electricProperties.maxBatteryLife = convertTimeToHours(maxTime);
                Console.WriteLine($"The maximum battery life in hours is: {electricProperties.maxBatteryLife:F2} hours.");

                Console.WriteLine("Please enter the current battery life in hours (Separated by a ':' Eg 1 hour and 30 min is 1:30): ");
                string currentTime = Console.ReadLine();

                electricProperties.currentBatteryLife = convertTimeToHours(currentTime);
                validateBatteryLife(electricProperties.currentBatteryLife, electricProperties.maxBatteryLife);

                Console.WriteLine($"The current battery life is: {electricProperties.currentBatteryLife:F2} hours, and it is within the max capacity of {electricProperties.maxBatteryLife:F2} hours.");
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return electricProperties;
        }
        private static void validateBatteryLife(float i_currentBatteryLife, float i_maxBatteryLife)
        {
            if (i_currentBatteryLife < 0)
            {
                throw new ArgumentException("Current battery life cannot be negative.");
            }

            if (i_currentBatteryLife > i_maxBatteryLife)
            {
                throw new ArgumentException("Current battery life cannot exceed max battery life.");
            }
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
            
        
        internal static void NewFuel() { }

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
    }
}
