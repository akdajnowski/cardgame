using System.Collections.Generic;
using YamlDotNet.Serialization;

public class CardDescriptor
{
    public List<CardAttribute> CardAttributes { get; set; }
    public string Name { get; set; }
    [YamlMember(Alias = "card-image")]
    public string CardImage { get; set; }
}
