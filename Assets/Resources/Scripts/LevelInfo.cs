using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelInfo : MonoBehaviour
{
	public static LevelInfo instance;

	public Transform StartPosition;
	public Tile WaterTile;

	[SerializeField]
	private Tilemap levelTiles;

	private void Awake()
	{
		if (!instance)
			instance = this;
		else
			Debug.LogError("Level Already Spawned!!!");

		if (!levelTiles)
			Debug.LogError("No level tiles assigned!");
	}

	public TileBase GetTileAtPos(Vector3 position)
	{
		Vector3Int posInt = new Vector3Int(Mathf.FloorToInt(position.x), Mathf.FloorToInt(position.y), Mathf.RoundToInt(position.z));
		return levelTiles.GetTile(posInt);
	}

	public bool IsAtWaterTile(Vector3 position)
	{
		var tile = GetTileAtPos(position);

		return (tile != null && tile.Equals(WaterTile));
	}
}
