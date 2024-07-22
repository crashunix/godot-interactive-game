public class PlayerInfo
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string SpritePath { get; set; }
    public string InputAction { get; set; }

    public PlayerInfo(string id, string name, string spritePath, string inputAction)
    {
        Id = id;
        Name = name;
        SpritePath = spritePath;
        InputAction = inputAction;
    }
}
