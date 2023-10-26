using Godot;
using System;

public partial class Map : Panel
{
	private Radio radio;

	[Signal]
	public delegate void HideLabelsEventHandler();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		radio = GetNode<Radio>("%Radio");
		GenerateBoat(); // for debugging purposes
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton)
		{
			InputEventMouseButton mouseEvent = @event as InputEventMouseButton;
            if (mouseEvent.ButtonIndex == MouseButton.Left)
            {
                if (GetGlobalRect().HasPoint(mouseEvent.GlobalPosition))
				{
					EmitSignal(SignalName.HideLabels);
				}
            }
        }
	}

	public void RemoveBoat(Boat boat)
	{
		radio.RemoveCallsign(boat.Callsign);

		boat.Free();
	}


    public void GenerateBoat()
	{
		// Do stuff
		Boat boat = new Boat("TEST", "TestShip", ShipType.SHIP, BoatState.NORMAL); //temp, for testing
		boat.HideLabels += () => { EmitSignal(SignalName.HideLabels); };
		HideLabels += () => { boat.Label.Hide(); };

		AddChild(boat);
	}

	public Boat GetBoat(string callsign)
	{
		foreach (Boat boat in GetChildren())
		{
			if (boat.Callsign == callsign)
			{
				return boat;
			}
		}
		return null;
	}
}
