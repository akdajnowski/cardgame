using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[ExecuteInEditMode]
public class CardBehaviour : MonoBehaviour, IPointerClickHandler
{
    public CardDescriptor Card { get; private set; }
    public int Index { get; private set; }
    private List<Sprite> _icons;
    private Transform _cardDescription;
    private Transform _cardAttrPrefab;
    public HandBehaviour hand;

    void Start()
    {     
        //Card = CardRepository.DrawCard();
    }

    public void Init(CardDescriptor card, int index)
    {
        Card = card;
        Index = index;
        _icons = AssetDatabase.LoadAllAssetRepresentationsAtPath("Assets/icons.png").OfType<Sprite>().ToList();
        _cardAttrPrefab = (Transform)AssetDatabase.LoadAssetAtPath("Assets/Card Description Icon.prefab", typeof(Transform));
        _cardDescription = transform.FindChild("Card Description");

        transform.FindChild("Text").GetComponent<Text>().text = Card.Name;
        transform.FindChild("Image").GetComponent<Image>().sprite = GetSpriteFromPath(Card.CardImage);

        RemoveOldIcons();
        AddCardAttributeIcons(Card.CardAttributes);
    }


    // Update is called once per frame
    void Update()
    {
    }

    private Sprite GetSpriteForAttribute(CardAttribute.Type type)
    {
        return _icons.First(x => x.name == CardAttribute.IconMap[type]);
    }

    private Sprite GetSpriteFromPath(string path)
    {
        return _icons.First(x => x.name == path);
    }

    private void AddCardAttributeIcons(List<CardAttribute> attributes)
    {
        attributes.SelectMany(s => Enumerable.Range(1, s.Quantity).Select(i => s.Quality)).ToList().ForEach(i => 
        {
            var attributeIcon = Instantiate(_cardAttrPrefab) as Transform;
            attributeIcon.GetComponent<Image>().sprite = GetSpriteForAttribute(i);
            attributeIcon.SetParent(_cardDescription, false);
        });      
    }

    private void RemoveOldIcons()
    {
        for (int i = _cardDescription.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(_cardDescription.GetChild(i).gameObject);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (hand != null && transform.parent.name == "Hand")
        {
            Debug.Log("Kurwa kliklem w karte");
            hand.PlayCard(Index);
        }
    }
}
