using Godot;
using System;
using static Placeable;
using System.Linq;

public partial class PlaceableButton : Button
{
	public GameManager gameManager;
	public Contents buttonData;
    public Sprite2D buttonSprite;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        gameManager = (GameManager)GetNode("/root/GameManager");
        buttonSprite = GetNode<Sprite2D>("PlaceableSprite");
    }

    public void ButtonSetup()
    {
        GD.Print("CONTENTID" + buttonData.spriteName);
        int[] spriteInfo = gameManager.spriteInfo.First(array => array.Key == buttonData.spriteName).Value;
        GetSprite(buttonSprite, buttonData.id, spriteInfo[0], spriteInfo[1], spriteInfo[2], "res://Assets/Sprites/Contents/" + buttonData.spriteName + ".png");
        Action buttonAction = () => { gameManager.SelectPlaceable(buttonData); };
        Connect("pressed", Callable.From(buttonAction));
    }
    public void GetSprite(Sprite2D sprite, int spriteID, int columns, int width, int height, string texturePath)
    {
        int spriteColumns = columns;
        sprite.RegionEnabled = true;
        Texture2D texture = (Texture2D)ResourceLoader.Load(texturePath);
        sprite.Texture = texture;
        int spriteX = Mathf.FloorToInt(spriteID % spriteColumns);
        int spriteY = (spriteID / spriteColumns);
        sprite.RegionRect = new Rect2((spriteX * width), spriteY * height, new Vector2(width, height));
    }
}
