

using Ex_03;
using Ex03.GarageLogic;
using System.Runtime.InteropServices;


namespace GarageLogic

{
    internal class Program
    {
        static void Main()
        {
            string[] boardOptions = new string[] { "4x4", "4x5", "4x6", "5x4", "5x6", "6x4", "6x5", "6x6" };

            string[] nameOptions = new string[] { "Rephaela Jankelowitz", "Rafaela Janks", "Esta Janks ", "RJ","this is goitn to be so lon i just want to see how it is goin to work in the prgram" };
            string s =ConnsoleUtil.ChooseOption("Choose a baby Name", nameOptions, 1, 0, 1);
            ConnsoleUtil.ChooseOption("choose board", boardOptions);
            
            System.Console.WriteLine(s);
        }
    }
}
