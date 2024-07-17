using Godot;

public partial class ScoreEntry : HBoxContainer
{
	private TextureRect playerTextureRect;
	private Label playerNameLabel;
	private Label scoreLabel;

	public override void _Ready()
	{
		// Verifique se os nós foram encontrados corretamente
		playerTextureRect = GetNode<TextureRect>("ColorRect/TextureRect");
		playerNameLabel = GetNode<Label>("VBoxContainer/PlayerNameLabel");
		scoreLabel = GetNode<Label>("VBoxContainer/HBoxContainer/ScoreLabel");

		playerNameLabel.Text = "Player";
		scoreLabel.Text = "0";

		if (playerTextureRect == null)
		{
			GD.PrintErr("playerTextureRect is null!");
		}
		if (playerNameLabel == null)
		{
			GD.PrintErr("playerNameLabel is null!");
		}
		else
		{
			GD.Print($"playerNameLabel: {playerNameLabel.Text}");
		}
		if (scoreLabel == null)
		{
			GD.PrintErr("scoreLabel is null!");
		}
		else
		{
			GD.Print($"scoreLabel: {scoreLabel.Text}");
		}
	}

	public void SetPlayerTexture(Texture2D texture)
    {
        if (playerTextureRect != null)
        {
            playerTextureRect.Texture = texture;
			// playerTextureRect.Scale = new Vector2(0.1f, 0.1f);
        }
    }

	public void SetPlayerName(string name)
	{
		playerNameLabel.Text = name;
	}

	public void SetScore(int score)
	{
		scoreLabel.Text = score.ToString();
	}
}
