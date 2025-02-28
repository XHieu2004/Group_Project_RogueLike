using UnityEngine;

public class PlayerBuffSystem : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public PlayerMovement playerMovement;
    public ProjectTile projectile;

    private float buffDuration = 10f; // : The duration of the buff effect is extended

    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        playerMovement = GetComponent<PlayerMovement>();
        projectile = GetComponent<ProjectTile>();
        if (playerHealth == null || playerMovement == null || projectile == null)
        {
            Debug.LogError("Please assign PlayerHealth, PlayerMovement, and ProjectTile references in the inspector.");
        }
    }

    public void ApplyHealthBuff(int extraHealth)
    {
        playerHealth.maxHealth += extraHealth;
        playerHealth.currentHealth += extraHealth;
        playerHealth.UpdateHealthBar();
        Debug.Log("Health buff applied: " + extraHealth);
        Invoke("RemoveHealthBuff", buffDuration);
    }

    private void RemoveHealthBuff()
    {
        playerHealth.maxHealth -= 20; // Reduce the value of the old buff
        if (playerHealth.currentHealth > playerHealth.maxHealth)
        {
            playerHealth.currentHealth = playerHealth.maxHealth;
        }
        playerHealth.UpdateHealthBar();
        Debug.Log("Health buff removed.");
    }

    public void ApplySpeedBuff(float speedMultiplier)
    {
        playerMovement.speed *= speedMultiplier;
        Debug.Log("Speed buff applied: x" + speedMultiplier);
        Invoke("RemoveSpeedBuff", buffDuration);
    }

    private void RemoveSpeedBuff()
    {
        playerMovement.speed /= 1.25f; // Reduce the value of the old buff
        Debug.Log("Speed buff removed.");
    }

    public void ApplyDamageBuff(int extraDamage)
    {
        projectile.damage += extraDamage;
        Debug.Log("Damage buff applied: +" + extraDamage);
        Invoke("RemoveDamageBuff", buffDuration);
    }

    private void RemoveDamageBuff()
    {
        projectile.damage -= 10; // Reduce the value of the old buff
        Debug.Log("Damage buff removed.");
    }

    public void ApplyAllBuffs()
    {
        ApplyHealthBuff(20);
        ApplySpeedBuff(1.25f);
        ApplyDamageBuff(10);
        Debug.Log("All buffs applied.");
    }

    // Call the ApplyBuff function for testing
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ApplyAllBuffs();
        }
    }
}
