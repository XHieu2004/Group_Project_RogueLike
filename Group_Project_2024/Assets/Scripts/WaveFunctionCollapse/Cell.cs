using System; // Import System for IComparable
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps; // Import Tilemaps

public class Cell : MonoBehaviour, IComparable<Cell> // Implement IComparable<Cell>
{
    public bool collapsed;
    public List<Tile> tileOptions;

    public void CreateCell(bool collapseState, Tile[] tiles)
    {
        collapsed = collapseState;
        tileOptions = tiles.ToList(); // Convert to List
    }

    public void RecreateCell(Tile[] tiles)
    {
        collapsed = false; // Reset collapsed state on recreation
        tileOptions = tiles.ToList();
    }

    public int GetEntropy()
    {
        return tileOptions.Count;
    }

    // Implementation of IComparable<Cell>
    public int CompareTo(Cell other)
    {
        if (other == null) return 1; // Standard null check

        // Compare based on entropy (lower entropy comes first).
        return this.GetEntropy().CompareTo(other.GetEntropy());
    }

    // Helper function to remove a tile from the options.  Returns bool.
    public bool RemoveOption(Tile tile)
    {
        return tileOptions.Remove(tile);
    }

    // Helper function to check if a tile is still an option
    public bool HasOption(Tile tile)
    {
        return tileOptions.Contains(tile);
    }
}