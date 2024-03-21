using Godot;

using System;

public partial class MenuView : View
{
    [Export] View PacksView;
    [Export] View StartView;

    private MenuViewModel ViewModel = null;
    public override void _Ready()
    {
        if (ViewModel == null)
        {
            ViewModel = new MenuViewModel();
            ViewModel.Name = "Menu ViewModel";
            CallDeferred(MethodName.AddChild, ViewModel);
        }
    }

    void OnPacksPressed()
    {
        EmitSignal(SignalName.ViewSwitched, PacksView);
    }
    void OnStartPressed()
    {
        EmitSignal(SignalName.ViewSwitched, StartView);
    }
    void OnQuitPressed()
    {
        ViewModel.QuitGame();
    }
}
