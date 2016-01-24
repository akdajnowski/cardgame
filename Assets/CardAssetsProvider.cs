using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class CardAssetsProvider
{
    public Transform CardAttrPrefab { get; private set; }
    public List<Sprite> Icons { get; private set; }

    public CardAssetsProvider()
    {
        Debug.Log("CardAssetsProvider");
        Icons = AssetDatabase.LoadAllAssetRepresentationsAtPath("Assets/icons.png").OfType<Sprite>().ToList();
        CardAttrPrefab = (Transform)AssetDatabase.LoadAssetAtPath("Assets/Card Description Icon.prefab", typeof(Transform));
    }

    public Sprite GetSpriteForAttribute(CardAttribute.Type type)
    {
        return Icons.First(x => x.name == CardAttribute.IconMap[type]);
    }

    public Sprite GetSpriteFromPath(string path)
    {
        return Icons.First(x => x.name == path);
    }
}
