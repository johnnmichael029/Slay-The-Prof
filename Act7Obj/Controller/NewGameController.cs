using Act7Obj.Controller;
using Act7Obj.Model;
using Act7Obj.View;
using Slay_The_Prof.Model;
using Slay_The_Prof.Service;
using Slay_The_Prof.View;
using System;
using System.Collections.Generic;
using System.Text;

namespace Slay_The_Prof.Controller
{
    public class NewGameController
    {
        public static void NewGameData()
        {
            Console.Clear();
            Player myPlayer = ConsoleInterface.DisplaySetPlayerName();
            PlayerCharacterModel? selectedHero = ConsoleInterface.DisplayCharacterSelection();

            if (selectedHero != null)
            {
                myPlayer.SelectedHero = selectedHero;
                Console.Clear();
                ConsoleInterface.DisplayPlayerStats(selectedHero, myPlayer);
                ConsoleInterface.DisplayCreatedMessage();
                SavePlayerData(myPlayer, selectedHero);
                TextMoveInUIController.BottomRightPromptContinue();
                StagesInterfaceView.ShowFirstStagesInterfaces(myPlayer);
            }
        }

        public static void SavePlayerData(Player myPlayer, PlayerCharacterModel selectedHero)
        {
            // SYNC: Keep your logic for copying hero stats to player object
            myPlayer.CharacterName = selectedHero.CharacterName;
            myPlayer.CharacterType = selectedHero.CharacterType;
            myPlayer.CharacterDescription = selectedHero.CharacterDescription;
            myPlayer.Health = selectedHero.Health;
            myPlayer.MaxHealth = selectedHero.MaxHealth;
            myPlayer.AttackDamage = selectedHero.AttackDamage;
            myPlayer.CritChance = selectedHero.CritChance;
            myPlayer.CritDamage = selectedHero.CritDamage;
            myPlayer.IntelLect = selectedHero.IntelLect;
            myPlayer.Speed = selectedHero.Speed;
            myPlayer.PlayerLevel = selectedHero.PlayerLevel;
            myPlayer.CurrentExp = selectedHero.CurrentExp;
            myPlayer.PlayerGold = selectedHero.PlayerGold;

            myPlayer.SkillNames = selectedHero.StartingDeck.ConvertAll(card => card.Name);
            myPlayer.SkillDescriptions = selectedHero.StartingDeck.ConvertAll(card => card.CardDescription);
            myPlayer.StartingDeck = new List<CardModel>(selectedHero.StartingDeck);
            myPlayer.PassiveSkills = selectedHero.PassiveSkills;
            myPlayer.PassiveDescriptions = selectedHero.PassiveDescriptions;

            // Database Call
            DatabaseService.SavePlayerData(myPlayer);
            Console.WriteLine("Saving player data...");
            Console.WriteLine("Successfully saved player data!");
        }
    }
}
