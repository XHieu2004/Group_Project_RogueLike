using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public List<GameObject> doors; 
    private int enemyCount; 
    private bool roomLocked = false; 
    public string enemyTag; 

    void Start()
    {
        enemyCount = GameObject.FindGameObjectsWithTag(enemyTag).Length;
        UnlockRoom();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !roomLocked)
        {
            if (enemyCount > 0)
            {
                LockRoom(); 
            }
        }
    }

    public void EnemyDefeated()
    {
        enemyCount--;

        if (enemyCount <= 0 && roomLocked)
        {
            UnlockRoom(); 
        }
    }

    private void LockRoom()
    {
        roomLocked = true;

        // Kích hoạt cửa
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
        
        foreach (GameObject door in doors)
        {
            if (door != null)
            {
                door.SetActive(false);
            }
        }
    }
}