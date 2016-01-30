using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class CardDeck
{
    public List<CardDescriptor> Cards { get; set; }

    public CardDeck()
    {
        Cards = new List<CardDescriptor>();
    }

    public void AddCard(CardDescriptor card)
    {
        Cards.Add(card);
    }

    

}
