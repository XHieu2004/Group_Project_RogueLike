using UnityEngine;
using UnityEngine.UI;

public class SkillCooldownUI : MonoBehaviour
{
    public Slider cooldownSlider; 
    public float cooldownTime = 5f; 
    private float cooldownRemaining = 0f; 

    void Update()
    {
        if (cooldownRemaining > 0)
        {
            cooldownRemaining -= Time.deltaTime;
            cooldownSlider.value = cooldownRemaining / cooldownTime; 
        }
         if (Input.GetKeyDown(KeyCode.Q)) 
    {
        ActivateSkill();
    }
    }

    public void ActivateSkill()
    {
        if (cooldownRemaining <= 0)
        {
            Debug.Log("Skill activated!");
            UseSkill();
            StartCooldown();
        }
        else
        {
            Debug.Log("Skill is on cooldown!");
        }
    }

    private void UseSkill()
    {
    }

    private void StartCooldown()
    {
        cooldownRemaining = cooldownTime;
        cooldownSlider.value = 1; 
    }
}

