using Act7Obj.Controller;
using System;
using System.Collections.Generic;
using System.Text;

namespace Slay_The_Prof.View
{
    public class PlayerGameStoryView
    {
        public static string CantindogsStory()
        {
            Console.Clear();
            // Header for Stage 1
            Console.ForegroundColor = ConsoleColor.Yellow;
            TextMoveInUIController.CenterText("╔══════════════════════════════════════════╗");
            TextMoveInUIController.CenterText("║          STAGE 1: FIRST SEMESTER         ║");
            TextMoveInUIController.CenterText("╚══════════════════════════════════════════╝");
            Console.ResetColor();
            TextMoveInUIController.CenterText("Class Battle 1");
            Console.WriteLine("\n");

            // Story Text
            TextMoveInUIController.CenterText("It is your very first day of class for BSIS.");
            TextMoveInUIController.CenterText("The hallway is quiet as you enter the classroom...");
            TextMoveInUIController.CenterText("Suddenly, a wild PROF. CANTINDOGS appears!");
            Console.WriteLine("\n");

            Console.ForegroundColor = ConsoleColor.Red;
            TextMoveInUIController.CenterText("\"CANTINDOGS: Prepare for your Program in Notepad!\"");
            Console.ResetColor();
            Console.WriteLine("\n");
            Console.WriteLine("\n" + new string('─', Console.WindowWidth));

            Console.WriteLine("What will you do?\n");
            Console.WriteLine("   [1] Fight (Do the Program)");
            Console.WriteLine("   [2] Escape (Skip Cantindogs Class)");
            Console.WriteLine("   [3] Return to Main Menu");
            Console.WriteLine();

            Console.Write("Your Action: ");
            string action = Console.ReadLine()!;

            return action;
        }

        public static string StrangeEncounter()
        {
            Console.Clear();
            // Header for Stage 1
            Console.ForegroundColor = ConsoleColor.Yellow;
            TextMoveInUIController.CenterText("╔══════════════════════════════════════════╗");
            TextMoveInUIController.CenterText("║             STRANGE ENCOUNTER            ║");
            TextMoveInUIController.CenterText("╚══════════════════════════════════════════╝");
            Console.ResetColor();

            Console.WriteLine("\n\n");
            TextMoveInUIController.CenterText("You are exhausted after the Cantindogs class. As you walk home,");
            TextMoveInUIController.CenterText("you suddenly encounter a strange man blocking your path.");

            Console.WriteLine("\n");
            TextMoveInUIController.CenterText("STRANGE MAN: Are you lost baby boy? I can make you an offer.");
            TextMoveInUIController.CenterText("I'll give you 150 [GOLD] if you can spare some of your time to me [lose 10 Health]");
            Console.WriteLine("\n" + new string('─', Console.WindowWidth));

            Console.WriteLine("What will you do?\n");
            Console.WriteLine("   [1] Accept Offer (+150 GOLD, -10 HEALTH)");
            Console.WriteLine("   [2] Escape (Chances are...?)");
            Console.WriteLine("   [3] Return to Main Menu");
            Console.WriteLine();

            Console.Write("Your Action: ");
            string action = Console.ReadLine()!;

            return action;
        }

        public static string StrangeEncounterMessage()
        {
            Console.Clear();
            // Header for Stage 1
            Console.ForegroundColor = ConsoleColor.Yellow;
            TextMoveInUIController.CenterText("╔══════════════════════════════════════════╗");
            TextMoveInUIController.CenterText("║             STRANGE ENCOUNTER            ║");
            TextMoveInUIController.CenterText("╚══════════════════════════════════════════╝");
            Console.ResetColor();


            Console.WriteLine("\n\n");
            TextMoveInUIController.CenterText("Got You!, You can't escape.");
            TextMoveInUIController.CenterText("Now, I will give you one last chance. Accept my offer or fight me.");
            Console.WriteLine("\n" + new string('─', Console.WindowWidth));

            Console.WriteLine("What will you do?\n");
            Console.WriteLine("   [1] Accept Offer (+150 GOLD, -10 HEALTH)");
            Console.WriteLine("   [2] Fight");
            Console.WriteLine();

            Console.Write("Your Action: ");
            string action = Console.ReadLine()!;

            return action;
        }

