using UnityEngine;
using UnityEditor; // Make sure this is here

public class RandomTrapSpawner : MonoBehaviour
{
    public GameObject trapPrefab;
    public int numberOfTrapsToSpawn = 5;
    public Bounds spawnArea;

    void Start()
    {
        SpawnTraps();
    }

    void SpawnTraps()
    {
        for (int i = 0; i < numberOfTrapsToSpawn; i++)
        {
            float randomX = Random.Range(spawnArea.min.x, spawnArea.max.x);
            float randomY = Random.Range(spawnArea.min.y, spawnArea.max.y);
            Vector3 spawnPosition = new Vector3(randomX, randomY, 0f);
            Instantiate(trapPrefab, spawnPosition, Quaternion.identity);
        }
    }

    // This draws the Gizmo in the Scene view when the object is selected
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(spawnArea.center, spawnArea.size);

        // Allow editing the center using a handle
        EditorGUI.BeginChangeCheck();
        Vector3 newCenter = Handles.DoPositionHandle(spawnArea.center, Quaternion.identity);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(this, "Move Spawn Area Center");
            spawnArea.center = newCenter;
        }

        // Allow editing the size using scale handles
        EditorGUI.BeginChangeCheck();
        Vector3 newSize = Handles.DoScaleHandle(spawnArea.size, spawnArea.center, Quaternion.identity, HandleUtility.GetHandleSize(spawnArea.center));
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(this, "Scale Spawn Area Size");
            spawnArea.size = newSize;
        }
    }
}