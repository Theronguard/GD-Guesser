using Godot;
using Quiztomize.Scripts.Data_Access;
using Quiztomize.Scripts.Models;
using Quiztomize.Scripts.View_Models;
using System;

public partial class StartView : View
{
    [ExportGroup("Pack Button")]
	[Export] PackedScene PackButton;
    [Export] Node PackButtonSpawnParent;

    [ExportGroup("UI References")]
    [Export] LineEdit AmountOfPrompts;
    [Export] LineEdit TimeToAnswer;

    private Button SelectedButton = null;

    private StartViewModel ViewModel = null;
    
    public override void _Ready()
	{
		if(ViewModel == null)
		{
			ViewModel = new StartViewModel();
			ViewModel.Name = "Start ViewModel";
			ViewModel.OnRedraw += OnViewmodelRedraw;
            //GetTree().CurrentScene.CallDeferred(MethodName.AddChild, ViewModel);
            CallDeferred(MethodName.AddChild, ViewModel);
        }
    }

    public override void _ExitTree()
    {
		ViewModel.OnRedraw -= OnViewmodelRedraw;
        base._ExitTree();
    }

    #region Signals

    void OnPackButtonPressed(Button pressedButton)
    {
        SelectedButton = pressedButton;
        foreach(Node node in PackButtonSpawnParent.GetChildren())
        {
            Button button = node as Button;
            button?.RemoveThemeStyleboxOverride("normal");
        }
        StyleBoxFlat normal = SelectedButton.GetThemeStylebox("normal") as StyleBoxFlat;
        StyleBoxFlat newStyleBox = normal.Duplicate(true) as StyleBoxFlat;
        newStyleBox.BgColor = SelectedButton.GetThemeColor("SelectedColor");
        newStyleBox.BorderColor = SelectedButton.GetThemeColor("SelectedColor") * new Color(0.5f, 0.5f, 0.5f, 1f);
        SelectedButton.AddThemeStyleboxOverride("normal", newStyleBox);
    }

    void OnBackPressed()
    {
        Back();
    }

    void OnViewmodelRedraw()
    {
        Redraw();
    }

    void OnStartGamePressed()
    {
        if (SelectedButton == null) return;

        if (!int.TryParse(AmountOfPrompts.Text, out int promptsNum)) return;
        if (!int.TryParse(TimeToAnswer.Text, out int timeToAnswer)) return;

        if (timeToAnswer <= 0 || promptsNum <= 0) return;


        GD.Print(promptsNum, timeToAnswer);

        Game.GameData startGameData = new(SelectedButton.Text, timeToAnswer, promptsNum);
        ViewModel.StartGame(startGameData);
    }

    public override void OnViewLoaded()
    {
        ViewModel.UpdateUI();
    }

    #endregion

    public void Redraw()
    {
        ClearButtons();
        CreateButtons();
    }

    public void CreateButtons()
    {   
        foreach (Pack pack in ViewModel.AllPacks)
        {
            Button button = PackButton.Instantiate() as Button;
            button.Text = pack.Name;
            button.Pressed += () => { OnPackButtonPressed(button);  };
            PackButtonSpawnParent.AddChild(button);
        }
    }
    public void ClearButtons()
    {
        foreach (Node button in PackButtonSpawnParent.GetChildren())
        {
           button.QueueFree();
        }
    }
}
