using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WaveFunctionCollapse : MonoBehaviour
{
    public Tilemap roadTilemap;          // Tilemap where road tiles will be placed
    public Tile placeholderTile;         // Tile used as a guide for generation
    public Tile[] roadTiles;             // Array of road tiles to randomly choose from
    public Camera mainCamera;            // Camera to get the screen bounds

    private List<Vector3Int> placeholderPositions = new List<Vector3Int>();  // List of positions with placeholders

    void Start()
    {
        FindPlaceholderPositions();
        GenerateRoads();
    }

    // Step 1: Find positions with placeholder tiles across the entire scene
    void FindPlaceholderPositions()
    {
        placeholderPositions.Clear();

        // Get the world bounds from the camera (i.e., the visible area in the scene)
        Vector3 cameraBottomLeft = mainCamera.ScreenToWorldPoint(Vector3.zero);
        Vector3 cameraTopRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        // Convert world bounds to grid coordinates
        Vector3Int bottomLeftCell = roadTilemap.WorldToCell(cameraBottomLeft);
        Vector3Int topRightCell = roadTilemap.WorldToCell(cameraTopRight);

        // Iterate through all positions in the camera view range
        for (int x = bottomLeftCell.x; x <= topRightCell.x; x++)
        {
            for (int y = bottomLeftCell.y; y <= topRightCell.y; y++)
            {
                Vector3Int position = new Vector3Int(x, y, 0);
                if (roadTilemap.HasTile(position) && roadTilemap.GetTile(position) == placeholderTile)
                {
                    placeholderPositions.Add(position);
                }
            }
        }
    }

    // Step 2: Generate road tiles only at placeholder positions
    void GenerateRoads()
    {
        foreach (Vector3Int position in placeholderPositions)
        {
            PlaceRandomTile(position);
        }
    }

    // Step 3: Place a random road tile at a specific position
    void PlaceRandomTile(Vector3Int position)
    {
        if (roadTiles.Length > 0)
        {
            Tile randomTile = roadTiles[Random.Range(0, roadTiles.Length)];
            roadTilemap.SetTile(position, randomTile);
        }
    }
}
