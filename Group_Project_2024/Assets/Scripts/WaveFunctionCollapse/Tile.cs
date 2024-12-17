using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "NewTile", menuName = "Tile/CustomTile")]
public class Tile : TileBase
{
    public Tile[] upNeighbours;
    public Tile[] rightNeighbours;
    public Tile[] downNeighbours;
    public Tile[] leftNeighbours;

    // Reference to the sprite for this tile
    public Sprite tileSprite;

    // Optionally, you can add other tile properties (e.g., walkability, type)
    public bool isWalkable = true;

    // Override GetTileData to set the sprite and other data for the tile
    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        // Set the sprite to be used by this tile
        tileData.sprite = tileSprite;
        
        // You can set additional properties like color, transform, flags, etc.
        tileData.flags = TileFlags.LockAll;  // Lock tile properties (optional)
        
        // No colliderType line needed since you're not using colliders
        // tileData.colliderType = Tile.ColliderType.None;  // Remove this line
    }
}
