using UnityEngine;

public class GameController : MonoBehaviour
{
    public Character player; // Nhan vat Player
    public Character enemy; // Nhan vat Enemy

    void Start()
    {
        // Tao buff danh cho ky nang cua Player
        Buff playerSkillBuff = new Buff(
            "Player Skill Buff", 
            10f, // Thoi gian hieu luc
            10f, // Tang damage
            5f,  // Tang toc do di chuyen
            20f, // Tang mau
            15f, // Tang nang luong
            5f,  // Tang giap
            true // Mien nhiem doc
        );

        // Gan he thong ky nang cho Player
        SkillSystem playerSkillSystem = player.gameObject.AddComponent<SkillSystem>();
        playerSkillSystem.character = player;
        playerSkillSystem.skillBuff = playerSkillBuff;
        playerSkillSystem.cooldown = 5f; // Hồi chiêu 5 giây

        // Tao buff danh cho ky nang Enemy
        Buff enemySkillBuff = new Buff(
            "Enemy Skill Buff", 
            5f,  // Thoi gian hieu luc
            5f,  // Giam damage
            2f,  // Giam toc do
            -10f, // Hut mau
            -10f, // Hut nang luong
            -5f,  // Giam giap
            false // Khong mien nhiem doc
        );

        // Gan he thong ky nang cho Enemy
        EnemySkillSystem enemySkillSystem = enemy.gameObject.AddComponent<EnemySkillSystem>();
        enemySkillSystem.enemyCharacter = enemy;
        enemySkillSystem.enemySkillBuff = enemySkillBuff;
        enemySkillSystem.activationInterval = 10f; // Kích hoạt mỗi 10 giây

        Debug.Log("GameController initialized with Player and Enemy skill systems.");
    }
}
