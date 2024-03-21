using Godot;
using Quiztomize.Scripts.Data_Access;
using Quiztomize.Scripts.Models;
using Quiztomize.Scripts.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class PromptsViewModel : ViewModel
{
    public string PackName;

    public Pack CurrentPack = null;

    public void AddPrompt(string prompt)
    {
        CurrentPack = PackDA.Get(PackName);
        CurrentPack.Prompts.Add(prompt);
        PackDA.Save(CurrentPack);
        UpdateUI();
    }

    public void RemovePrompt(string prompt)
    {
        if (CurrentPack == null) return;

        Pack pack = PackDA.Get(CurrentPack.Name);

        if(pack == null) return;

        pack.Prompts.Remove(prompt);
        PackDA.Save(pack);

        UpdateUI();
    }

    public override void UpdateUI()
    {
        CurrentPack = PackDA.Get(PackName);
        base.UpdateUI();
    }
}
