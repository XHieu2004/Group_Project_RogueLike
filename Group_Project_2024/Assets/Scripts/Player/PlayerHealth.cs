using UnityEngine.UI;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public  Image healthBar;
    public Animator anim;
    private bool isDead = false;
    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
        
    }

    
    void Update()
    {
        UpdateHealthBar();
    }
    public void TakeDamage(int damage)
    {
        if (isDead) return;
        anim.SetTrigger("Hurt");
        currentHealth -= damage;
        UpdateHealthBar(); 

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    public void UpdateHealthBar()
    {
        healthBar.fillAmount = (float)currentHealth / maxHealth;
        Debug.Log("Health bar fill amount: " + healthBar.fillAmount);
    }

    private void Die()
    {
        isDead = true;
        anim.SetTrigger("Death");  
        FindObjectOfType<RetryMenu>().ShowRetryMenu();
        Time.timeScale = 0; 
        
    }
    public void ResetHP(){
        anim.Play(anim.GetLayerName(0) + ".Idle");
        currentHealth = maxHealth;
        isDead = false;
    }
}