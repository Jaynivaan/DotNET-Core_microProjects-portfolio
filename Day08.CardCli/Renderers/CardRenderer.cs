//gs
using Day08.CardCli.Models;
using System;

namespace Day08.CardCli.Renderers
{
    //s-srp
    //This class only renders terminal ui
    //
    //No data creation
    //No config management
    //No app start up logic

    public static class CardRenderer
    {
        public static void Render(DeveloperCard card)
        {
            //Terminal Title
            Console.Title = "Developer Card";

            //Border Color
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(
                "|========================================================================================|");
            Console.WriteLine(
                "|                                  DEVELOPER CARD                                        |");
            Console.WriteLine(
                "|========================================================================================|");
            Console.WriteLine();

            //Reset Default Color
            Console.ResetColor();

            PrintSection("NAME", card.Name, ConsoleColor.Cyan);
            PrintSection("ROLE", card.Role, ConsoleColor.Green);
            PrintSection("GITHUB", card.Github, ConsoleColor.Yellow);
            PrintSection("WEBSITE", card.Website, ConsoleColor.Blue);
            PrintSection("LEARNING", card.LearningDirection, ConsoleColor.DarkCyan);
            PrintSection("ARCHITECTURE", card.ArchitecturePhilosophy, ConsoleColor.DarkYellow);
            PrintSection("CURRENT QUEST", card.CurrentQuest, ConsoleColor.Magenta);
            PrintSection("ENERGY", card.FavouriteEnergy, ConsoleColor.Red);
            PrintSection("PRIME RULE", card.MyPrimePrinciple, ConsoleColor.White);

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.DarkGray;

            Console.WriteLine(
                "--------------------------------------------------------------------------------------------");
            Console.WriteLine(
                "Build With Clean Architecture Thinking using .NET");
            Console.ResetColor();

        }
        //s-small foccussed helper method
        //REsponsible only for rendering one section
        private static void PrintSection(
            string title, string value, ConsoleColor color)
        {
            Console.ForegroundColor = color;

            Console.Write($"{title,-18}");

            Console.ResetColor();

            Console.WriteLine($": {value}");

            Console.WriteLine();

        }
    }
}