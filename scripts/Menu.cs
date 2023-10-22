using Godot;
using System;

public partial class Menu : PanelContainer
{
	private game root;
	private Settings settings;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
		this.root = GetNode<game>("/root/Main");
		this.settings = GetNode<Settings>("/root/Main/SettingsContainer");

		GetNode<Button>("/root/Main/MenuContainer/CenterContainer/VBoxContainer/Settings").Pressed += () =>
		{
			this.Hide();
			settings.Show();
		};
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	public override void _Input(InputEvent @event) {
		if (@event is InputEventKey keyEvent && keyEvent.Pressed) {
			switch (keyEvent.Keycode)
			{
				case (Key.Escape):
					this.Visible = !this.Visible;
					break;
            }
        }
	}
}
