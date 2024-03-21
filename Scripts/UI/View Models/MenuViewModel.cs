using Godot;
using Quiztomize.Scripts.View_Models;
using System;

public partial class MenuViewModel : ViewModel
{
    public void QuitGame()
    {
        GetTree().Quit();
    }
}
