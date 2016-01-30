using Adic;
using System;
using System.Collections.Generic;
using System.Linq;

public class CardRepository
{
    [Inject]
    public GameStateStore gameStore;

    public static readonly int HandSize = 4;
    private readonly static Stack<CardDescriptor> _deck = new Stack<CardDescriptor>();
    public readonly static List<CardDescriptor> Hand = new List<CardDescriptor>();
    //public static bool IsPlayerTurn { get; set; }

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

    public static readonly List<CardDescriptor> Cards = GameStateStore.Instance.CardInformation;
}
