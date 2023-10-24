using Godot;
using Godot.Collections;
using System;

public partial class Radio : PanelContainer
{
	[Export]
	public Array<Color> COLORS;

	private RichTextLabel radioLog;

	private Array<string> callsigns;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		radioLog = GetNode<RichTextLabel>("../HBoxContainer2/RichTextLabel");
		GetNode<Button>("HBoxContainer/Button").Pressed += () =>
		{
			
		};
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Send(string callsign, string message, RadioID id)
	{
		if (id == 0)
		{
			throw new ArgumentOutOfRangeException(nameof(id), id, "ID 0 is reserved and may not be used");
		}
		radioLog.PushColor(COLORS[0]);
		radioLog.AppendText($"[{callsign}] ");
		radioLog.Pop();
		radioLog.PushColor(COLORS[(int)id]);
		radioLog.AppendText(message);
		radioLog.Pop();
		radioLog.AppendText("\n");
	}
}

public enum RadioID
{
	RESERVED,
	PLAYER,
	SHORE,
	ELEVATED,
	NORMAL
}