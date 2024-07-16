public class PlayerInfo
{
    public string Name { get; set; }
    public string SpritePath { get; set; }
    public string InputAction { get; set; }

    public PlayerInfo(string name, string spritePath, string inputAction)
    {
        Name = name;
        SpritePath = spritePath;
        InputAction = inputAction;
    }
}
