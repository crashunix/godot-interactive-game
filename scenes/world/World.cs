using Godot;
using System;
using System.Collections.Generic;

public partial class World : Node2D
{

	private PackedScene _playerScene;

	private List<PlayerInfo> _playersInfo = new List<PlayerInfo>
	{
		new PlayerInfo("Brasil", "res://assets/brasil.png", "ui_left"),
		new PlayerInfo("EUA", "res://assets/eua.png", "ui_right"),
		new PlayerInfo("Eslovênia", "res://assets/eslovenia.png", "ui_up"),
		new PlayerInfo("Japão", "res://assets/japao.png", "ui_down")
	};

	public override void _Ready()
	{
		_playerScene = (PackedScene)ResourceLoader.Load("res://scenes/player/Player.tscn");

		Vector2 initialPosition = new Vector2(100, 200);

		float yOffset = 80.0f;

		foreach (var playerInfo in _playersInfo)
		{
			Player player = (Player)_playerScene.Instantiate();

			player.PlayerName = playerInfo.Name;
			player.SpritePath = playerInfo.SpritePath;
			player.InputAction = playerInfo.InputAction;

			AddChild(player);

			player.initialPosition = initialPosition;
			player.Position = player.initialPosition;

			initialPosition.Y += yOffset;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
