using Godot;
using Godot.Collections;
using System.IO;

public partial class SaveSystem : Node
{
	public State State { get; private set; }

	public Result Save()
	{
		Array<Node> group = GetTree().GetNodesInGroup("CallToSave");

		foreach (Node node in group)
		{
			if (node is not Saveable)
			{
				return new Result(false, "SaveSystem.Save", $"{node.GetType().Name} is not saveable");
			}
			Saveable saveNode = node as Saveable;
			PayloadResult<Dictionary<string, Variant>> result1 = saveNode.Serialize();
			if (!result1) { return result1; }
			State.SetSavedData(node.GetType().Name, result1.Payload);
		}

		Result result = State.Save();
        if (!result) { return result; }

        return new Result(true, "SaveSystem.Save", "Successfully saved save");
    }

	public Result Load()
	{
		Result result = State.Load();
        if (!result) { return result; }

        Array<Node> group = GetTree().GetNodesInGroup("CallToLoad");

		foreach (Node node in group)
		{
            if (node is not Loadable)
            {
                return new Result(false, "SaveSystem.Load", $"{node.GetType().Name} is not loadable");
            }
            Loadable loadNode = node as Loadable;

			PayloadResult<Dictionary<string, Variant>> result1 = State.GetSavedData(node.GetType().Name);
			if (!result1) { return result1; }
			Result result2 = loadNode.FromSerial(result1.Payload);
            if (!result2) { return result2; }
        }

        return new Result(true, "SaveSystem.Load", "Successfully loaded save");
    }

	public Result SetupSave(string filename)
	{
		State = new State(filename);
        return new Result(true, "SaveSystem.SetupSave", "Save object was correctly set up");
    }

	public static string[] GetAvailableSaves()
	{
		return Directory.GetFiles(ProjectSettings.GlobalizePath("user://"), "*.save");
	}
}