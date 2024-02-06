using Godot;
using System;
using System.Linq;

public partial class Placeable : Node2D
{
	private GameManager gameManager;
	public class Contents
	{
		public int id;
		public string name;
		public string spriteName;
		public int[] size;
		public string layer;
		public bool ontop;
	}

	public Contents contents;

	public Sprite2D sprite;
	public Area2D area;
	public CollisionShape2D collisionShape;
	public Vector2 location;
	public bool canBePlaced, isPlaced;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        gameManager = (GameManager)GetNode("/root/GameManager");
        sprite = GetNode<Sprite2D>("PlaceableSprite");
		int[] spriteInfo = gameManager.spriteInfo.First(array => array.Key == contents.spriteName).Value;
        GetSprite(sprite, contents.id, spriteInfo[0], spriteInfo[1], spriteInfo[2], "res://Assets/Sprites/Contents/" + contents.spriteName + ".png");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (canBePlaced)
		{
			Position = ToIsometricGrid(GetGlobalMousePosition());
        }
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

    public Vector2 ToIsometricGrid(Vector2 localPosition)
    {
        float xx = Mathf.Round(localPosition.Y / 16 + localPosition.X / 32);// X grid coordinate based on real world coordinate
        float yy = Mathf.Round(localPosition.Y / 16 - localPosition.X / 32);// Y grid coordinate based on real world coordinate

        float x = (xx - yy) * 0.5f * 32; //Converts the tile coordinate to its origin real world coordinate
        float y = (xx + yy) * 0.5f * 16; //Converts the tile coordinate to its origin real world coordinate
        return new Vector2(x, y);
    }
}
