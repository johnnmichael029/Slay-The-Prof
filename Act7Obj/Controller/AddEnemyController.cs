using Microsoft.Data.Sqlite;
using Slay_The_Prof.Model;
using Slay_The_Prof.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Slay_The_Prof.Controller
{
    public class AddEnemyController
    {
        public static void SaveEnemyData(Enemy enemy)
        {

            enemy.EnemyName = enemy.EnemyName;         
            enemy.SkillDescriptions = enemy.StartingDeck.ConvertAll(card => card.CardDescription);
            DatabaseService.SaveEnemyData(enemy);
            Console.WriteLine("Saving enemy data...");
            Console.WriteLine("Succesfully saved enemy data!");
        }
        public static void SeedEnemies()
        {
            List<Enemy> allEnemies = new List<Enemy>
        {
            new CantindogsCharacterModel()
            // Future enemies 
        };

            foreach (var enemy in allEnemies)
            {
                if (!DoesEnemyExist(enemy.EnemyName))
                {
                    SaveEnemyData(enemy);
                    Console.WriteLine($"[Database] Added new enemy: {enemy.EnemyName}");
                }
            }
        }

        private static bool DoesEnemyExist(string name)
        {
            using var connection = new SqliteConnection("Data Source=SlayTheProf.db");
            connection.Open();
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT COUNT(1) FROM EnemyData WHERE EnemyName = $name";
            cmd.Parameters.AddWithValue("$name", name);
            return (long)cmd.ExecuteScalar() > 0;
        }
    }
}
