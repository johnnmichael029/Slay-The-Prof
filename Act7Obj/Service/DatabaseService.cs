using Act7Obj.Model;
using Microsoft.Data.Sqlite;
using Slay_The_Prof.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Slay_The_Prof.Service
{
    public class DatabaseService
    {
        public static void InitializePlayerDataTable()
        {
            using var connection = new SqliteConnection("Data Source=SlayTheProf.db");
            connection.Open();
            var command = connection.CreateCommand();

            // Combined table for Player info and Character stats
            command.CommandText = @"
            CREATE TABLE IF NOT EXISTS PlayerData (
                PlayerID INTEGER PRIMARY KEY AUTOINCREMENT,
                PlayerName TEXT NOT NULL UNIQUE,
                PlayerLevel INTEGER,
                PlayerGold INTEGER,
                CharacterName TEXT,
                CharacterDescription TEXT,
                CharacterType TEXT,
                CurrentHealth INTEGER,
                MaxHealth INTEGER,
                AttackDamage INTEGER,
                CritChance INTEGER,
                CritDamage INTEGER,
                Intelect INTEGER,
                Speed INTEGER
            );";
            command.ExecuteNonQuery();

            var cmd2 = connection.CreateCommand();
            cmd2.CommandText = @"
            CREATE TABLE IF NOT EXISTS PlayerCharacterSkills (
                SkillID INTEGER PRIMARY KEY AUTOINCREMENT,
                OwnerID INTEGER,
                SkillName TEXT,
                SkillDescription TEXT,
                FOREIGN KEY(OwnerID) REFERENCES PlayerData(PlayerID) ON DELETE CASCADE
            );";
            cmd2.ExecuteNonQuery();

            var cmd3 = connection.CreateCommand();
            cmd3.CommandText = @"
            CREATE TABLE IF NOT EXISTS PlayerCharacterPassives (
                PassiveID INTEGER PRIMARY KEY AUTOINCREMENT,
                OwnerID INTEGER, 
                PassiveName TEXT,
                PassiveDescription TEXT,
                FOREIGN KEY(OwnerID) REFERENCES PlayerData(PlayerID) ON DELETE CASCADE
            );";
            cmd3.ExecuteNonQuery();
        }
        public static void SavePlayerData(Player player)
        {
            using var connection = new SqliteConnection("Data Source=SlayTheProf.db");
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = @"
            INSERT OR REPLACE INTO PlayerData (PlayerName, PlayerLevel, PlayerGold, CharacterName, CharacterDescription, CharacterType, CurrentHealth, MaxHealth, AttackDamage, CritChance, CritDamage, Intelect, Speed)
            VALUES ($playerName, $playerLevel, $playerGold, $characterName, $characterDescription, $characterType, $currentHealth, $maxHealth, $attackDamage, $critChance, $critDamage, $intelect, $speed);";
    
            command.Parameters.AddWithValue("$playerName", player.PlayerName);
            command.Parameters.AddWithValue("$playerLevel", player.PlayerLevel);
            command.Parameters.AddWithValue("$playerGold", player.PlayerGold);
            command.Parameters.AddWithValue("$characterName", player.CharacterName ?? "");
            command.Parameters.AddWithValue("$characterDescription", player.CharacterDescription ?? "");
            command.Parameters.AddWithValue("$characterType", player.CharacterType ?? "");
            command.Parameters.AddWithValue("$currentHealth", player.Health);
            command.Parameters.AddWithValue("$maxHealth", player.MaxHealth);
            command.Parameters.AddWithValue("$attackDamage", player.AttackDamage);
            command.Parameters.AddWithValue("$critChance", player.CritChance);
            command.Parameters.AddWithValue("$critDamage", player.CritDamage);
            command.Parameters.AddWithValue("$intelect", player.Intelect);
            command.Parameters.AddWithValue("$speed", player.Speed);

            // FIX: Execute only ONCE to prevent the crash in image_e43a83
            command.ExecuteNonQuery();

            // FIX: Get the ID using a separate SELECT command
            using var idCmd = connection.CreateCommand();
            idCmd.CommandText = "SELECT last_insert_rowid();";
            long newPlayerId = (long)idCmd.ExecuteScalar();

            SaveSkills(newPlayerId, player, connection);
            SavePassives(newPlayerId, player, connection);
        }
        private static void SaveSkills(long ownerId, Player player, SqliteConnection connection)
        {
            // Clear only the skills belonging to THIS specific ID
            var deleteCmd = connection.CreateCommand();
            deleteCmd.CommandText = "DELETE FROM PlayerCharacterSkills WHERE OwnerID = $ownerID";
            deleteCmd.Parameters.AddWithValue("$ownerID", ownerId);
            deleteCmd.ExecuteNonQuery();

            // Loop through the 10+ skills in your Model
            for (int i = 0; i < player.SkillNames.Count; i++)
            {
                var skillCmd = connection.CreateCommand();
                skillCmd.CommandText = "INSERT INTO PlayerCharacterSkills (OwnerID, SkillName, SkillDescription) VALUES ($ownerID, $sName, $sDesc)";
                skillCmd.Parameters.AddWithValue("$ownerID", ownerId);
                skillCmd.Parameters.AddWithValue("$sName", player.SkillNames[i]);
                skillCmd.Parameters.AddWithValue("$sDesc", player.SkillDescriptions[i]);
                skillCmd.ExecuteNonQuery();
            }
        }
        private static void SavePassives(long ownerId, Player player, SqliteConnection connection)
        {
            // 1. Clear existing buffs for this specific ID
            var deleteCmd = connection.CreateCommand();
            deleteCmd.CommandText = "DELETE FROM PlayerCharacterPassives WHERE OwnerID = $ownerID";
            deleteCmd.Parameters.AddWithValue("$ownerID", ownerId);
            deleteCmd.ExecuteNonQuery();

            // 2. Loop through all currently active buffs
            for (int i = 0; i < player.PassiveSkills.Count; i++)
            {
                var passiveCmd = connection.CreateCommand();
                passiveCmd.CommandText = @"
            INSERT INTO PlayerCharacterPassives (OwnerID, PassiveName, PassiveDescription) 
            VALUES ($ownerID, $pName, $pDesc)";

                passiveCmd.Parameters.AddWithValue("$ownerID", ownerId);
                passiveCmd.Parameters.AddWithValue("$pName", player.PassiveSkills[i]);
                passiveCmd.Parameters.AddWithValue("$pDesc", player.PassiveDescriptions[i] ?? "");

                passiveCmd.ExecuteNonQuery();
            }
        }
        public static void InitializeEnemyDataTable()
        {
            using var connection = new SqliteConnection("Data Source=SlayTheProf.db");
            connection.Open();
            var command = connection.CreateCommand();

            // Combined table for Player info and Character stats
            command.CommandText = @"
            CREATE TABLE IF NOT EXISTS EnemyData (
                EnemyID INTEGER PRIMARY KEY AUTOINCREMENT,
                EnemyName TEXT NOT NULL UNIQUE,
                EnemyLevel INTEGER,
                EnemyDescription TEXT,
                EnemyType TEXT,
                CurrentHealth INTEGER,
                MaxHealth INTEGER,
                AttackDamage INTEGER,
                CritChance INTEGER,
                CritDamage INTEGER,
                Intelect INTEGER,
                Speed INTEGER
            );";
            command.ExecuteNonQuery();

            var cmd2 = connection.CreateCommand();
            cmd2.CommandText = @"
            CREATE TABLE IF NOT EXISTS EnemyCharacterSkills (
                SkillID INTEGER PRIMARY KEY AUTOINCREMENT,
                OwnerID INTEGER,
                SkillName TEXT,
                SkillDescription TEXT,
                FOREIGN KEY(OwnerID) REFERENCES EnemyData(EnemyID) ON DELETE CASCADE
            );";
            cmd2.ExecuteNonQuery();

            var cmd3 = connection.CreateCommand();
            cmd3.CommandText = @"
            CREATE TABLE IF NOT EXISTS EnemyCharacterPassives (
                PassiveID INTEGER PRIMARY KEY AUTOINCREMENT,
                OwnerID INTEGER, 
                PassiveName TEXT,
                PassiveDescription TEXT,
                FOREIGN KEY(OwnerID) REFERENCES EnemyData(EnemyID) ON DELETE CASCADE
            );";
            cmd3.ExecuteNonQuery();
        }
        public static void SaveEnemyData(Enemy enemy)
        {
            using var connection = new SqliteConnection("Data Source=SlayTheProf.db");
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = @"
            INSERT OR REPLACE INTO EnemyData (EnemyName, EnemyLevel, EnemyDescription, EnemyType, CurrentHealth, MaxHealth, AttackDamage, CritChance, CritDamage, Intelect, Speed)
            VALUES ($enemyName, $enemyLevel, $enemyDescription, $enemyType, $currentHealth, $maxHealth, $attackDamage, $critChance, $critDamage, $intelect, $speed);";

            command.Parameters.AddWithValue("$enemyName", enemy.EnemyName);
            command.Parameters.AddWithValue("$enemyLevel", enemy.EnemyLevel);
            command.Parameters.AddWithValue("$enemyDescription", enemy.EnemyDescription ?? "");
            command.Parameters.AddWithValue("$enemyType", enemy.EnemyType ?? "");
            command.Parameters.AddWithValue("$currentHealth", enemy.Health);
            command.Parameters.AddWithValue("$maxHealth", enemy.MaxHealth);
            command.Parameters.AddWithValue("$attackDamage", enemy.AttackDamage);
            command.Parameters.AddWithValue("$critChance", enemy.CritChance);
            command.Parameters.AddWithValue("$critDamage", enemy.CritDamage);
            command.Parameters.AddWithValue("$intelect", enemy.Intelect);
            command.Parameters.AddWithValue("$speed", enemy.Speed);

            // FIX: Execute only ONCE to prevent the crash in image_e43a83
            command.ExecuteNonQuery();

            // FIX: Get the ID using a separate SELECT command
            using var idCmd = connection.CreateCommand();
            idCmd.CommandText = "SELECT last_insert_rowid();";
            long newEnemyId = (long)idCmd.ExecuteScalar();

            SaveEnemySkills(newEnemyId, enemy, connection);
            SaveEnemyPassives(newEnemyId, enemy, connection);
        }
        private static void SaveEnemySkills(long ownerId, Enemy enemy, SqliteConnection connection)
        {
            // Clear old skills for this owner
            var deleteCmd = connection.CreateCommand();
            deleteCmd.CommandText = "DELETE FROM EnemyCharacterSkills WHERE OwnerID = $ownerID";
            deleteCmd.Parameters.AddWithValue("$ownerID", ownerId);
            deleteCmd.ExecuteNonQuery();

            // Loop through the StartingDeck because that is where your cards are added
            for (int i = 0; i < enemy.StartingDeck.Count; i++)
            {
                var skillCmd = connection.CreateCommand();
                skillCmd.CommandText = @"INSERT INTO EnemyCharacterSkills (OwnerID, SkillName, SkillDescription) 
                                 VALUES ($ownerID, $sName, $sDesc)";

                skillCmd.Parameters.AddWithValue("$ownerID", ownerId);

                // Grab the Name from the CardModel in the deck
                skillCmd.Parameters.AddWithValue("$sName", enemy.StartingDeck[i].Name);

                // Match it with the description at the same index
                // Adding a safety check in case descriptions are shorter than the deck
                string description = (i < enemy.SkillDescriptions.Count)
                                     ? enemy.SkillDescriptions[i]
                                     : "No description available.";

                skillCmd.Parameters.AddWithValue("$sDesc", description);

                skillCmd.ExecuteNonQuery();
            }
        }
        private static void SaveEnemyPassives(long ownerId, Enemy enemy, SqliteConnection connection)
        {
            // 1. Clear existing buffs for this specific ID
            var deleteCmd = connection.CreateCommand();
            deleteCmd.CommandText = "DELETE FROM EnemyCharacterPassives WHERE OwnerID = $ownerID";
            deleteCmd.Parameters.AddWithValue("$ownerID", ownerId);
            deleteCmd.ExecuteNonQuery();

            // 2. Loop through all currently active buffs
            for (int i = 0; i < enemy.PassiveSkills.Count; i++)
            {
                var passiveCmd = connection.CreateCommand();
                passiveCmd.CommandText = @"
                INSERT INTO EnemyCharacterPassives (OwnerID, PassiveName, PassiveDescription) 
                VALUES ($ownerID, $pName, $pDesc)";

                passiveCmd.Parameters.AddWithValue("$ownerID", ownerId);
                passiveCmd.Parameters.AddWithValue("$pName", enemy.PassiveSkills[i]);
                passiveCmd.Parameters.AddWithValue("$pDesc", enemy.PassiveDescriptions[i] ?? "");

                passiveCmd.ExecuteNonQuery();
            }
        }
        public static bool CheckIfPlayerExists(string playerName)
        {
            using var connection = new SqliteConnection("Data Source=SlayTheProf.db");
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT COUNT(1) FROM PlayerData WHERE PlayerName = $playerName";
            command.Parameters.AddWithValue("$playerName", playerName);

            long count = (long)command.ExecuteScalar();
            return count > 0;

        }
        public static List<string> GetAllSavedPlayerNames()
        {
            List<string> names = new List<string>();
            using var connection = new SqliteConnection("Data Source=SlayTheProf.db");
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT PlayerName FROM PlayerData";

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                names.Add(reader.GetString(0));
            }
            return names;
        }
        public static Player? LoadPlayerData(string name)
        {
            using var connection = new SqliteConnection("Data Source=SlayTheProf.db");
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM PlayerData WHERE PlayerName = $name";
            command.Parameters.AddWithValue("$name", name);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                var player = new Player
                {
                    PlayerName = reader.GetString(reader.GetOrdinal("PlayerName")),
                    PlayerLevel = reader.GetInt32(reader.GetOrdinal("PlayerLevel")),
                    PlayerGold = reader.GetInt32(reader.GetOrdinal("PlayerGold")),
                    CharacterName = reader.GetString(reader.GetOrdinal("CharacterName")),
                    CharacterDescription = reader.GetString(reader.GetOrdinal("CharacterDescription")),
                    CharacterType = reader.GetString(reader.GetOrdinal("CharacterType")),
                    Health = reader.GetInt32(reader.GetOrdinal("CurrentHealth")),
                    MaxHealth = reader.GetInt32(reader.GetOrdinal("MaxHealth")),
                    AttackDamage = reader.GetInt32(reader.GetOrdinal("AttackDamage")),
                    CritChance = reader.GetInt32(reader.GetOrdinal("CritChance")),
                    CritDamage = reader.GetInt32(reader.GetOrdinal("CritDamage")),
                    Intelect = reader.GetInt32(reader.GetOrdinal("Intelect")),
                    Speed = reader.GetInt32(reader.GetOrdinal("Speed"))
                };

                long playerId = reader.GetInt64(reader.GetOrdinal("PlayerID"));
                LoadSkillsAndPassives(playerId, player, connection);
                return player;
            }
            return null;
        }
        private static void LoadSkillsAndPassives(long playerId, Player player, SqliteConnection connection)
        {
            // Load Skills
            using var skillCmd = connection.CreateCommand();
            skillCmd.CommandText = "SELECT SkillName, SkillDescription FROM PlayerCharacterSkills WHERE OwnerID = $id";
            skillCmd.Parameters.AddWithValue("$id", playerId);
            using var sReader = skillCmd.ExecuteReader();
            while (sReader.Read())
            {
                player.SkillNames.Add(sReader.GetString(0));
                player.SkillDescriptions.Add(sReader.GetString(1));
            }

            // Load Passives
            using var passiveCmd = connection.CreateCommand();
            passiveCmd.CommandText = "SELECT PassiveName, PassiveDescription FROM PlayerCharacterPassives WHERE OwnerID = $id";
            passiveCmd.Parameters.AddWithValue("$id", playerId);
            using var pReader = passiveCmd.ExecuteReader();
            while (pReader.Read())
            {
                player.PassiveSkills.Add(pReader.GetString(0));
                player.PassiveDescriptions.Add(pReader.GetString(1));
            }
        }
        public static void DeletePlayerData(String playerName)
        {
            using var connection = new SqliteConnection("Data Source=SlayTheProf.db");
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "DELETE FROM PlayerDAta WHERE PlayerName = @name";
                command.Parameters.AddWithValue("@name", playerName);
                command.ExecuteNonQuery();
            }
        }
    }
}
