using System.Collections.Generic;
using YamlDotNet.Serialization;

public class CardAttribute
{
    public enum Type {
        MagicAttack,
        Attack,
        Heal,
        MagicDefense,
        Defense
    }

    [YamlMember(Alias = "quality")]
    public Type Quality
    {
        get;
        set;
    }

    [YamlMember(Alias = "quantity")]
    public int Quantity
    {
        get;
        set;
    }

    public static readonly IDictionary<Type, string> IconMap = new Dictionary<Type, string> {
        { Type.Attack, "icons_1" },
        { Type.MagicAttack, "icons_0" },
        { Type.Heal, "icons_5" },
        { Type.MagicDefense, "icons_4" },
        { Type.Defense, "icons_2" },
    };
}
