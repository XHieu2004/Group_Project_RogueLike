public class Debuff : StatusEffect
{
    public float DamageReduction { get; private set; }
    public float SlowAmount { get; private set; }
    public float HealthDrain { get; private set; }
    public float EnergyDrain { get; private set; }
    public float ArmorReduction { get; private set; }
    public bool IsPoisoned { get; private set; }

    public Debuff(string name, float duration, float damageReduction, float slowAmount, float healthDrain, float energyDrain, float armorReduction, bool isPoisoned)
        : base(name, duration, StatusEffectType.Debuff)
    {
        DamageReduction = damageReduction;
        SlowAmount = slowAmount;
        HealthDrain = healthDrain;
        EnergyDrain = energyDrain;
        ArmorReduction = armorReduction;
        IsPoisoned = isPoisoned;
    }

    public override void ApplyEffect(Character character)
    {
        character.Damage -= DamageReduction;
        character.Speed -= SlowAmount;
        character.Health -= HealthDrain;
        character.Energy -= EnergyDrain;
        character.Armor -= ArmorReduction;
        if (IsPoisoned) character.IsPoisoned = true;
    }

    public override void RemoveEffect(Character character)
    {
        character.Damage += DamageReduction;
        character.Speed += SlowAmount;
        character.Health += HealthDrain;
        character.Energy += EnergyDrain;
        character.Armor += ArmorReduction;
        if (IsPoisoned) character.IsPoisoned = false;
    }
}
