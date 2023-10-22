using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class game : Node2D
{
	private SaveSystem Saves;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Saves = GetNode<SaveSystem>("/root/Saves");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public Result Save()
	{
		return Saves.Save();
	}
}

public class Result
{
	private bool Success { get; }
	private string operation;
	private string message;
	
	public Result(bool success, string operation, string message, bool forceLog=false)
	{
		this.Success = success;
		this.operation = operation;
		this.message = message;

		if (!success)
		{
			GD.PushError((string)this);
		} else if (forceLog)
		{
			GD.Print((string)this);
		}
	}

	public static implicit operator bool(Result result)
	{
		return result.Success;
	}

	public static implicit operator string(Result result)
	{
		return (result.Success ? "[SUCESS] " : "[FAILURE] ") + result.operation + " : " + result.message;
	}
}

public class PayloadResult<T> : Result
{
	private T Payload { get; }

    public PayloadResult(bool success, string operation, string message, T payload, bool forceLog = false) : base(success, operation, message, forceLog)
    {
		Payload = payload;
    }

	public static implicit operator T(PayloadResult<T> result)
	{
		return result.Payload;
	}
}