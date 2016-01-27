using System;
using System.Collections.Generic;
using System.Linq;

public class OpponentCardRepository
{
    public static readonly int HandSize = 4;
    private readonly static Stack<CardDescriptor> _deck = new Stack<CardDescriptor>();
    public readonly static List<CardDescriptor> Hand = new List<CardDescriptor>();
    public static bool IsPlayerTurn { get; set; }

    public static CardDescriptor DrawCard(int index = 0)
    {
        // to refactor
        if(_deck.Count == 0)
        {
            FillDeck();
        }

        var card = _deck.Pop();
        Hand.Insert(index, card);
        return card;
    }

    private static void FillDeck(int cardNo = 10)
    {
        var rnd = new Random();
        for (int i = 0; i < cardNo; i++)
        {
            var idx = rnd.Next(0, Cards.Count - 1);
            _deck.Push(Cards[idx]);
        };
    }

    public readonly static List<CardDescriptor> Cards = new List<CardDescriptor> {
        new CardDescriptor
        {
            Name = "Cannon ball salve",
            CardImage = "icons_9",
            CardAttributes = new List<CardAttribute>
            {
                new CardAttribute { Quality = CardAttribute.Type.Attack, Quantity = 2 }
            }
        },
        new CardDescriptor
        {
            Name = "Welsh faggot",
            CardImage = "icons_9",
            CardAttributes = new List<CardAttribute>()
        },
        new CardDescriptor
        {
            Name = "Boarding party",
            CardImage = "icons_9",
            CardAttributes = new List<CardAttribute>
            {
                new CardAttribute { Quality = CardAttribute.Type.Attack, Quantity = 1 },
                new CardAttribute { Quality = CardAttribute.Type.Defense, Quantity = 1 }
            }
        }
    };
}
