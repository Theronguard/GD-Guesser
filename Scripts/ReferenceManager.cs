using Godot;
using System;

public partial class ReferenceManager : Node
{
    public static ReferenceManager Instance;

	[Export] public PackedScene MessageBox;

    public override void _Ready()
    {
        if (Instance == null)
            Instance = this;
        else
            throw new Exception("Recreated an already existing singleton!");
    }
}
