using Godot;
using Godot.Collections;

public partial class Boat : Node2D
{
	[Export]
	public Array<Color> COLORS;
	[Export]
	public Color HOVER_MASK;

	public ShipType ShipType { get; private set; }
	public string Callsign { get; private set; }
	public string DisplayName { get; private set; }
	public string Tag { get; private set; }

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

	private Sprite2D sprite;
	public Label Label { get; private set; }

	private bool wasHovered;

	[Signal]
	public delegate void HideLabelsEventHandler();

	public Boat()
	{
		// throw new NotImplementedException();
	}

	public Boat(string callsign, string name, ShipType shipType, BoatState state)
	{
		Callsign = callsign;
		DisplayName = name;
		ShipType = shipType;
		this.state = state;
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		COLORS = GetNode<Boat>("/root/Main/BoatPrefab").COLORS;
		HOVER_MASK = GetNode<Boat>("/root/Main/BoatPrefab").HOVER_MASK;

        sprite = (Sprite2D)GetNode("/root/Main/BoatPrefab/Sprite2D").Duplicate();
        Label = (Label)GetNode("/root/Main/BoatPrefab/Label").Duplicate();

		sprite.Texture = ImageTexture.CreateFromImage(Image.LoadFromFile($"res://textures/{ShipType}.png"));
        Label.Text = $"{Callsign} - {DisplayName}";

        AddChild(sprite);
        AddChild(Label);

		Show();

        Update();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public void Update()
	{
		if (!wasHovered)
		{
            sprite.SelfModulate = COLORS[(int)state];
        } else
		{
			sprite.SelfModulate = COLORS[(int)state].Blend(HOVER_MASK);
        }
	}

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseMotion)
		{
			if (sprite.IsPixelOpaque(sprite.ToLocal(GetGlobalMousePosition())) ^ wasHovered)
			{
				wasHovered = !wasHovered;
				Update();
			}
		} else if (@event is InputEventMouseButton)
		{
			InputEventMouseButton mouseEvent = @event as InputEventMouseButton;
			if (mouseEvent.ButtonIndex == MouseButton.Left)
			{
				if (sprite.IsPixelOpaque(sprite.ToLocal(mouseEvent.GlobalPosition)))
				{
					Label.Show();
				}
            } 
		}
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