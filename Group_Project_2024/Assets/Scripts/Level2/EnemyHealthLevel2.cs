using System.Collections;
using UnityEngine;

public class EnemyHealthLevel2 : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public Animator anim;
    private bool isDeath;
    public float disappearTime = 0.5f;
    private RoomManagerLevel2 roomManager;

    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        roomManager = GetComponentInParent<RoomManagerLevel2>();
        if (roomManager == null)
        {
            Debug.LogError("RoomManager not found on the parent of " + gameObject.name);
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
        if (roomManager != null)
        {
            roomManager.EnemyDefeated();
        }

        StartCoroutine(Disappear());
    }

    private IEnumerator Disappear()
    {
        yield return new WaitForSeconds(disappearTime);
        Destroy(gameObject);
    }
}