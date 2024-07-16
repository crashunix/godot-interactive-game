using Godot;

public partial class FinishLine : Area2D
{
    private GameManager gameManager;

    public override void _Ready()
    {
        gameManager = (GameManager)GetNode("%GameManager");
    }

    public void _on_body_entered(Node2D body)
    {
        if (body is Player player)
        {
            gameManager.AddPoint(player.PlayerName);

            // Reset players
            ResetPlayers();
        }
    }

    private void ResetPlayers()
    {
        foreach (Node node in GetTree().GetNodesInGroup("players"))
        {
            if (node is Player player)
            {
                player.ResetPosition();
            }
        }
    }

}
