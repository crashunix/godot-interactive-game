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
				new PlayerInfo("Bolsonaro", "res://assets/bolsonaro.png", "player_one"),
				new PlayerInfo("Lula", "res://assets/lula.png", "player_two"),
			};
		} else if (gameManager.GetSelectedType() == 1) {
			_playersInfo = new List<PlayerInfo>() {
				new PlayerInfo("Trump", "res://assets/trump.png", "player_one"),
				new PlayerInfo("Biden", "res://assets/biden.png", "player_two"),
			};
		} else {
			_playersInfo = new List<PlayerInfo>() {
				new PlayerInfo("EUA", "res://assets/eua.png", "player_one"),
				new PlayerInfo("Eslovênia", "res://assets/eslovenia.png", "player_two"),
				new PlayerInfo("Brasil", "res://assets/brasil.png", "player_three"),
				new PlayerInfo("Japão", "res://assets/japao.png", "player_four")
			};
		}


		_playerScene = (PackedScene)ResourceLoader.Load("res://scenes/player/Player.tscn");
		_scoreEntryScene = (PackedScene)ResourceLoader.Load("res://scenes/score_entry/ScoreEntry.tscn");

		Vector2 initialPosition = new Vector2(50, 200);

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

			// Instanciar e adicionar o placar do jogador
            ScoreEntry scoreEntry = (ScoreEntry)_scoreEntryScene.Instantiate();
			scoreEntry.Position = new Vector2(yOffset, yOffset);
            scoreGrid.AddChild(scoreEntry);
            scoreEntry.SetPlayerName(player.PlayerName);
			
			// Carregar e definir a textura do jogador
            Texture2D playerTexture = (Texture2D)GD.Load(player.SpritePath);
            scoreEntry.SetPlayerTexture(playerTexture);

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
