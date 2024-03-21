using Godot;
using Quiztomize.Scripts;
using Quiztomize.Scripts.Data_Access;
using Quiztomize.Scripts.Models;
using Quiztomize.Scripts.View_Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public partial class Game : Node2D
{
    public struct GameData
    {
        public string PackName;
        public int TimeToAnswer;
        public int AmountOfPrompts;

        public GameData(string packName, int timeToAnswer, int amountOfPrompts)
        {
            PackName = packName;
            TimeToAnswer = timeToAnswer;
            AmountOfPrompts = amountOfPrompts;
        }
    }

    [Export] GameViewModel ViewModel;

    public void PassData(GameData data)
    {
        ViewModel.StartGameData = data;
    }

}
