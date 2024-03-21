using Godot;
using Quiztomize.Scripts;
using Quiztomize.Scripts.Models;
using System;

public partial class PromptsView : View
{
    public PromptsViewModel ViewModel = null;

    [Export] Control PromptNodeContainer;
    [Export] PackedScene PromptUIPrefab;
    [Export] Label TitleLabel;
    [Export] LineEdit PromptInput;

    private MessageBox MessageBox = null;

    public override void _Ready()
    {
        if (ViewModel == null)
        {
            ViewModel = new PromptsViewModel();
            ViewModel.Name = "Prompts ViewModel";
            ViewModel.OnRedraw += OnViewmodelRedraw;

            //GetTree().CurrentScene.CallDeferred(MethodName.AddChild, ViewModel);
            CallDeferred(MethodName.AddChild, ViewModel);
        }
    }

    #region Signals

    public void OnViewmodelRedraw()
    {
        CreatePrompts();
    }

    public void OnAddButtonPressed()
    {
        ViewModel.AddPrompt(PromptInput.Text);
    }

    public void OnDeleteButtonPressed(PromptButton button)
    {
        if (IsInstanceValid(MessageBox))
            MessageBox.QueueFree();

        MessageBox = MessageBox.Message(this, () =>
        {
            ViewModel.RemovePrompt(button.Text);
        });
    }

    public void OnBackPressed()
    {
        Back();
    }
    #endregion

    public void SetPack(string packName)
    {
        ViewModel.PackName = packName;
        TitleLabel.Text = packName;
        ViewModel.UpdateUI();
    }

    void CreatePrompts()
    {
        foreach (Node button in PromptNodeContainer.GetChildren())
        {
            if(button is Button)
                button.QueueFree();
        }
            

        if (ViewModel.CurrentPack == null) return;

        foreach (string prompt in ViewModel.CurrentPack.Prompts)
        {
            PromptButton uiButton = PromptUIPrefab.Instantiate() as PromptButton;
            uiButton.DeleteButton.Pressed += () => OnDeleteButtonPressed(uiButton);
            uiButton.Text = prompt;
            PromptNodeContainer.AddChild(uiButton);
        }
    }

    public override void Back()
    {
        if (IsInstanceValid(MessageBox))
            MessageBox.QueueFree();

        base.Back();
    }
}
