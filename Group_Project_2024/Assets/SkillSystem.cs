using UnityEngine;

public class SkillSystem : MonoBehaviour
{
    public Character character; // Tham chieu den nhan vat
    public KeyCode activationKey = KeyCode.E; // Phim de kich hoat ky nang
    public Buff skillBuff; // Buff se duoc kich hoat khi su dung ky nang
    public float cooldown = 5f; // Thoi gian hoi chieu
    private float cooldownTimer = 0f; // Bo dem thoi gian hoi chieu

    void Update()
    {
        // Kiem tra thoi gian hoi chieu
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }

        // Kich hoat ky nang khi nhan phim va khong trong thoi gian hoi chieu
        if (Input.GetKeyDown(activationKey) && cooldownTimer <= 0)
        {
            ActivateSkill();
            cooldownTimer = cooldown; // Reset thoi gian hoi chieu
        }
    }

    void ActivateSkill()
    {
        // Ap dung buff cho nhan vat
        character.AddStatusEffect(skillBuff);
        Debug.Log($"{character.name} used skill: {skillBuff.Name}");
    }
}
