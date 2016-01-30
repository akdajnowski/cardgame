using System.Collections.Generic;
using YamlDotNet.Serialization;

public class CardDescriptor
{
    public enum CardRarity
    {
        Common,
        Uncommon,
        Rare
    }

    public List<CardAttribute> CardAttributes { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string CardImage { get; set; }
    public CardRarity Rarity { get; set; }
}
