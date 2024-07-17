using Godot;
using System.Collections.Generic;

public partial class GameManager : Node
{
    private Dictionary<string, int> scores = new Dictionary<string, int>();
    private int SelectedType = 0;

    public override void _Ready()
    {
        GD.Print("[GameManager] ~ i'm ready!");
    }

    public void SetSelectedType(int type) {
        SelectedType = type;
    }

    public int GetSelectedType() {
        return SelectedType;
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
