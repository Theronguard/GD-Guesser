using Godot;
using Quiztomize.Scripts.Data_Access;
using Quiztomize.Scripts.Models;
using Quiztomize.Scripts.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class PacksViewModel : ViewModel
{
    public List<Pack> AllPacks = new List<Pack>();

    public void Add(string packName)
    {
        Pack pack = new Pack(packName);
        PackDA.Save(pack);
        UpdateUI();
    }

    public void Remove(string packName)
    {
        PackDA.Delete(packName);
        UpdateUI();
    }

    public override void UpdateUI()
    {
        AllPacks = PackDA.GetAll();
        base.UpdateUI();
    }
}
