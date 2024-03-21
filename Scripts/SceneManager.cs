using Godot;
using System;

public partial class SceneManager : Node
{
    public static SceneManager Instance;

    [ExportGroup("Scenes")][Export] public PackedScene MainMenu;
    [ExportGroup("Scenes")][Export] public PackedScene Game;

    public delegate void SceneDataTransfer(Node instantiatedNode);

    public override void _Ready()
    {
        if (Instance == null)
            Instance = this;
        else
            throw new Exception("Recreated an already existing singleton!");
    }

	public Node Switch(PackedScene scene)
	{
        Node newNode = scene.Instantiate();
        GetTree().Root.AddChild(newNode);
        GetTree().CurrentScene.QueueFree();
        GetTree().CurrentScene = newNode;

        return newNode;
    }

    public Node Switch(PackedScene scene, SceneDataTransfer passData)
    {
        Node newNode = scene.Instantiate();
        passData(newNode);
        GetTree().Root.AddChild(newNode);
        GetTree().CurrentScene.QueueFree();
        GetTree().CurrentScene = newNode;
        return newNode;
    }
}
