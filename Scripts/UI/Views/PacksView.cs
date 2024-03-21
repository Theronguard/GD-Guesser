using Godot;
using Quiztomize.Scripts.Data_Access;
using Quiztomize.Scripts.Models;
using System;

public partial class PacksView : View
{
	[Export] LineEdit NameInput;
	[Export] PackedScene PackButton;
    [Export] Node PackButtonSpawnParent;
    [Export] PromptsView PromptView;

    private Button SelectedButton = null;
    private PacksViewModel ViewModel = null;
    private MessageBox MessageBox = null;
    
    public override void _Ready()
	{
		if(ViewModel == null)
		{
			ViewModel = new PacksViewModel();
			ViewModel.Name = "Packs ViewModel";
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

    void OnAddPressed()
	{
		if (NameInput.Text == "") return;

        foreach(Pack pack in ViewModel.AllPacks)
        {
            if (pack.Name == NameInput.Text)
            {
                GD.PrintRich("[color=yellow] Pack already exists! [/color]");
                break;
            }
        }
		ViewModel.Add(NameInput.Text);
	}

    void OnEditPressed()
    {
        if (SelectedButton == null) return;

        PromptView.SetPack(SelectedButton.Text);
        EmitSignal(SignalName.ViewSwitched, PromptView);
    }

    void OnRemovePressed()
    {
        if(SelectedButton != null)
        {
            if (IsInstanceValid(MessageBox))
                MessageBox.QueueFree();

            MessageBox = MessageBox.Message(this, () =>
            {
                ViewModel.Remove(SelectedButton.Text);
                SelectedButton = null;
            });
        }   
    }

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
        newStyleBox.BorderColor = SelectedButton.GetThemeColor("SelectedColor")*new Color(0.5f,0.5f,0.5f,1f);
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
            if (button is Button)
                button.QueueFree();
        }    
    }

    public override void Back()
    {
        if (IsInstanceValid(MessageBox))
            MessageBox.QueueFree();

        base.Back();
    }
}
