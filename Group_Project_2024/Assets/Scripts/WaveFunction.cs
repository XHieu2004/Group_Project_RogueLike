using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine.Tilemaps;
using UnityEngine;

public class WaveFunction : MonoBehaviour
{
    public int dimensions;
    public Tile[] tileObjects;
    public List<Cell> gridComponents;
    public Cell cellObj;

    int iterations = 0;

    void CenterCameraOnMap()
    {
    float tileSize = cellObj.GetComponent<Tilemap>().cellSize.x; // Assuming square tiles
    float gridWidth = dimensions * tileSize;
    float gridHeight = dimensions * tileSize;

    // Calculate the center position of the grid
    Vector3 gridCenter = new Vector3(gridWidth / 2f, gridHeight / 2f, 0);

    // Set the camera's position to the grid center
    Camera.main.transform.position = new Vector3(gridCenter.x, gridCenter.y, Camera.main.transform.position.z);
    }


    void AdjustCameraToFitMap()
    {
        // Get the tile size and grid dimensions
        Vector2 tileSize = cellObj.GetComponent<Tilemap>().cellSize;
        float gridWidth = dimensions * (tileSize.x + 0.1f); // Include spacing between tiles
        float gridHeight = dimensions * (tileSize.y + 0.1f); // Include spacing between tiles

        // Calculate the required camera orthographic size to fit the map
        Camera.main.orthographicSize = Mathf.Max(gridWidth, gridHeight) / 2f;

        // Account for the camera's aspect ratio
        float aspectRatio = (float)Screen.width / Screen.height;

        // Calculate the center position of the grid
        float centerX = gridWidth / 2f;
        float centerY = gridHeight / 2f;

        // Fine-tuning offsets (adjust these values manually)
        float offsetX = 7.5f; // Adjust this value to move the camera horizontally (positive: move right, negative: move left)
        float offsetY = 0f;   // Adjust this value to move the camera vertically (positive: move up, negative: move down)

        // Adjust the camera's position to be centered
        if (aspectRatio > 1) // Landscape mode
        {
            Camera.main.transform.position = new Vector3(centerX + offsetX, centerY + offsetY, Camera.main.transform.position.z);
        }
        else // Portrait mode
        {
            Camera.main.transform.position = new Vector3(centerX + offsetX, centerY + offsetY, Camera.main.transform.position.z);
        }
    }

    void Awake()
    {
        gridComponents = new List<Cell>();
        InitializeGrid();
        CenterCameraOnMap();
        AdjustCameraToFitMap();
    }

    void InitializeGrid()
    {
        // Get the tile size from the Tilemap component
        Vector2 tileSize = cellObj.GetComponent<Tilemap>().cellSize;

        // Ensure there is no overlap by adding spacing to the tile positions
        float spacing = 1.4f; // Adjust this value to increase/decrease the space between tiles
        
        // Calculate the position offset to prevent overlap (this will use the original tile size + spacing)
        for (int y = 0; y < dimensions; y++)
        {
            for (int x = 0; x < dimensions; x++)
            {
                // Calculate the position of each tile based on its size + spacing
                Vector2 tilePosition = new Vector2(x * (tileSize.x + spacing), y * (tileSize.y + spacing));

                // Instantiate the cell at the calculated position
                Cell newCell = Instantiate(cellObj, tilePosition, Quaternion.identity);

                // No scaling or size change, tile size is based on the Inspector settings
                newCell.CreateCell(false, tileObjects);
                gridComponents.Add(newCell);
            }
        }

        StartCoroutine(CheckEntropy());
    }








    IEnumerator CheckEntropy()
    {
        List<Cell> tempGrid = new List<Cell>(gridComponents);

        tempGrid.RemoveAll(c => c.collapsed);

        tempGrid.Sort((a, b) => { return a.tileOptions.Length - b.tileOptions.Length; });

        int arrLength = tempGrid[0].tileOptions.Length;
        int stopIndex = default;

        for (int i = 1; i < tempGrid.Count; i++)
        {
            if (tempGrid[i].tileOptions.Length > arrLength)
            {
                stopIndex = i;
                break;
            }
        }

        if (stopIndex > 0)
        {
            tempGrid.RemoveRange(stopIndex, tempGrid.Count - stopIndex);
        }

        yield return new WaitForSeconds(0.01f);

        CollapseCell(tempGrid);
    }

