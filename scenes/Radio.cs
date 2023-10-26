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
		radioLog.PushColor(COLORS[(int)id]);
		radioLog.AppendText(callsign + "to Tower: " + message);
		radioLog.Pop();
		radioLog.AppendText("\n");
	}

	public void AddCallsign(string callsign)
	{
		GetNode<OptionButton>("/PanelContainer/HBoxContainer/OptionButton").AddItem(callsign);
	}

	public void RemoveCallsign(string callsign)
	{
		OptionButton button = GetNode<OptionButton>("/PanelContainer/HBoxContainer/OptionButton");
		for (int indexer = 0; indexer < button.ItemCount; indexer++)
		{
			if(button.GetItemText(indexer).Equals(callsign))
			{
				button.RemoveItem(indexer);
			}
		}

    }
}

public enum RadioID
{
	PLAYER,
	SHORE,
	ELEVATED,
	NORMAL
}