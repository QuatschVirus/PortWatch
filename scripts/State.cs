using Godot;
using Godot.Collections;
using System;

public partial class State : Node
{
    private string filename;
    private Dictionary<string, Dictionary<string, Variant>> data = null;

    public PayloadResult<Dictionary<string, Variant>> GetSavedData(string key)
    {
        if (data == null)
        {
            return new PayloadResult<Dictionary<string, Variant>>(false, "Save.GetSavedData", "Data has not been loaded", new Dictionary<string, Variant>());
        }

        if (!data.ContainsKey(key))
        {
            return new PayloadResult<Dictionary<string, Variant>>(false, "Save.GetSavedData", $"Key {key} is not present", new Dictionary<string, Variant>());
        }

        return new PayloadResult<Dictionary<string, Variant>>(true, "Save.GetSavedData", $"Fetched {key} from data", data[key]);
    }

    public void SetSavedData(string key, Dictionary<string, Variant> value) { data[key] = value; }


    public State(string filename) { this.filename = filename; }

    public Result Load()
    {
        string path = "user://" + filename;
        if (!FileAccess.FileExists(path))
        {
            return new Result(false, "State.Load", "Path (" + path + ") does not exist");
        }


        FileAccess saveGame = FileAccess.Open(path, FileAccess.ModeFlags.Read);
        Json json = new Json();
        Error parseResult = json.Parse(saveGame.GetAsText());
        if (parseResult != Error.Ok)
        {
            return new Result(false, "State.Load", $"Failed to parse JSON: {json.GetErrorMessage()} at {json.GetErrorLine()} in file {path}");
        }
        data = new Dictionary<string, Dictionary<string, Variant>>((Dictionary<string, Dictionary<string, Variant>>)json.Data);

        return new Result(true, "State.Load", "Successfully loaded data into State object from " + path);
    }

    public Result Save()
    {
        string path = "user://" + filename;

        FileAccess saveGame = FileAccess.Open(path, FileAccess.ModeFlags.Write);
        saveGame.StoreLine(Json.Stringify(data));

        return new Result(true, "State.Save", "Successfully loaded data into Save object from " + path);
    }
    

}


public interface Saveable
{
    PayloadResult<Dictionary<string, Variant>> Serialize();
}

public interface Loadable
{
    Result FromSerial(Dictionary<string, Variant> data);
}