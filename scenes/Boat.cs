using Godot;
using Godot.Collections;
using System;

public partial class Boat : Node2D
{
	[Export]
	public Array<Color> COLORS;

	public ShipType ShipType { get; private set; }
	public string Callsign { get; private set; }
	public string DisplayName { get; private set; }
	public string Tag { get; private set; }

	public Vector2 Pos { get; private set; }

	private BoatState state;
	public BoatState State
    {
        get { return state; }
        set
        {
            state = value;
			Update();
        }
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Update()
	{
		SelfModulate = COLORS[(int)state];
	}
}

public enum ShipType
{
	BOAT,
	SHIP,
	FREIGHTER
}

public enum BoatState
{
	NORMAL,
	DISTRESS,
	SUNKEN,
	ANCHORING,
	DAMAGED
}