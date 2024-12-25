using System.Collections.Generic;
using UnityEngine;

public class FirstRoomManager : MonoBehaviour
{
    public List<GameObject> doors; // List of all doors in the room
    private int enemyCount; // Total enemies in the room
    private bool roomLocked = false; // Room lock state

    void Start()
    {
        // Count enemies in the room
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        roomLocked = true;
        LockRoom(); // Initially lock the room
    }

    public void EnemyDefeated()
    {
        enemyCount--;

        Debug.Log("Enemies Remaining: " + enemyCount);

        if (enemyCount <= 0 && roomLocked)
        {
            UnlockRoom(); // Unlock the room when all enemies are defeated
        }
    }

    private void LockRoom()
    {
        roomLocked = true;

        // Activate all doors to lock the room
        foreach (GameObject door in doors)
        {
            if (door != null)
            {
                door.SetActive(true);
            }
        }

        Debug.Log("Room Locked: Doors Activated");
    }

    private void UnlockRoom()
    {
        roomLocked = false;

        // Deactivate or destroy all doors to unlock the room
        foreach (GameObject door in doors)
        {
            if (door != null)
            {
                door.SetActive(false); // Deactivate the door
                // Or destroy the door instead:
                // Destroy(door);
            }
        }

        Debug.Log("Room Unlocked: Doors Deactivated or Destroyed");
    }

    // The OpenRoom function seems redundant in your current logic.
    // You're already unlocking the room when all enemies are defeated.
    // If you have a specific reason to open the room without defeating enemies,
    // explain that scenario.
}