using Godot;
using Quiztomize.Scripts.Data_Access;
using Quiztomize.Scripts.Models;
using Quiztomize.Scripts.View_Models;
using System;

public partial class GameView : View
{
    [ExportGroup("Cards")]
    [Export] private Node CardsParent = null;
    [Export] private PackedScene CardPrefab = null;
    [ExportGroup("View")]
    [Export] private GameViewModel ViewModel = null;
    [Export] private Label TimerLabel = null;
    [ExportGroup("View Navigation")]
    [Export] private FinishView FinishView = null;

    private Card CurrentCard = null;
    
    public override void _Ready()
	{
        ViewModel.OnRedraw += OnViewmodelRedraw;
        ViewModel.OnNextCard += OnNextCard;
        ViewModel.OnFinish += OnFinish;
    }

    public override void _ExitTree()
    {
        ViewModel.OnRedraw -= OnViewmodelRedraw;
        base._ExitTree();
    }
    public override void _Process(double delta)
    {
        TimerLabel.Text = Mathf.Ceil(ViewModel.CardTimer.TimeLeft).ToString();
    }

    #region Signals

    void OnViewmodelRedraw()
    {

    }

    void OnNextCard(bool correct)
    {
        
        if (CurrentCard is null)
            CreatePromptCard(ViewModel.GetCurrentCard());
        else
        {
            CurrentCard.Exit(correct);
            CurrentCard.OnDisappeared += () =>
            {
                CreatePromptCard(ViewModel.GetCurrentCard());
            };
        }
        
        
    }

    void OnFinish()
    {
        EmitSignal(SignalName.ViewSwitched, FinishView);
    }

    #endregion

    Card CreatePromptCard(string prompt)
    {
        Card card = CardPrefab.Instantiate<Card>();
        card.SetPrompt(prompt);
        card.OnCardPressAndSlide += ViewModel.NextCard;

        CardsParent.AddChild(card);
        CurrentCard = card;

        return card;
    }

    public override void Back()
    {
        ViewModel.Finish();
    }
}
