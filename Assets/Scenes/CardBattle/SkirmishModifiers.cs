using System.Collections.Generic;
using System.Linq;

public class SkirmishModifiers
{
    public List<StatModifier> Modifiers { get; set; }

    public SkirmishModifiers ()
    {
        Modifiers = new List<StatModifier> ();
    }

    public int CalculateDamageTaken (ModifierTarget player, List<CardAttribute.Type> attacker, List<CardAttribute.Type> defender)
    {
        var physDealtDamage = (attacker.Count (q => q == CardAttribute.Type.Attack) - defender.Count (q => q == CardAttribute.Type.Defense)).NonNegative ();
        var magDealtDamage = (attacker.Count (q => q == CardAttribute.Type.MagicAttack) - defender.Count (q => q == CardAttribute.Type.MagicDefense)).NonNegative ();
        var heal = defender.Count (q => q == CardAttribute.Type.Heal);

        var damageTakenOverall = ResolveDamageWithMods (player, physDealtDamage, magDealtDamage) - heal;
        return damageTakenOverall;
    }

    private int ResolveDamageWithMods (ModifierTarget player, int physDealtDamageTaken, int magDealtDamageTaken)
    {
        
        int physicalDamageTotal = (physDealtDamageTaken +
                                  GetModifierValue (player, ModifierType.PhysicalDamage) -
                                  GetModifierValue (player, ModifierType.PhysicalDefence)).NonNegative ();
        int magicalDamageTaken = (magDealtDamageTaken +
                                 GetModifierValue (player, ModifierType.MagicalDamage) -
                                 GetModifierValue (player, ModifierType.MagicalDefence)).NonNegative ();
        int heal = GetModifierValue (player, ModifierType.Heal);
        int unblockableDamage = GetModifierValue (player, ModifierType.Damage);
        var damageTakenOverall = (physicalDamageTotal + magicalDamageTaken + unblockableDamage) - heal;
        return damageTakenOverall;
    }

    private int GetModifierValue (ModifierTarget player, ModifierType modifierType)
    {
        ModifierTarget[] correctTargets = { player, ModifierTarget.All };
        return Modifiers.Where (
            i => correctTargets.Contains (i.Target) &&
            i.Modifier == modifierType).Sum (i => i.Value);
    }
}
