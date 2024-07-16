using Godot;
using System;

public partial class MainMenu : Node2D
{

	private GameManager gameManager;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		gameManager = (GameManager)GetNode("/root/GameManager");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _on_play_pressed(int arg)
	{
		gameManager.SetSelectedType(arg);
		GetTree().ChangeSceneToFile("res://scenes/world/World.tscn");
	}
	
	public void _on_quit_pressed()
	{
		GetTree().Quit();
	}
}
