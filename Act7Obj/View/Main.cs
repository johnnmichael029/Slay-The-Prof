    using Act7Obj.Controller;
    using Act7Obj.Model;
    using Microsoft.Data.Sqlite;
    using Slay_The_Prof.Controller;
    using Slay_The_Prof.Service;
    using Slay_The_Prof.View;

    namespace Act7Obj.View
    {
        public class SlayTheProf
        {

            public static void Main(string[] args)
            {
                DatabaseService.InitializePlayerDataTable();
                DatabaseService.InitializeEnemyDataTable();
                AddEnemyController.SeedEnemies();

                ConsoleInterface.DisplayWelcomeMessage();
                ConsoleInterface.DisplayGameDescription();
                bool programRunning = true;
                while (programRunning)
                {

                    Player? currentPlayer = UserInputController.UserInputFunction();
                    if (currentPlayer != null)
                    {
                        StagesInterfaceView.ShowFirsttagesInterfaces(currentPlayer);
                    }
                    else
                    {
                        programRunning = false;
                    }
                }
            }

        }


        public class Item
        {
            public required string ItemName { get; set; }
            public required string ItemType { get; set; }
        }

    }
