using Godot;
using System.Collections.Generic;

public partial class Player : CharacterBody2D
{
    public const float Speed = 300.0f;
    public const float MoveDistance = 50.0f; // Distância fixa a ser percorrida

    [Export] public Vector2 initialPosition;
    private bool isMoving = false;
    private Vector2 targetPosition;
    private Queue<Vector2> movementQueue = new Queue<Vector2>(); // Fila de movimentos

    [Export] public string PlayerId = "1";
    [Export] public string PlayerName = "Player";
    [Export] public string SpritePath;
    [Export] public string InputAction = "player_one";

    [Export] public int Score = 0;

    [Signal]
    public delegate void ScoreUpdatedEventHandler(int score);

    public override void _Ready()
    {
        Sprite2D sprite = new Sprite2D();
        sprite.Texture = (Texture2D)GD.Load(SpritePath);
        sprite.Scale = new Vector2(0.233f, 0.233f);
        AddChild(sprite);
        AddToGroup("players");

        GD.Print($"Player {PlayerName} ready with input action: {InputAction}");
    }

    public override void _PhysicsProcess(double delta)
    {
        if (Input.IsActionJustPressed(InputAction))
        {
            movementQueue.Enqueue(new Vector2(MoveDistance, 0)); // Adiciona um novo movimento na fila

            if (!isMoving)
            {
                StartNextMovement();
            }
        }

        if (isMoving)
        {
            Vector2 direction = (targetPosition - Position).Normalized();
            Vector2 velocity = direction * Speed * (float)delta;

            if (Position.DistanceTo(targetPosition) <= velocity.Length())
            {
                Position = targetPosition;
                isMoving = false;

                if (movementQueue.Count > 0)
                {
                    StartNextMovement();
                }
            }
            else
            {
                Position += velocity;
            }
        }
    }

    private void StartNextMovement()
    {
        if (movementQueue.Count > 0)
        {
            Vector2 nextMove = movementQueue.Dequeue();
            targetPosition = Position + nextMove;
            isMoving = true;
        }
    }

    public void ResetPosition()
    {
        Position = initialPosition;
        isMoving = false;
        movementQueue.Clear(); // Limpa a fila ao resetar a posição
    }

    public void AddScore(int score)
    {
        Score += score;
        EmitSignal(nameof(ScoreUpdated), Score);
        GD.Print($"{PlayerName} now has {Score} points");
    }
}
