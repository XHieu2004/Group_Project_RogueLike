using UnityEngine;

public class Character : MonoBehaviour
{
    public string CharacterName;
    
    public string Name
    {
        get => CharacterName;
        set => CharacterName = value;
    }
    public float Damage;
    public float Speed;
    public float Health;
    public float Energy;
    public float Armor;
    public bool IsPoisonImmune;
    public bool IsPoisoned; 



    //Them hieu ung trang thai buff hoac debuff vao nhan vat
    public void AddStatusEffect(StatusEffect effect)
    {
        //Ap dung hieu ung cho nhan vat
        effect.ApplyEffect(this);
        
        //Log thong bao nhan hieu ung
        Debug.Log($"{CharacterName} received effect: {effect.Name}");
    }
}
