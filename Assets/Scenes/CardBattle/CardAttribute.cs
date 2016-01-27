using System.Collections.Generic;

public class CardAttribute
{
    public enum Type {
        MagicAttack,
        Attack,
        Heal,
        MagicDefense,
        Defense
    }

    public Type Quality;

    public int Quantity;

    public static readonly IDictionary<Type, string> IconMap = new Dictionary<Type, string> {
        { Type.Attack, "icons_1" },
        { Type.MagicAttack, "icons_0" },
        { Type.Heal, "icons_5" },
        { Type.MagicDefense, "icons_4" },
        { Type.Defense, "icons_2" },
    };
}
