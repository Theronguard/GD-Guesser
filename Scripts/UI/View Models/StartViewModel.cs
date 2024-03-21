using Godot;
using Quiztomize.Scripts.Data_Access;
using Quiztomize.Scripts.Models;
using Quiztomize.Scripts.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class StartViewModel : ViewModel
{
    public List<Pack> AllPacks = new List<Pack>();

    public override void _Ready()
    {
        UpdateUI();
    }

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

    public void StartGame(Game.GameData data)
    {
        SceneManager.Instance.Switch(SceneManager.Instance.Game, (Node game) =>
        {
            Game gameManager = game as Game;
            if (gameManager != null)
                gameManager.PassData(data);
        });
    }

    public override void UpdateUI()
    {
        AllPacks = PackDA.GetAll();
        OnRedraw?.Invoke();
    }
}
