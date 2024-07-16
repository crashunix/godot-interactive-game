using Godot;
using System.Collections.Generic;

public partial class GameManager : Node
{
    private Dictionary<string, int> scores = new Dictionary<string, int>();
    private Label scoreLabel;

    public override void _Ready()
    {
        scoreLabel = GetNode<Label>("ScoreLabel");
        if (scoreLabel != null)
        {
            UpdateScoreLabel();
        }
        else
        {
            GD.PrintErr("ScoreLabel not found!");
        }
    }

    public void AddPoint(string playerName)
    {
        if (!scores.ContainsKey(playerName))
        {
            scores[playerName] = 0;
        }
        scores[playerName]++;
        GD.Print($"{playerName} now has {scores[playerName]} points");

        UpdateScoreLabel();
    }

    private void UpdateScoreLabel()
    {
        if (scoreLabel != null)
        {
            scoreLabel.Text = "";
            foreach (var score in scores)
            {
                scoreLabel.Text += $"{score.Key}: {score.Value}  ";
            }
        }
    }

    public void ResetScores()
    {
        scores.Clear();
    }

    public Dictionary<string, int> GetScores()
    {
        return scores;
    }
}
