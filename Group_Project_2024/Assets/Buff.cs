public class Buff : StatusEffect
{
    public float BonusDamage { get; private set; }
    public float SpeedBoost { get; private set; }
    public float HealthBoost { get; private set; }
    public float EnergyBoost { get; private set; }
    public float ArmorBoost { get; private set; }
    public bool PoisonImmunity { get; private set; }

    public Buff(string name, float duration, float bonusDamage, float speedBoost, float healthBoost, float energyBoost, float armorBoost, bool poisonImmunity)
        : base(name, duration, StatusEffectType.Buff)
    {
        BonusDamage = bonusDamage;
        SpeedBoost = speedBoost;
        HealthBoost = healthBoost;
        EnergyBoost = energyBoost;
        ArmorBoost = armorBoost;
        PoisonImmunity = poisonImmunity;
    }

    public override void ApplyEffect(Character character)
    {
        character.Damage += BonusDamage;
        character.Speed += SpeedBoost;
        character.Health += HealthBoost;
        character.Energy += EnergyBoost;
        character.Armor += ArmorBoost;
        character.IsPoisonImmune = PoisonImmunity;
    }

    public override void RemoveEffect(Character character)
    {
        character.Damage -= BonusDamage;
        character.Speed -= SpeedBoost;
        character.Health -= HealthBoost;
        character.Energy -= EnergyBoost;
        character.Armor -= ArmorBoost;
        if (PoisonImmunity) character.IsPoisonImmune = false;
    }
}