        public static void StrangeEncounterHappy()
        {
            Console.Clear();
            // Header for Stage 1
            Console.ForegroundColor = ConsoleColor.Yellow;
            TextMoveInUIController.CenterText("╔══════════════════════════════════════════╗");
            TextMoveInUIController.CenterText("║             STRANGE ENCOUNTER            ║");
            TextMoveInUIController.CenterText("╚══════════════════════════════════════════╝");
            Console.ResetColor();


            Console.WriteLine("\n\n");
            TextMoveInUIController.CenterText("STRANGER: See you again!");
            Console.WriteLine("\n" + new string('─', Console.WindowWidth));
        }

        public static string TrinityStory()
        {
            Console.Clear();
            // Header for Stage 1
            Console.ForegroundColor = ConsoleColor.Yellow;
            TextMoveInUIController.CenterText("╔══════════════════════════════════════════╗");
            TextMoveInUIController.CenterText("║          STAGE 1: FIRST SEMESTER         ║");
            TextMoveInUIController.CenterText("╚══════════════════════════════════════════╝");
            Console.ResetColor();
            TextMoveInUIController.CenterText("Class Battle 2");
            Console.WriteLine("\n");

            // Story Text
            TextMoveInUIController.CenterText("During class, Trinity said, 'WHY NO ONE IS MAKING INITIATE??' ");
            TextMoveInUIController.CenterText("Everyone is laughing because no one is making initiate.");
            Console.WriteLine("\n");

            Console.ForegroundColor = ConsoleColor.Red;
            TextMoveInUIController.CenterText("And she call your name and ask why are you laughing?");
            Console.ResetColor();
            Console.WriteLine("\n");
            Console.WriteLine("\n" + new string('─', Console.WindowWidth));

            Console.WriteLine("What will you do?\n");
            Console.WriteLine("   [1] Said you are not (Fight)");
            Console.WriteLine("   [2] Escape (You will miss a good fight if you do..)");
            Console.WriteLine("   [3] Return to Main Menu");
            Console.WriteLine();

            Console.Write("Your Action: ");
            string action = Console.ReadLine()!;

            return action;
        }

        public static void TrinityMessage()
        {
            Console.Clear();
            // Header for Stage 1 Battle 2
            Console.ForegroundColor = ConsoleColor.Yellow;
            TextMoveInUIController.CenterText("╔══════════════════════════════════════════╗");
            TextMoveInUIController.CenterText("║          STAGE 1: FIRST SEMESTER         ║");
            TextMoveInUIController.CenterText("╚══════════════════════════════════════════╝");
            Console.ResetColor();
            TextMoveInUIController.CenterText("Class Battle 2");
            Console.WriteLine("\n");

            // Story Text
            TextMoveInUIController.CenterText("You realy think you can't fight me?");
            TextMoveInUIController.BottomRightPromptContinue();

        }

        public static string ClassBreak1()
        {
            Console.Clear();
            // Header for Stage 1
            Console.ForegroundColor = ConsoleColor.Yellow;
            TextMoveInUIController.CenterText("╔══════════════════════════════════════════╗");
            TextMoveInUIController.CenterText("║               CLASS BREAK                ║");
            TextMoveInUIController.CenterText("╚══════════════════════════════════════════╝");
            Console.ResetColor();
            TextMoveInUIController.CenterText("Class Break 1");
            Console.WriteLine("\n");

            // Story Text
            TextMoveInUIController.CenterText("Player is hungry and wants something to eat.");
            TextMoveInUIController.CenterText("As he walk, Trader Yamashii sees him.");
            Console.WriteLine("\n");

            Console.ForegroundColor = ConsoleColor.Green;
            TextMoveInUIController.CenterText("YAMASHII: You look very exhuasted, want something to eat?");
            Console.ResetColor();
            Console.WriteLine("\n");
            Console.WriteLine("\n" + new string('─', Console.WindowWidth));

            Console.WriteLine("What will you do?\n");
            Console.WriteLine("   [1] Buy Items (Gusto ko pastil with Egg)");
            Console.WriteLine("   [2] Buy Cards ");
            Console.WriteLine("   [3] Skip (Wala me pera)");
            Console.WriteLine("   [4] Return to Main Menu");
            Console.WriteLine();

            Console.Write("Your Action: ");
            string action = Console.ReadLine()!;

            return action;
        }
    }
}
