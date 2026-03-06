using Act7Obj.Controller;
using Act7Obj.Model;
using Slay_The_Prof.Service;
using Slay_The_Prof.View;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Act7Obj.View
{
    public class ConsoleInterface
    {

        public static void DisplayWelcomeMessage()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;

            TextMoveInUIController.CenterText("╔══════════════════════════════════════════╗");
            TextMoveInUIController.CenterText("║                                          ║");
            TextMoveInUIController.CenterText("║              SLAY THE PROF               ║");
            TextMoveInUIController.CenterText("║                                          ║");
            TextMoveInUIController.CenterText("╚══════════════════════════════════════════╝");

            Console.ForegroundColor = ConsoleColor.White;
            TextMoveInUIController.CenterText("Welcome to the adventure, Student!");
            TextMoveInUIController.CenterText("Inspired by Slay the Spire");
            Console.ResetColor();

            TextMoveInUIController.BottomRightPromptContinue();
        }
        public static void DisplayGameDescription()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n\n\n\n\n\n\n"); // Add some top margin

            TextMoveInUIController.CenterText("--- THE MISSION ---");
            Console.ResetColor();
            Console.WriteLine();

            TextMoveInUIController.CenterText("The story is to embark on the challenges you face in class for BSIS-3B.");
            TextMoveInUIController.CenterText("In this game, you will fight against enemies (Our Profs) to become stronger.");
            TextMoveInUIController.CenterText("There are 8 Stages representing the 8 Semesters required to graduate.");
            TextMoveInUIController.CenterText("Each Semester has 4 Classrooms, meaning 4 different Profs to defeat.");

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            TextMoveInUIController.CenterText("Each Prof has unique attack patterns and difficulty.");
            Console.ResetColor();

            TextMoveInUIController.BottomRightPromptContinue();
        }
        public static string  DisplayGameMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            TextMoveInUIController.CenterText("╔══════════════════════════════════════════╗");
            TextMoveInUIController.CenterText("║                                          ║");
            TextMoveInUIController.CenterText("║                MAIN MENU                 ║");
            TextMoveInUIController.CenterText("║                                          ║");
            TextMoveInUIController.CenterText("╚══════════════════════════════════════════╝");
            Console.WriteLine();
            Console.ResetColor();
            TextMoveInUIController.CenterText("  [1]  Start");
            TextMoveInUIController.CenterText("    [2]  Continue");
            TextMoveInUIController.CenterText("[3]  Exit");
            Console.WriteLine("\n" + new string('─', Console.WindowWidth));

            Console.Write("    Select your game: ");
            return Console.ReadLine()!;

        }
        public static void DisplayPlayerStats(PlayerCharacterModel character, Player player)
        {
           

            // Header - Player Identity
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine($"║  PLAYER: {player.PlayerName,-38}║"); 
            Console.WriteLine("╠════════════════════════════════════════════════╣");
            Console.ResetColor();

            // Character Identity
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"   HERO: {character.CharacterName.ToUpper()} ({character.CharacterType})");
            Console.WriteLine($"   GOLD: {character.PlayerGold}");
            Console.WriteLine($"   LEVEL: {character.PlayerLevel}");
            Console.ResetColor();
            Console.WriteLine("╚════════════════════════════════════════════════╝");

            // Health Bar Visualization
            Console.Write(" HP:  ");

            // Draw health bar blocks based on Health percentage
            StagesInterfaceView.DrawHealthBar(character.Health, character.MaxHealth, ConsoleColor.Green);

            Console.WriteLine("──────────────────────────────────────────────────");

            // Attribute Grid
            Console.WriteLine($" {"ATTACK:",-15} {character.AttackDamage,-10} {"SPEED:",-10} {character.Speed}");
            Console.WriteLine($" {"INTELECT:",-15} {character.Intelect,-10}");

            Console.WriteLine("──────────────────────────────────────────────────");

            // Combat Ratios
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($" {"CRIT CHANCE:",-15} {character.CritChance}%");
            Console.WriteLine($" {"CRIT DAMAGE:",-15} {character.CritDamage}%");
            Console.ResetColor();

        }

        public static void DisplayCreatedMessage()
        {
            Console.WriteLine("==================================================");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("          PLAYER CHARACTER SUCCESSFULLY CREATED! ");
            Console.ResetColor();
            Console.WriteLine("==================================================");
        }
        public static Player DisplaySetPlayerName()
        {
            while (true) // Use a loop instead of recursion to prevent stack overflow
            {
                Console.Clear();
                Console.Write("Enter your Player Name: ");
                string playerName = Console.ReadLine()?.Trim() ?? "";

                // 1. Check Length
                if (playerName.Length < 2)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Player name must be at least 2 characters long. Press any key to try again...");
                    Console.ResetColor();
                    Console.ReadKey();
                    continue;
                }

                // 2. Check Database for Existing Name
                if (DatabaseService.CheckIfPlayerExists(playerName))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"The name '{playerName}' is already taken! Please choose another.");
                    Console.ResetColor();
                    Console.WriteLine("Press any key to try again...");
                    Console.ReadKey();
                    continue;
                }

                // 3. If it passes all checks, return the player
                return new Player { PlayerName = playerName };
            }
        }

        public static PlayerCharacterModel? DisplayCharacterSelection()
        {
            while (true) 
            {
                Console.Clear();
                Console.WriteLine("=== HERO SELECTION ===");
                Console.WriteLine("[1] Aristain (Archer)");
                Console.WriteLine("[2] Paul (Mage)");
                Console.WriteLine("[3] Sumayang (Tank)");
                Console.WriteLine("[4] Pascual (Fighter)");
                Console.WriteLine("-----------------------");
                Console.WriteLine("[5] View All Hero Stats");
                Console.WriteLine("[6] Back to Main Menu");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine()!; 

                switch (choice)
                {
                    case "1": return new Aristain(); 
                    case "2": return new Paul();
                    case "3": return new Sumayang();
                    case "4": return new Pascual();

                    case "5":
                        ShowPreviewStats();
                        break;

                    case "6":
                        return null; 

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid choice. Press any key...");
                        Console.ResetColor();
                        Console.ReadKey();
                        break;
                }
            }
        }
        public static void ShowPreviewStats()
        {
            // We create temporary instances just to show the data in the Model
            DisplayHeroStats(new Aristain());
            DisplayHeroStats(new Paul());
            DisplayHeroStats(new Sumayang());
            DisplayHeroStats(new Pascual());
        }
        public static void DisplayHeroStats(PlayerCharacterModel hero)
        {
            Console.Clear();

            // Header with dynamic Character Type color
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("==================================================");
            Console.WriteLine($"           {hero.CharacterName.ToUpper()} - THE {hero.CharacterType.ToUpper()}           ");
            Console.WriteLine("==================================================");
            Console.ResetColor();

            // Health Bar logic (Visual representation)
            Console.Write(" HP:  ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"[{hero.Health}/{hero.MaxHealth}] ".PadRight(15));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(new string('█', hero.Health / 10)); 
            Console.ResetColor();

            Console.WriteLine("--------------------------------------------------");

            // Primary Stats
            Console.WriteLine($" {"ATTACK:",-15} {hero.AttackDamage}");
            Console.WriteLine($" {"SPEED:",-15} {hero.Speed}");
            Console.WriteLine($" {"INTELECT:",-15} {hero.Intelect}");

            Console.WriteLine("--------------------------------------------------");

            // Combat Stats
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($" {"CRIT CHANCE:",-15} {hero.CritChance}%");
            Console.WriteLine($" {"CRIT DAMAGE:",-15} {hero.CritDamage}%");
            Console.ResetColor();

            Console.WriteLine("==================================================");
            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey();
        }
    }
}

