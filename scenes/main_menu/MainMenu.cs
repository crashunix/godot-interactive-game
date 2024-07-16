using Godot;
using System;

public partial class MainMenu : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Hello World!");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _on_play_pressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/world/World.tscn");
	}
	
	public void _on_quit_pressed()
	{
		GetTree().Quit();
	}
}
