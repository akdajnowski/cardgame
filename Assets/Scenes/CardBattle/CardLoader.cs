using UnityEngine;
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

    void Start()
    {
        this.Inject();
        gameStore.CardInformation = gameStore.CardInformation ?? LoadCards();
    }

    private List<CardDescriptor> LoadCards()
    {
        var deserializer = new Deserializer(namingConvention: new CamelCaseNamingConvention());
        var cardsString = new StringReader(cardAsset.text);
        List<CardDescriptor> cards = deserializer.Deserialize<List<CardDescriptor>>(cardsString);

        //foreach (var card in cards)
        //{
        //    Debug.Log("name = " + card.Name);
        //    Debug.Log("image = " + card.CardImage);
        //    foreach (var attr in card.CardAttributes)
        //    {
        //        Debug.Log("attr = " + attr.Quality.ToString() + " " + attr.Quantity.ToString());
        //    }
        //    Debug.Log("------------------------------------");
        //}

        return cards;
    }
}
