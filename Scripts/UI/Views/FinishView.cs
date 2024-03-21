using Godot;
using System;

public partial class FinishView : View
{
    [Export] public Node GuessedPromptsParent;
    [Export] public Node FailedPromptsParent;

    [Export] public PackedScene PromptBase;

    [ExportGroup("View")] 
    [Export] public FinishViewModel ViewModel;
    
    public override void _Ready()
    {
        ViewModel.OnRedraw += OnViewmodelRedraw;
    }

    #region Signals

    void OnViewmodelRedraw()
    {
        FillPrompts();
    }
    void OnBackPressed()
    {
        Back();
    }
    #endregion

    void ClearPrompts()
    {
        foreach (Node node in GuessedPromptsParent.GetChildren())
            node.QueueFree();
        foreach (Node node in FailedPromptsParent.GetChildren())
            node.QueueFree();
    }
    void FillPrompts()
    {
        ClearPrompts();
        foreach (string prompt in ViewModel.GuessedPrompts)
        {
            FinalPrompt finalPrompt = PromptBase.Instantiate<FinalPrompt>();
            finalPrompt.PromptLabel.Text = prompt;
            GuessedPromptsParent.AddChild(finalPrompt);
        }
        foreach (string prompt in ViewModel.FailedPrompts)
        {
            FinalPrompt finalPrompt = PromptBase.Instantiate<FinalPrompt>();
            finalPrompt.PromptLabel.Text = prompt;
            FailedPromptsParent.AddChild(finalPrompt);
        }
    }

    public override void Back()
    {
        SceneManager.Instance.Switch(SceneManager.Instance.MainMenu);
    }
}
