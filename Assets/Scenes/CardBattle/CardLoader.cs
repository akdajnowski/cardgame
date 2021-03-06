﻿using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;
using Adic;

public class CardLoader : MonoBehaviour
{
    public TextAsset cardAsset;
    public List<CardDescriptor> cards;

    [Inject]
    public GameStateStore gameStore;

    void Start ()
    {
        this.Inject ();
        Debug.Log ("Card Loader behaviour injected");
        gameStore.CardInformation = gameStore.CardInformation ?? LoadCards ();
    }

    private List<CardDescriptor> LoadCards ()
    {
        var deserializer = new Deserializer (namingConvention: new CamelCaseNamingConvention ());
        var cardsString = new StringReader (cardAsset.text);
        List<CardDescriptor> cards = deserializer.Deserialize<List<CardDescriptor>> (cardsString);

        //PrintDebug(cards);

        return cards;
    }

    private void PrintDebug (List<CardDescriptor> cards)
    {
        foreach (var card in cards) {
            Debug.Log ("name = " + card.Name);
            Debug.Log ("description = " + card.Description);
            Debug.Log ("image = " + card.CardImage);
            Debug.Log ("rarity = " + card.Rarity);
            foreach (var attr in card.CardAttributes) {
                Debug.Log ("attr = " + attr.Quality.ToString () + " " + attr.Quantity.ToString ());
            }
            Debug.Log ("------------------------------------");
        }
    }
}
