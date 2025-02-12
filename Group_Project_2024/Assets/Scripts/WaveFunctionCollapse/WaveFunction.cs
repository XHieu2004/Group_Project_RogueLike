using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WaveFunctionCollapse : MonoBehaviour
{
    public Tilemap roadTilemap;
    public Tile placeholderTile;
    public Tile[] roadTiles;

    private Dictionary<Vector3Int, Cell> cellMap = new Dictionary<Vector3Int, Cell>();
    private PriorityQueue<Cell> entropyQueue;

    void Start()
    {
        InitializeGrid();
        PerformWFC();
    }

    void InitializeGrid()
    {
        entropyQueue = new PriorityQueue<Cell>();

        List<Vector3Int> placeholderPositions = FindPlaceholderPositions();
        foreach (Vector3Int pos in placeholderPositions)
        {
            GameObject cellObject = new GameObject($"Cell_{pos.x}_{pos.y}");
            cellObject.transform.parent = this.transform;
            Cell newCell = cellObject.AddComponent<Cell>();
            newCell.CreateCell(false, roadTiles);
            cellMap.Add(pos, newCell);
            entropyQueue.Enqueue(newCell);
            roadTilemap.SetTile(pos, null);
        }
    }

    List<Vector3Int> FindPlaceholderPositions()
    {
        List<Vector3Int> placeholderPositions = new List<Vector3Int>();
        BoundsInt bounds = roadTilemap.cellBounds;

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int tilePos = new Vector3Int(x, y, 0);
                TileBase tile = roadTilemap.GetTile(tilePos);
                if (tile != null && tile == placeholderTile)
                {
                    placeholderPositions.Add(tilePos);
                }
            }
        }
        return placeholderPositions;
    }

    void PerformWFC()
    {
        Dictionary<Vector3Int, Tile> tilesToRender = new Dictionary<Vector3Int, Tile>();

        while (entropyQueue.Count > 0)
        {
            Cell cellToCollapse = GetLowestEntropyCell();
            if (cellToCollapse == null) break;

            Vector3Int cellPosition = cellMap.FirstOrDefault(x => x.Value == cellToCollapse).Key;
            CollapseCell(cellToCollapse, cellPosition, tilesToRender);
            PropagateConstraints(cellPosition);
        }

        foreach (var entry in tilesToRender)
        {
            roadTilemap.SetTile(entry.Key, entry.Value);
        }
    }

    Cell GetLowestEntropyCell()
    {
        if (entropyQueue.Count == 0) return null;
        return entropyQueue.Dequeue();
    }

    void CollapseCell(Cell cell, Vector3Int position, Dictionary<Vector3Int, Tile> tilesToRender)
    {
        if (cell.tileOptions.Count == 0)
        {
            Debug.LogError("Contradiction at: " + position);
            cell.RecreateCell(roadTiles);
            entropyQueue.Enqueue(cell);
            return;
        }

        Tile selectedTile = cell.tileOptions[UnityEngine.Random.Range(0, cell.tileOptions.Count)];
        cell.collapsed = true;
        cell.tileOptions.Clear();
        cell.tileOptions.Add(selectedTile);
        tilesToRender.Add(position, selectedTile);
    }

    void PropagateConstraints(Vector3Int collapsedPosition)
    {
        Queue<Vector3Int> propagationQueue = new Queue<Vector3Int>();
        propagationQueue.Enqueue(collapsedPosition);

        while (propagationQueue.Count > 0)
        {
            Vector3Int currentPos = propagationQueue.Dequeue();
            Cell currentCell = cellMap[currentPos];

            if (currentCell.tileOptions.Count == 0 || !currentCell.collapsed) continue;
            Tile currentTile = currentCell.tileOptions[0];

            Vector2Int[] directions = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };
            List<Tile>[] neighborLists = { currentTile.upNeighbours, currentTile.rightNeighbours, currentTile.downNeighbours, currentTile.leftNeighbours };

            for (int i = 0; i < directions.Length; i++)
            {
                Vector3Int neighborPos = currentPos + (Vector3Int)directions[i];
                if (cellMap.ContainsKey(neighborPos))
                {
                    Cell neighborCell = cellMap[neighborPos];
                    if (neighborCell.collapsed) continue;

                    List<Tile> optionsToRemove = new List<Tile>();
                    foreach (Tile option in neighborCell.tileOptions)
                    {
                        if (!neighborLists[i].Contains(option))
                        {
                            optionsToRemove.Add(option);
                        }
                    }
                    bool changed = false;
                    foreach (Tile toRemove in optionsToRemove)
                    {
                        if (neighborCell.RemoveOption(toRemove))
                        {
                            changed = true;
                        }
                    }
                    if (changed)
                    {
                        if (entropyQueue.Contains(neighborCell))
                        {
                            PriorityQueue<Cell> tempQueue = new PriorityQueue<Cell>();
                            while (entropyQueue.Count > 0)
                            {
                                Cell tempCell = entropyQueue.Dequeue();
                                if (tempCell != neighborCell)
                                {
                                    tempQueue.Enqueue(tempCell);
                                }
                            }
                            while (tempQueue.Count > 0)
                            {
                                entropyQueue.Enqueue(tempQueue.Dequeue());
                            }
                        }
                        entropyQueue.Enqueue(neighborCell);
                        propagationQueue.Enqueue(neighborPos);
                    }
                }
            }
        }
    }
}