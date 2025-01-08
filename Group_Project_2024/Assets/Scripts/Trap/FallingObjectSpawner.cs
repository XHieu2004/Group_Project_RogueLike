using UnityEngine;

public class FallingObjectSpawner : MonoBehaviour
{
    public GameObject fallingObjectPrefab;
    public Vector2 spawnOffset = new Vector2(0, 2f);
    public float delayBeforeFall = 0.5f;

    private Animator animator;
    private bool triggered = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component missing on the trigger!");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;
            if (animator != null)
            {
                animator.SetTrigger("Activate");
            }
            Invoke("SpawnFallingObject", delayBeforeFall);
            Destroy(gameObject, delayBeforeFall + 1f);
        }
    }

    void SpawnFallingObject()
    {
        if (fallingObjectPrefab != null)
        {
            Vector2 spawnPosition2D = (Vector2)transform.position + spawnOffset;
            Vector3 spawnPosition = spawnPosition2D;

            GameObject fallingObjectInstance = Instantiate(fallingObjectPrefab, spawnPosition, Quaternion.identity);

            FallingObject fallingObjectScript = fallingObjectInstance.GetComponent<FallingObject>();
            if (fallingObjectScript != null)
            {
                fallingObjectScript.spawningTrigger = gameObject;
            }
            else
            {
                Debug.LogError("FallingObject component not found on the spawned prefab!");
            }
        }
        else
        {
            Debug.LogError("Falling object prefab not assigned!");
        }
    }
}