using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public List<GameObject> doors; // List of all doors in the room
    private int enemyCount; // Total enemies in the room
    private bool roomLocked = false; // Room lock state



    void Start()
    {
        // Count enemies in the room
        enemyCount = GameObject.FindGameObjectsWithTag("Enemyroom2").Length; // Assuming enemies are children of this GameObject
        UnlockRoom();
    
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !roomLocked)
        {
            if (enemyCount > 0)
            {
                LockRoom(); // Lock the room if there are enemies
            }
        }
    }

    public void EnemyDefeated()
    {
        enemyCount--;

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
    }
}
