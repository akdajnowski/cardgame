using Adic;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class CardRepository
{
    [Inject]
    public GameStateStore gameStore;

    public static readonly int HandSize = 4;
    private readonly static Stack<CardDescriptor> _deck = new Stack<CardDescriptor> ();
    public readonly static List<CardDescriptor> Hand = new List<CardDescriptor> ();

    public static CardDescriptor DrawCard (int index = 0)
    {
        // to refactor
        if (_deck.Count == 0) {
            FillDeck ();
            ShuffleDeck();
        }

        var card = _deck.Pop ();
        Hand.Insert (index, card);
        return card;
    }

    private static void FillDeck (int cardNo = 10)
    {
        int rare = (int)Math.Round (0.0 * cardNo);
        int uncommon = (int)Math.Round (0.3 * cardNo);
        int common = cardNo - uncommon - rare;

        var cards = GameStateStore.Instance.GetRandomCards (rare, CardDescriptor.CardRarity.Rare);
        cards.AddRange (GameStateStore.Instance.GetRandomCards (uncommon, CardDescriptor.CardRarity.Uncommon));
        cards.AddRange (GameStateStore.Instance.GetRandomCards (common, CardDescriptor.CardRarity.Common));
        
        foreach (var card in cards)
        {
            _deck.Push(card);
        }
    }

    public static void GetRareCardsFromOpponent(int number = 1)
    {
        var cards = OpponentCardRepository.GetRareCards(number);
        foreach (var card in cards)
        {
            _deck.Push(card);
        }
        ShuffleDeck();
    }

    private static void ShuffleDeck()
    {
        var cards = _deck.ToList();
        System.Random r = new System.Random();
        cards = cards.OrderBy(i => r.Next()).ToList();

        _deck.Clear();
        foreach (var card in cards)
        {
            _deck.Push(card);
        }
    }
}
