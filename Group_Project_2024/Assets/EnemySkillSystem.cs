using UnityEngine;

public class EnemySkillSystem : MonoBehaviour
{
    public Character enemyCharacter;
    public Buff enemySkillBuff;
    public float activationInterval = 10f; // Khoang thoi gian giua cac lan kich hoat
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        // Tu dong kich hoat ky nang sau moi khoang thoi gian
        if (timer >= activationInterval)
        {
            ActivateSkill();
            timer = 0f;
        }
    }

    void ActivateSkill()
    {
        enemyCharacter.AddStatusEffect(enemySkillBuff);
        Debug.Log($"{enemyCharacter.name} activated skill: {enemySkillBuff.Name}");
    }
}
