using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Animator anim;
    private bool isDeath;
    public float disappearTime = 0.5f;
    private RoomManager roomManager;  // Reference to the RoomManager.

    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();

        // Find the RoomManager.
        roomManager = GetComponentInParent<RoomManager>();
        if (roomManager == null)
        {
            Debug.LogError("RoomManager not found in the scene!");
        }
    }

    void Update()
    {
        if (isDeath) { return; }
    }

    public void TakeDamage(int damage)
    {
        if (isDeath) { return; }
        currentHealth -= damage;
        anim.SetTrigger("Hit");
        if (currentHealth <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        isDeath = true;
        anim.SetTrigger("Death");

        Collider2D enemyCollider = GetComponent<Collider2D>();
        if (enemyCollider != null)
        {
            enemyCollider.enabled = false;
        }

        // Notify the RoomManager.
        if (roomManager != null)
        {
            roomManager.EnemyDefeated();
        }

        ScoreManager.Instance.AddScore(2); // Assuming you have a ScoreManager.
        StartCoroutine(Disappear());
    }

    private IEnumerator Disappear()
    {
        yield return new WaitForSeconds(disappearTime);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (roomManager != null && !isDeath)
        {
          roomManager.EnemyDefeated();
        }
    }
}