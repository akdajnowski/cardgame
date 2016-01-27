using Adic;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandBehaviour : MonoBehaviour
{
    [Inject]
    public GameTracker GameTracker;

    public List<GameObject> cards;
    public GameObject opponentCard;

    public GameObject cardPrefab;
    public GameObject play;
    public GameObject winningIndicator;
    private enum ActivePlayer { Player, Opponent };
    public int indexOfCardPlayed;
    private ActivePlayer firstPlayer;
    private CardReducer cardReducer;
    

    // Use this for initialization
    void Start()
    {
        this.Inject();
        cards = new List<GameObject>();
        cardReducer = new CardReducer();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DealCard()
    {
        if (IsHandEmpty())
        {
            Debug.Log("Dealing Kurwa leci");
            while (ShouldDealMoreCards())
            {
                var card = (GameObject)Instantiate(cardPrefab, Vector3.zero, Quaternion.identity);
                // get a card
                var cardBehaviour = card.GetComponent<CardBehaviour>();
                
                cardBehaviour.hand = this;
                cards.Add(card);
                card.transform.SetParent(transform);
                cardBehaviour.Init(CardRepository.DrawCard(cards.Count - 1), cards.Count -1);
                var cardRectTransform = (RectTransform)card.transform;
                cardRectTransform.localPosition = Vector3.zero;

                cardRectTransform.Translate(75 + (cards.Count - 1) * 150, 190, 0);

            }

            DrawOponentCard();

            ChoosePlayer();
        }
    }

    private void DrawOponentCard()
    {
        opponentCard = (GameObject)Instantiate(cardPrefab, Vector3.zero, Quaternion.identity);
        var cardBehaviour = opponentCard.GetComponent<CardBehaviour>();
        cardBehaviour.Init(OpponentCardRepository.DrawCard(), cards.Count);
        opponentCard.transform.SetParent(play.transform);
        var cardRectTransform = (RectTransform)opponentCard.transform;
        cardRectTransform.localPosition = Vector3.zero;
        cardRectTransform.Translate(520, 110, 0);
        SetOpponentCardVisibility(false);
    }

    private void ChoosePlayer()
    {
        firstPlayer = new System.Random().Next(2) == 1 ? ActivePlayer.Opponent : ActivePlayer.Player;
        NewTurn();
    }

    private void NewTurn()
    {
        switch (firstPlayer)
        {
            case ActivePlayer.Player:
                GameTracker.IsPlayerTurn = true;
                break;
            case ActivePlayer.Opponent:
                OpponentTurn();
                break;
            default:
                throw new NotImplementedException();
        }
    }

    private void OpponentTurn()
    {
        var cardBehaviour = opponentCard.GetComponent<CardBehaviour>();
        cardBehaviour.Init(OpponentCardRepository.DrawCard(), cards.Count);
        SetOpponentCardVisibility(true);
        HandleOpponentAction();
    }

    private bool IsHandEmpty()
    {
        return cards.Count == 0;
    }

    private bool ShouldDealMoreCards()
    {
        return cards.Count < CardRepository.HandSize;
    }

    private void DrawCard(int index)
    {
        SetOpponentCardVisibility(false);
        Transform card = GetCardWithIndex(index);
        var cardBehaviour = card.GetComponent<CardBehaviour>();
        Debug.Log("Kurwa znalazlem index " + cardBehaviour.Index + ", przy wejsciowym " + index);
        cardBehaviour.Init(CardRepository.DrawCard(index), index);
        cardBehaviour.hand = this;

        card.transform.SetParent(transform);
        var cardRectTransform = (RectTransform)card.transform;
        cardRectTransform.localPosition = Vector3.zero;

        cardRectTransform.Translate(75 + (index) * 150, 190, 0);
    }

    private void SetOpponentCardVisibility(bool visible)
    {
        opponentCard.GetComponent<CanvasGroup>().alpha = visible ? 1 : 0;
    }

    private Transform GetCardWithIndex(int index)
    {
        foreach (Transform card in transform)
        {
            var cardBehaviour = card.GetComponent<CardBehaviour>();
            if (cardBehaviour.Index == index)
            {
                return card;
            }
        }

        throw new KeyNotFoundException("No card with index " + index);
    }

    public void PlayCard(int index)
    {
        if (GameTracker.IsPlayerTurn)
        {
            GameTracker.IsPlayerTurn = false;
            cards[index].transform.SetParent(play.transform);
            cards[index].transform.localPosition = Vector3.zero;
            cards[index].transform.Translate(75, 110, 0);
            indexOfCardPlayed = index;
            HandlePlayerAction();
        }
    }

    private void HandlePlayerAction()
    {
        Debug.Log("Handling players action, first player is: " + firstPlayer);
        if (firstPlayer == ActivePlayer.Opponent)
        {
            Debug.Log("Resolving");
            ResolveCards();
        }
        else
        {
            Debug.Log("Opponent turn");
            OpponentTurn();
        }
    }

    private void HandleOpponentAction()
    {
        Debug.Log("Handling oipponent action, first player is: " + firstPlayer);
        if (firstPlayer == ActivePlayer.Opponent)
        {
            Debug.Log("Player turn");
            GameTracker.IsPlayerTurn = true;
        }
        else
        {
            Debug.Log("Resolving");
            ResolveCards();
        }
    }

    private void ResolveCards()
    {
        // if game not finished
        Debug.Log("napierdalamy tutaj");
        StartCoroutine(Continue());
    }

    private IEnumerator Continue()
    {
        yield return new WaitForSeconds(1.75f);
        var result = cardReducer.ResolveCard(PlayerCard().GetComponent<CardBehaviour>().Card, opponentCard.GetComponent<CardBehaviour>().Card);
        HealthTracker.PlayerHealth -= result.PlayerDamageTaken;
        HealthTracker.OpponentHealth -= result.OpponentDamageTaken;
        Debug.LogFormat("Player received {0} dmg, remaining {1} hp", result.PlayerDamageTaken, HealthTracker.PlayerHealth);
        Debug.LogFormat("Opponent received {0} dmg, remaining {1} hp", result.OpponentDamageTaken, HealthTracker.OpponentHealth);
        if (!WinningConditionCheck())
        {
            DrawPlayerCard();
            NewTurn();
        }
        else
        {
            winningIndicator.GetComponent<WinningIndicator>().ShowWinner(HealthTracker.PlayerHealth > 0 ? "Player" : "Opponent");
        }
    }

    private bool WinningConditionCheck()
    {
        return HealthTracker.SomeoneDied();
    }

    public void DrawPlayerCard()
    {
        Debug.Log("Ciagniemy karte");
        PlayerCard().transform.SetParent(transform);
        PlayerCard().transform.localPosition = Vector3.zero;
        DrawCard(indexOfCardPlayed);
    }

    private GameObject PlayerCard()
    {
        return cards[indexOfCardPlayed];
    }
}
