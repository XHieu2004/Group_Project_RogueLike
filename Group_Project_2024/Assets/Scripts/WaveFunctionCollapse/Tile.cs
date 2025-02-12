using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewTile", menuName = "Tile/CustomTile")]
[System.Serializable]
public class Tile : UnityEngine.Tilemaps.Tile
{
    public List<Tile> upNeighbours = new List<Tile>();
    public List<Tile> rightNeighbours = new List<Tile>();
    public List<Tile> downNeighbours = new List<Tile>();
    public List<Tile> leftNeighbours = new List<Tile>();

    public Sprite tileSprite;
    public bool isWalkable = true;

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        tileData.sprite = tileSprite;
        tileData.flags = TileFlags.LockAll;
    }
}