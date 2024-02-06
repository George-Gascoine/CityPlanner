using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;
using Newtonsoft.Json;

public partial class GameManager : Node2D
{
	public string gameState;

	public Node2D currentPlaceable;

    //spriteJSON format = "name" : [columns, width, height]
    [Export]
    public Json levelJSON, contentsJSON, socialJSON, spriteJSON;

    public List<LevelManager.Level> levelData;
    public List<Placeable.Contents> contentData;

    public Dictionary<string, int[]> spriteInfo = new();

    public PackedScene Fade = ResourceLoader.Load<PackedScene>("res://Scenes/UI/TransitionFade.tscn");
    public PackedScene defPlaceable = ResourceLoader.Load<PackedScene>("res://Scenes/InteractiveNodes/Placeable.tscn");

    Resource defCursor = ResourceLoader.Load("res://Assets/Sprites/Cursors/defCursor.png");
    Resource hoverCursor = ResourceLoader.Load("res://Assets/Sprites/Cursors/hoverCursor.png");
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		gameState = "Place";
        ReadJSON();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public void ReadJSON()
    {
        //Level Data Parse
        JObject levels = JObject.Parse(levelJSON.Data.ToString());
        List<JToken> jLevels = levels["level"].Children().ToList();
        levelData = jLevels.Select(level => level.ToObject<LevelManager.Level>()).ToList();
        //Content Data Parse
        JObject contents = JObject.Parse(contentsJSON.Data.ToString());
        List<JToken> jContents = contents["content"].Children().ToList();
        contentData = jContents.Select(content => content.ToObject<Placeable.Contents>()).ToList();
        spriteInfo = JsonConvert.DeserializeObject<Dictionary<string, int[]>>(spriteJSON.Data.ToString());


        foreach (LevelManager.Level level in levelData) 
        { 
            GD.Print(level.name, level.id, level.contentTitles[0], level.contents[0][0], level.nextlevel);
        }
    }

	public void StateChange(string state)
	{
		gameState = state;
		switch (gameState)
		{
			case "Place":
                SetCursor("def");
				break;
            case "Spanner":
                SetCursor("hover");
                break;
            case "Clean":
                SetCursor("def");
                break;
            case "Rubbish":
                break;
            case "Photo":
                break;
        }
	}

    public void SetCursor(string type)
    {
        switch (type)
        {
            case "def":
                Input.SetCustomMouseCursor(defCursor);
                break;
            case "hover":
                Input.SetCustomMouseCursor(hoverCursor);
                break;
            default:
                // Code to execute if variable does not equal any cases
                break;
        }
    }

    public void SelectPlaceable(Placeable.Contents contents)
    {
        GD.Print(contents.name);
    }
}
