using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class CardAssetsProvider
{
    public List<Sprite> Icons { get; private set; }

    public CardAssetsProvider ()
    {
        Icons = Resources.LoadAll ("icons").OfType<Sprite> ().ToList ();
        Icons.AddRange (LoadSprites ("mjornir", "arrow", "arrows", "sword_diag", "two_swords_diag"));
        Debug.Log (Icons.Count.ToString ());
    }

    public Sprite GetSpriteForAttribute (CardAttribute.Type type)
    {
        return Icons.First (x => x.name == CardAttribute.IconMap [type]);
    }

    public Sprite GetSpriteFromPath (string path)
    {
        return Icons.First (x => x.name == path);
    }

    private IEnumerable<Sprite> LoadSprites (params string[] names)
    {
        return names.Select (name => Resources.Load<Sprite> (name));
    }
}
