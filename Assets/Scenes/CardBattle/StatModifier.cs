using System;

public struct StatModifier
{
    public ModifierType Modifier { get; set; }

    public ModifierTarget Target { get; set; }

    public int Value { get; set; }

    public static StatModifier Of (ModifierType modifier, ModifierTarget target, int value)
    {
        return new StatModifier {
            Modifier = modifier,
            Target = target,
            Value = value
        };
    }
}


