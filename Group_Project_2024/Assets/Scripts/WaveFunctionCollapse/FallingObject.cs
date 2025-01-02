using UnityEngine;

public class FallingObject : MonoBehaviour
{
    public int damageAmount = 20;
    [HideInInspector] public GameObject spawningTrigger;
    private float reachDistance = 0.5f;
    private Vector2 initialTriggerPosition; // Store the initial position

    void Start()
    {
        if (spawningTrigger != null)
        {
            initialTriggerPosition = spawningTrigger.transform.position;
        }
        else
        {
            Debug.LogWarning("Spawning trigger is not assigned to the falling object!");
        }
    }

    void Update()
    {
        // Use the stored initial position for distance calculation
        Vector2 objectPosition = transform.position;
        float distanceToTarget = Vector2.Distance(objectPosition, initialTriggerPosition);

        if (distanceToTarget <= reachDistance)
        {
            Destroy(gameObject);
            Debug.Log(gameObject.name + " reached initial position of " + (spawningTrigger != null ? spawningTrigger.name : "a destroyed trigger") + " and was destroyed.");
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collider.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
                Debug.Log("Player hit by falling object! Damage: " + damageAmount);
            }
            else
            {
                Debug.LogWarning("Player hit, but no Health component found!");
            }
        }
        Destroy(gameObject);
    }
}