﻿using System;
using System.Collections.Generic;
using System.Linq;

public class OpponentCardRepository
{
    public static readonly int HandSize = 4;
    private readonly static Stack<CardDescriptor> _deck = new Stack<CardDescriptor> ();
    public readonly static List<CardDescriptor> Hand = new List<CardDescriptor> ();

    public static bool IsPlayerTurn { get; set; }

    public static CardDescriptor DrawCard (int index = 0)
    {
        // to refactor
        if (_deck.Count == 0) {
            FillDeck ();
        }

        var card = _deck.Pop ();
        Hand.Insert (index, card);
        return card;
    }

    private static void FillDeck (int cardNo = 10)
    {
        int rare = (int)Math.Round (0.1 * cardNo);
        int uncommon = (int)Math.Round (0.4 * cardNo);
        int common = cardNo - uncommon - rare;

        var cards = GameStateStore.Instance.GetRandomCards (rare, CardDescriptor.CardRarity.Rare);
        cards.AddRange (GameStateStore.Instance.GetRandomCards (uncommon, CardDescriptor.CardRarity.Uncommon));
        cards.AddRange (GameStateStore.Instance.GetRandomCards (common, CardDescriptor.CardRarity.Common));
        System.Random r = new System.Random ();
        cards = cards.OrderBy (i => r.Next ()).ToList ();

        foreach (var card in cards) {
            _deck.Push (card);
        }
    }
}
