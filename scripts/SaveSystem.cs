using Godot;
using Godot.Collections;

public partial class SaveSystem : Node
{
	public State State { get; private set; }

	public Result Save()
	{
        return new Result(true, "SaveSystem.Save", "Successfully saved save");
    }

	public Result Load()
	{
        return new Result(true, "SaveSystem.Load", "Successfully loaded save");
    }

	public Result SetupSave(string filename)
	{
		State = new State(filename);
		GD.Print(filename);
        return new Result(true, "SaveSystem.SetupSave", "Save object was correctly setup");
    }
}