    void CollapseCell(List<Cell> tempGrid)
    {
        //check empty cells
        if (tempGrid.Count == 0)
        {
            Debug.LogError("tempGrid is empty");
        }

        int randIndex = UnityEngine.Random.Range(0, tempGrid.Count);

        Cell cellToCollapse = tempGrid[randIndex];

        cellToCollapse.collapsed = true;
        Tile selectedTile = cellToCollapse.tileOptions[UnityEngine.Random.Range(0, cellToCollapse.tileOptions.Length)];
        cellToCollapse.tileOptions = new Tile[] { selectedTile };

        Tile foundTile = cellToCollapse.tileOptions[0];
        Instantiate(foundTile, cellToCollapse.transform.position, Quaternion.identity);

        UpdateGeneration();
    }

    void UpdateGeneration()
    {
        List<Cell> newGenerationCell = new List<Cell>(gridComponents);

        for (int y = 0; y < dimensions; y++)
        {
            for (int x = 0; x < dimensions; x++)
            {
                var index = x + y * dimensions;
                if (gridComponents[index].collapsed)
                {
                    Debug.Log("called");
                    newGenerationCell[index] = gridComponents[index];
                }
                else
                {
                    List<Tile> options = new List<Tile>();
                    foreach (Tile t in tileObjects)
                    {
                        options.Add(t);
                    }

                    //update above
                    if (y > 0)
                    {
                        Cell up = gridComponents[x + (y - 1) * dimensions];
                        List<Tile> validOptions = new List<Tile>();

                        foreach (Tile possibleOptions in up.tileOptions)
                        {
                            var valOption = Array.FindIndex(tileObjects, obj => obj == possibleOptions);
                            var valid = tileObjects[valOption].upNeighbours;

                            validOptions = validOptions.Concat(valid).ToList();
                        }

                        CheckValidity(options, validOptions);
                    }

                    //update right
                    if (x < dimensions - 1)
                    {
                        Cell right = gridComponents[x + 1 + y * dimensions];
                        List<Tile> validOptions = new List<Tile>();

                        foreach (Tile possibleOptions in right.tileOptions)
                        {
                            var valOption = Array.FindIndex(tileObjects, obj => obj == possibleOptions);
                            var valid = tileObjects[valOption].leftNeighbours;

                            validOptions = validOptions.Concat(valid).ToList();
                        }

                        CheckValidity(options, validOptions);
                    }

                    //look down
                    if (y < dimensions - 1)
                    {
                        Cell down = gridComponents[x + (y + 1) * dimensions];
                        List<Tile> validOptions = new List<Tile>();

                        foreach (Tile possibleOptions in down.tileOptions)
                        {
                            var valOption = Array.FindIndex(tileObjects, obj => obj == possibleOptions);
                            var valid = tileObjects[valOption].downNeighbours;

                            validOptions = validOptions.Concat(valid).ToList();
                        }

                        CheckValidity(options, validOptions);
                    }

                    //look left
                    if (x > 0)
                    {
                        Cell left = gridComponents[x - 1 + y * dimensions];
                        List<Tile> validOptions = new List<Tile>();

                        foreach (Tile possibleOptions in left.tileOptions)
                        {
                            var valOption = Array.FindIndex(tileObjects, obj => obj == possibleOptions);
                            var valid = tileObjects[valOption].rightNeighbours;

                            validOptions = validOptions.Concat(valid).ToList();
                        }

                        CheckValidity(options, validOptions);
                    }

                    Tile[] newTileList = new Tile[options.Count];

                    for (int i = 0; i < options.Count; i++)
                    {
                        newTileList[i] = options[i];
                    }

                    newGenerationCell[index].RecreateCell(newTileList);
                }
            }
        }

        gridComponents = newGenerationCell;
        iterations++;

        if(iterations < dimensions * dimensions)
        {
            StartCoroutine(CheckEntropy());
        }

    }

    void CheckValidity(List<Tile> optionList, List<Tile> validOption)
    {
        for (int x = optionList.Count - 1; x >= 0; x--)
        {
            var element = optionList[x];
            if (!validOption.Contains(element))
            {
                optionList.RemoveAt(x);
            }
        }
    }
}
