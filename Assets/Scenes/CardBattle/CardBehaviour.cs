﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Adic;

public class CardBehaviour : MonoBehaviour, IPointerClickHandler
{
    [Inject]
    public CardAssetsProvider cardAssetsProvider;

    public CardDescriptor Card { get; private set; }

    public int Index { get; private set; }

    public HandBehaviour hand;
    private Transform _cardDescription;
    private HoverOver hoverOver;
    public Transform CardAttrPrefab;

    public void Init (CardDescriptor card, int index)
    {
        this.Inject ();
        hoverOver = GetComponent<HoverOver> ();
        hoverOver.Reset (hoverEnabled: true);
        Card = card;
        Index = index;

        _cardDescription = transform.FindChild ("Card Description");

        transform.FindChild ("Text").GetComponent<Text> ().text = Card.Name;

        transform.FindChild ("Image").GetComponent<Image> ().sprite = cardAssetsProvider.GetSpriteFromPath (Card.CardImage);

        RemoveOldIcons ();
        AddCardAttributeIcons (Card.CardAttributes);
    }


    // Update is called once per frame
    void Update ()
    {
    }

    private void AddCardAttributeIcons (List<CardAttribute> attributes)
    {
        attributes.SelectMany (s => Enumerable.Range (1, s.Quantity).Select (i => s.Quality)).ToList ().ForEach (i => {
            var attributeIcon = Instantiate (CardAttrPrefab) as Transform;
            attributeIcon.GetComponent<Image> ().sprite = cardAssetsProvider.GetSpriteForAttribute (i);
            attributeIcon.SetParent (_cardDescription, false);
        });
    }

    private void RemoveOldIcons ()
    {
        for (int i = _cardDescription.childCount - 1; i >= 0; i--) {
            DestroyImmediate (_cardDescription.GetChild (i).gameObject);
        }
    }

    public void OnPointerClick (PointerEventData eventData)
    {
        if (hand != null && transform.parent.name == "Hand") {
            Debug.Log ("Kurwa kliklem w karte");
            hand.PlayCard (Index);
        }
    }
}
