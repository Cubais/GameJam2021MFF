using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelInfo : MonoBehaviour
{
	public static LevelInfo instance;

	public Transform StartPosition;
	public TileBase[] WaterTiles;

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
		if (tile == null)
			return false;


		for (int i = 0; i < WaterTiles.Length; i++)
		{
			if (tile.Equals(WaterTiles[i]))
				return true;
		}

		return false;
	}
}
