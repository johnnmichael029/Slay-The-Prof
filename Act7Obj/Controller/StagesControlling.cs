using Act7Obj.Controller;
using Act7Obj.Model;
using Slay_The_Prof.Model;
using Slay_The_Prof.View;
using System;
using System.Collections.Generic;
using System.Text;

namespace Slay_The_Prof.Controller
{
    public class StagesControlling
    {
        public static void FirstStagesBattle(Player player, Enemy enemy, CardManagerController deck)
        {

            bool isPlayerWon = FirstSemController.CatindogsBattle1(player, enemy, deck);
            if (isPlayerWon)
            {   
                TextMoveInUIController.BottomRightPromptContinue();
                StagesInterfaceView.ShowStrangeEncounterInterface(player);
            }
        }

        public static void StrangerEncounterBattle(Player player, Enemy enemy, CardManagerController deck)
        {
            bool isPlayerWon = FirstSemController.StrangeManBattle(player, enemy, deck);
            if (isPlayerWon)
            {
                TextMoveInUIController.BottomRightPromptContinue();
                StagesInterfaceView.ShowStage1Battle2Interface(player);
            }
        }

        public static void TrinityBattle(Player player, Enemy enemy, CardManagerController deck)
        {
            bool isPlayerWon = FirstSemController.TrinityBattle2(player, enemy, deck);
            if (isPlayerWon)
            {
                TextMoveInUIController.BottomRightPromptContinue();
            }
        }
    }
}
