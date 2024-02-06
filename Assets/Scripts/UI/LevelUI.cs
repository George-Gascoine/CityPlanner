using Godot;
using System;
using System.Linq;

public partial class LevelUI : CanvasLayer
{

    private GameManager gameManager;
    public LevelManager.Level levelData = new();
    public TabContainer Container;

    public PackedScene defNewTab = ResourceLoader.Load<PackedScene>("res://Scenes/UI/ContentsContainer.tscn");
    public PackedScene defButton = ResourceLoader.Load<PackedScene>("res://Scenes/UI/PlaceableButton.tscn");

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        gameManager = (GameManager)GetNode("/root/GameManager");
        Container = GetNode<TabContainer>("Contents");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}

    public void LoadLevelUI() 
    {
        int i = 0;
        foreach (string title in levelData.contentTitles)
        {
            MarginContainer titleContainer = (MarginContainer)defNewTab.Instantiate();
            Container.AddChild(titleContainer);
            foreach (int contentsID in levelData.contents[i])
            {
                GD.Print("ContentID " + levelData.contents[i]);
                PlaceableButton button = (PlaceableButton)defButton.Instantiate();
                button.buttonData = gameManager.contentData.First(x => x.id == contentsID);
                titleContainer.GetNode<GridContainer>("ButtonContainer").CallDeferred("add_child", button);
                button.CallDeferred("ButtonSetup");
                //AddChild(button);
            }
            i++;
        }
    }

    public void SwitchState(string state)
    {
        GD.Print(state);
        gameManager.StateChange(state);
    }
}
