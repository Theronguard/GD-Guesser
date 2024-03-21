using Godot;
using Quiztomize.Scripts.View_Models;
using System;
using System.Collections.Generic;

public partial class FinishViewModel : ViewModel
{
    public List<string> GuessedPrompts = new List<string>();
    public List<string> FailedPrompts = new List<string>();
}
