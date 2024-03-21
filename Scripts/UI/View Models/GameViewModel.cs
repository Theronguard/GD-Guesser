using Quiztomize.Scripts.Data_Access;
using Quiztomize.Scripts.Models;
using System.Collections.Generic;
using System.Linq;
using Godot;
using System;

namespace Quiztomize.Scripts.View_Models
{
    partial class GameViewModel : ViewModel
    {
        public delegate void OnNextCardHandler(bool correct);
        public OnNextCardHandler OnNextCard;

        public Action OnFinish;

        public Game.GameData StartGameData;

        public List<string> Prompts = new List<string>();
        public List<string> GuessedPrompts = new List<string>();
        public List<string> FailedPrompts = new List<string>();

        private IEnumerator<string> PromptEnumerator;

        [Export] public Timer CardTimer;
        [Export] public FinishViewModel FinishViewModel;
        public override void _Ready()
        {
            Initialize();
        }

        public void Initialize()
        {
            Pack pack = PackDA.Get(StartGameData.PackName);

            Prompts.Clear();
            Prompts = Misc.GetRandomElements(pack.Prompts.ToList(), StartGameData.AmountOfPrompts);
            PromptEnumerator = Prompts.GetEnumerator();

            CardTimer.WaitTime = StartGameData.TimeToAnswer;
            CardTimer.Timeout += () => { NextCard(false); };
            CardTimer.Start();

            NextCard(false);
        }

        public void NextCard(bool correct)
        {
            if(PromptEnumerator.Current != null)
            {
                if (correct)
                    GuessedPrompts.Add(GetCurrentCard());
                else
                    FailedPrompts.Add(GetCurrentCard());
            }
            
            if (!PromptEnumerator.MoveNext())
            {
                Finish();
                return;
            }

            CardTimer.Start();
            OnNextCard?.Invoke(correct);
        }

        public string GetCurrentCard()
        {
            return PromptEnumerator.Current;
        }

        public void Finish()
        {
            FinishViewModel.GuessedPrompts = new List<string>(GuessedPrompts);
            FinishViewModel.FailedPrompts = new List<string>(FailedPrompts);
            FinishViewModel.UpdateUI();
            OnFinish?.Invoke();
            CardTimer.Stop();
            SetProcess(false);
        }
    }
}
