using Godot;
using System;
using System.Collections.Generic;

public partial class World : Node2D
{

	private PackedScene _playerScene;
	private GameManager gameManager;
	private PackedScene _scoreEntryScene;
	private GridContainer scoreGrid;

	public override void _Ready()
	{
		gameManager = (GameManager)GetNode("/root/GameManager");
		scoreGrid = GetNode<GridContainer>("ScoreGrid");
		

		List<PlayerInfo> _playersInfo;

		GD.Print($"GameManager type: {gameManager.GetSelectedType()}");


		if (gameManager.GetSelectedType() == 0) {
			_playersInfo = new List<PlayerInfo>() {
				new PlayerInfo("1", "Bolsonaro", "res://assets/presidents/bolsonaro.png", "player_one"),
				new PlayerInfo("2", "Lula", "res://assets/presidents/lula.png", "player_two"),
			};
		} else if (gameManager.GetSelectedType() == 1) {
			_playersInfo = new List<PlayerInfo>() {
				new PlayerInfo("1", "Trump", "res://assets/presidents/trump.png", "player_one"),
				new PlayerInfo("2", "Biden", "res://assets/presidents/biden.png", "player_two"),
			};
		} else {
			_playersInfo = new List<PlayerInfo>() {
				new PlayerInfo("1", "USA", "res://assets/countries/eua.png", "player_one"),
				new PlayerInfo("2", "Inglaterra", "res://assets/countries/inglaterra.png", "player_two"),
				new PlayerInfo("3", "França", "res://assets/countries/franca.png", "player_three"),
				new PlayerInfo("4", "Australia", "res://assets/countries/australia.png", "player_four"),
				new PlayerInfo("5", "Israel", "res://assets/countries/israel.png", "player_five"),
				new PlayerInfo("6", "Palestine", "res://assets/countries/palestina.png", "player_six")
			};
		}


		_playerScene = (PackedScene)ResourceLoader.Load("res://scenes/player/Player.tscn");
		_scoreEntryScene = (PackedScene)ResourceLoader.Load("res://scenes/score_entry/ScoreEntry.tscn");

		Vector2 initialPosition = new Vector2(50, 242.5f);

		float yOffset = 75.9f;

		foreach (var playerInfo in _playersInfo)
		{
			Player player = (Player)_playerScene.Instantiate();

			player.PlayerId = playerInfo.Id;
			player.PlayerName = playerInfo.Name;
			player.SpritePath = playerInfo.SpritePath;
			player.InputAction = playerInfo.InputAction;

			AddChild(player);

			player.initialPosition = initialPosition;
			player.Position = player.initialPosition;

			initialPosition.Y += yOffset;

			// Instanciar e adicionar o placar do jogador
			ScoreEntry scoreEntry = (ScoreEntry)_scoreEntryScene.Instantiate();
			scoreEntry.Position = new Vector2(yOffset, yOffset);
			scoreGrid.AddChild(scoreEntry);
			scoreEntry.SetPlayerName(player.PlayerName);
			
			// Carregar e definir a textura do jogador
			Texture2D playerTexture = (Texture2D)GD.Load(player.SpritePath);
			scoreEntry.SetPlayerTexture(playerTexture);

			scoreEntry.SetPlayerId(player.PlayerId);

			// Conectar o sinal para atualizar a pontuação
			player.Connect("ScoreUpdated", new Callable(scoreEntry, "SetScore"));
		}
	}

	private void UpdateScores()
	{
		foreach (Node child in scoreGrid.GetChildren())
		{
			if (child is ScoreEntry scoreEntry)
			{
				foreach (Node node in GetTree().GetNodesInGroup("players"))
				{
					if (node is Player player && player.PlayerName == scoreEntry.GetNode<Label>("VBoxContainer/PlayerNameLabel").Text)
					{
						scoreEntry.SetScore(player.Score);
					}
				}
			}
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
}
