using Godot;
using System;
using System.Linq;

public partial class LevelManager : Node2D
{
	public GameManager gameManager;
    public PackedScene levelUI = ResourceLoader.Load<PackedScene>("res://Scenes/UI/LevelUI.tscn");

	public LevelUI ui;
	public class Level
	{
		public int id;
		public string name;
		public string[] contentTitles;
        public int[][] contents;
		public int nextlevel;
	}

	public Level levelData;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        gameManager = (GameManager)GetNode("/root/GameManager");
        ui = (LevelUI)levelUI.Instantiate();
		levelData = gameManager.levelData.First(level => level.name == Name);
        AddChild(ui);
		CallDeferred("UISetup");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}

	public void UISetup()
	{
        Level newLevel = new()
		{
			id = levelData.id,
			name = levelData.name,
			contentTitles = levelData.contentTitles,
			contents = levelData.contents,
			nextlevel = levelData.nextlevel,	
		};
		ui.levelData = newLevel;
		ui.CallDeferred("LoadLevelUI");
	}
}
