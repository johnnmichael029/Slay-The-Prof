using Act7Obj.Model;
using Slay_The_Prof.Model;
using Slay_The_Prof.Model.EnemyModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Slay_The_Prof.Controller
{
    public class InitializeEnemyBeforeBattleAndCard
    {
        public static void InitializeCatindogsAndCards(Player currentPlayer)
        {
            Console.Clear();
            Enemy boss = new CantindogsCharacterModel();

            if (currentPlayer.SelectedHero != null)
            {
                currentPlayer.StartingDeck = currentPlayer.SelectedHero.StartingDeck;
            }

            // Now the deck won't be empty!
            CardManagerController deck = new(currentPlayer.StartingDeck);
            // Note: BattleLoop calls DrawCards(4) inside it, so you don't need to call it here.
            StagesControlling.FirstStagesBattle(currentPlayer, boss, deck);
        }
        public static void InitializeStrangeManAndCards(Player currentPlayer)
        {
            Console.Clear();
            Enemy stranger = new StrangerCharacterModel();

            if (currentPlayer.SelectedHero != null)
            {
                // This is to ensure that the player has a hero selected before starting the battle. It assigns the hero's starting deck to the player's current deck.
                currentPlayer.StartingDeck = currentPlayer.SelectedHero.StartingDeck;

                // Remove the buff and debuff that have in the first battle and continue with the next battle. This is to ensure that the player starts the next battle with a clean slate,
                // without any lingering effects from the previous battle.


            }
            // Now the deck won't be empty!
            CardManagerController deck = new(currentPlayer.StartingDeck);
            StagesControlling.StrangerEncounterBattle(currentPlayer, stranger, deck); ;
        }

        public static void InitializeTrinityAndCards(Player currentPlayer)
        {
            Console.Clear();
            Enemy trinity = new TrinityCharacterModel();
            if (currentPlayer.SelectedHero != null)
            {
                // This is to ensure that the player has a hero selected before starting the battle. It assigns the hero's starting deck to the player's current deck.
                currentPlayer.StartingDeck = currentPlayer.SelectedHero.StartingDeck;
                // Remove the buff and debuff that have in the first battle and continue with the next battle. This is to ensure that the player starts the next battle with a clean slate,
                // without any lingering effects from the previous battle.

            }
            // Now the deck won't be empty!
            CardManagerController deck = new(currentPlayer.StartingDeck);
            StagesControlling.TrinityBattle(currentPlayer, trinity, deck); ;
        }
    }
}
