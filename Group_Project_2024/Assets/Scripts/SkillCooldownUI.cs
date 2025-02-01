using UnityEngine;
using UnityEngine.UI;

public class SkillCooldownUI : MonoBehaviour
{
    public Slider cooldownSlider; // Tham chiếu đến thanh trượt hiển thị thời gian hồi chiêu
    public float cooldownTime = 5f; // Thời gian hồi chiêu của kỹ năng
    private float cooldownRemaining = 0f; // Thời gian còn lại

    void Update()
    {
        if (cooldownRemaining > 0)
        {
            cooldownRemaining -= Time.deltaTime;
            cooldownSlider.value = cooldownRemaining / cooldownTime; // Cập nhật trạng thái thanh trượt
        }
         if (Input.GetKeyDown(KeyCode.Q)) // Phím Q để kích hoạt skill
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
        // Logic thực hiện kỹ năng ở đây
    }

    private void StartCooldown()
    {
        cooldownRemaining = cooldownTime;
        cooldownSlider.value = 1; // Đặt thanh trượt về đầy đủ
    }
}

