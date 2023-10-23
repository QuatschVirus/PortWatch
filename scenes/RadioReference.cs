using Godot;
using System;

public partial class RadioReference : HBoxContainer
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
        GetNode<ItemList>("ItemList").ItemClicked += setScroll;
	}

    private void setScroll(long index, Vector2 atPosition, long mouseButtonIndex)
    {
        string name = GetNode<ItemList>("ItemList").GetItemText((int)index);
        GD.Print(name);
        Control child = GetNode<VBoxContainer>("ScrollContainer/VBoxContainer").GetNode<Control>(name);
        int scroll = (int)child.Position.Y;
        GD.Print(scroll);
        GetNode<ScrollContainer>("ScrollContainer").ScrollVertical = scroll;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
}
