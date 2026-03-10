    using Act7Obj.Controller;
    using Act7Obj.Model;
    using Microsoft.Data.Sqlite;
    using Slay_The_Prof.Controller;
    using Slay_The_Prof.Service;
    using Slay_The_Prof.View;
    using System.Runtime.InteropServices;

namespace Act7Obj.View
{
    public class SlayTheProf
    {

        public static void Main(string[] args)
        {
            DatabaseService.InitializePlayerDataTable();
            DatabaseService.InitializeEnemyDataTable();
            DatabaseService.InitializePlayerItemDataTable();
            AddEnemyController.SeedEnemies();

            ConsoleInterface.DisplayWelcomeMessage();
            ConsoleInterface.DisplayGameDescription();
            GameFlowController.GameFlow();
        }
    }
}
