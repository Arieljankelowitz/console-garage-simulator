﻿using Ex03.GarageLogic;
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

       internal static void NewElectric()
        {

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
