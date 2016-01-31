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
        Icons.Add (Resources.Load<Sprite> ("mjornir"));
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
}
