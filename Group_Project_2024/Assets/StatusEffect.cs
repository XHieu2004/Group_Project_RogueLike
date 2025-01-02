using System;

public enum StatusEffectType
{
    Buff, // Tang kha nang
    Debuff // Giam kha nang
}

public class StatusEffect
{
    public string Name { get; private set; }
    public float Duration { get; set; }
    public StatusEffectType EffectType { get; private set; }

    public StatusEffect(string name, float duration, StatusEffectType effectType)
    {
        Name = name;
        Duration = duration;
        EffectType = effectType;
    }

    public virtual void ApplyEffect(Character character)
    {
        // Duoc override trong cac class con de ap dung hieu ung cu the
    }

    public virtual void RemoveEffect(Character character)
    {
        // Duoc override de go bo hieu ung cu the
    }
}
