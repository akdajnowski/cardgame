using Adic;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HandBehaviour : MonoBehaviour
{
    private const float CardMovingToPlayZoneTime = 0.75f;
    private const int PlayZoneCardHeightMarker = 110;
    private SkirmishModifiers skirmishModifiers;

    [Inject]
    public BattleHealthTracker healthTracker;
    [Inject]
    public GameTracker gameTracker;

    [Inject]
    public GameStateStore store;

    [Inject]
    public CardReducer cardReducer;

    public List<GameObject> cards;
    public GameObject opponentCard;

    public GameObject cardPrefab;
    public GameObject play;
    public Text playerHealth;
    public Text opponentHealth;
    public Text playerHealthChange;
    public Text opponentHealthChange;

    public GameObject winningIndicator;
    public float cardResolutionLag;

    private enum ActivePlayer
    {
        Player,
        Opponent
    }

    public int indexOfCardPlayed;
    private ActivePlayer firstPlayer;
    private static float cardDealSpeed = 0.5f;

    // Use this for initialization
    void Start ()
    {
        this.Inject ();
        skirmishModifiers = new SkirmishModifiers {
            Modifiers = new List<StatModifier> {
                new StatModifier {
                    Modifier = ModifierType.Damage,
                    Target = ModifierTarget.Oponnnent,
                    Value = 2
                }
            }
        };
        ShowPlayerHealth ();
        cards = new List<GameObject> ();
        Debug.Log ("fffff" + store.ReturnScene);
    }

    // Update is called once per frame
    void Update ()
    {

    }

    public void DealCards (GameObject source = null)
    {
        if (!IsHandEmpty ())
            return;

        Debug.Log ("Dealing Kurwa leci");
        while (ShouldDealMoreCards ()) {
            var p = source ? source.transform.position : Vector3.zero;
            var r = source ? source.transform.rotation : Quaternion.identity;

            var card = (GameObject)Instantiate (cardPrefab, p, r);
            card.transform.SetParent (transform);
            cards.Add (card);
            DealCard (card);
        }

        DrawOpponentCard ();

        ChoosePlayer ();
    }

    private void DealCard (GameObject card)
    {
        var cardBehaviour = card.GetComponent<CardBehaviour> ();
        cardBehaviour.hand = this;
        var index = cards.Count - 1;

        cardBehaviour.Init (CardRepository.DrawCard (index), index);
        var cardRectTransform = (RectTransform)card.transform;
        cardRectTransform.localPosition = Vector3.zero;

        TranslateCard (index, cardRectTransform);
    }

    private static void TranslateCard (int index, RectTransform cardRectTransform)
    {
        //by trial and error best fit for Deck element, this makes the DealCards(GameObject source = null) a bit pointless since it will work only for that element, whatev
        cardRectTransform.Translate (-110, 175, 0);

        var rotations = new float[] { 4, 1f, -1f, -4 };
        var yDeltas = new int[] { -40, 0, 0, -40 };
        var xDeltas = new int[] { 20, 5, -5, -20 };
        var cardRotation = Vector3.forward * (rotations [index] * 5);

        cardRectTransform
            .DOLocalMove (new Vector3 (75 + xDeltas [index] + index * 150, 190 + yDeltas [index], 0), cardDealSpeed)
            .JoinIntoSequence (cardRectTransform.DORotate (cardRotation, cardDealSpeed));
    }

    private void DrawOpponentCard ()
    {
        opponentCard = (GameObject)Instantiate (cardPrefab, Vector3.zero, Quaternion.identity);
        var cardBehaviour = opponentCard.GetComponent<CardBehaviour> ();
        cardBehaviour.Init (OpponentCardRepository.DrawCard (), cards.Count);
        opponentCard.transform.SetParent (play.transform);
        var cardRectTransform = (RectTransform)opponentCard.transform;
        cardRectTransform.localPosition = Vector3.zero;
        cardRectTransform.Translate (520, PlayZoneCardHeightMarker, 0);
        SetOpponentCardVisibility (false);
    }

    private void ChoosePlayer ()
    {
        firstPlayer = new System.Random ().Next (2) == 1 ? ActivePlayer.Opponent : ActivePlayer.Player;
        NewTurn ();
    }

    private void NewTurn ()
    {
        switch (firstPlayer) {
        case ActivePlayer.Player:
            gameTracker.IsPlayerTurn = true;
            break;
        case ActivePlayer.Opponent:
            OpponentTurn ();
            break;
        default:
            throw new NotImplementedException ();
        }
    }

    private void OpponentTurn ()
    {
        var cardBehaviour = opponentCard.GetComponent<CardBehaviour> ();
        cardBehaviour.Init (OpponentCardRepository.DrawCard (), cards.Count);
        SetOpponentCardVisibility (true);
        HandleOpponentAction ();
    }

    private bool IsHandEmpty ()
    {
        return cards.Count == 0;
    }

    private bool ShouldDealMoreCards ()
    {
        return cards.Count < CardRepository.HandSize;
    }

    private void DrawCard (int index)
    {
        SetOpponentCardVisibility (false);
        var card = GetCardWithIndex (index);
        var cardBehaviour = card.GetComponent<CardBehaviour> ();
        Debug.Log ("Kurwa znalazlem index " + cardBehaviour.Index + ", przy wejsciowym " + index);
        cardBehaviour.Init (CardRepository.DrawCard (index), index);
        cardBehaviour.hand = this;

        card.transform.SetParent (transform);
        var cardRectTransform = (RectTransform)card.transform;
        cardRectTransform.localPosition = Vector3.zero;
        TranslateCard (index, cardRectTransform);
    }

    private void SetOpponentCardVisibility (bool visible)
    {
        opponentCard.GetComponent<CanvasGroup> ().alpha = visible ? 1 : 0;
    }

    private Transform GetCardWithIndex (int index)
    {
        foreach (Transform card in transform) {
            var cardBehaviour = card.GetComponent<CardBehaviour> ();
            if (cardBehaviour.Index == index) {
                return card;
            }
        }

        throw new KeyNotFoundException ("No card with index " + index);
    }

    public void PlayCard (int index)
    {
        if (gameTracker.IsPlayerTurn) {
            gameTracker.IsPlayerTurn = false;
            indexOfCardPlayed = index;
            StartCoroutine (MoveCardToPlayZone (cards [indexOfCardPlayed]));
            HandlePlayerAction ();
        }
    }

    private IEnumerator MoveCardToPlayZone (GameObject card)
    {
        // we don't want to have the card hover wobbling enabled while the card is floating again
        var hoverOver = card.GetComponent<HoverOver> ();
        hoverOver.Reset (false);
        card.transform.SetParent (play.transform);
        var seq = card.transform
            .DOLocalMove (new Vector3 (90, PlayZoneCardHeightMarker, 0), CardMovingToPlayZoneTime)
            .JoinIntoSequence (card.transform.DOScale (1f, CardMovingToPlayZoneTime));

        yield return seq.WaitForCompletion ();
    }

    private void HandlePlayerAction ()
    {
        Debug.Log ("Handling players action, first player is: " + firstPlayer);
        if (firstPlayer == ActivePlayer.Opponent) {
            Debug.Log ("Resolving");
            ResolveCards ();
        } else {
            Debug.Log ("Opponent turn");
            OpponentTurn ();
        }
    }

    private void HandleOpponentAction ()
    {
        Debug.Log ("Handling oipponent action, first player is: " + firstPlayer);
        if (firstPlayer == ActivePlayer.Opponent) {
            Debug.Log ("Player turn");
            gameTracker.IsPlayerTurn = true;
        } else {
            Debug.Log ("Resolving");
            ResolveCards ();
        }
    }

    private void ResolveCards ()
    {
        // if game not finished
        Debug.Log ("napierdalamy tutaj");
        StartCoroutine (ResolveCard ());
    }

    private IEnumerator ResolveCard ()
    {
        var result = cardReducer.ResolveCard (PlayerCard ().GetComponent<CardBehaviour> ().Card, opponentCard.GetComponent<CardBehaviour> ().Card, skirmishModifiers);
        SetTextToIndicators (playerHealthChange, result.PlayerDamageTaken);
        SetTextToIndicators (opponentHealthChange, result.OpponentDamageTaken);
        /*var seq = 
            playerHealthChange.transform.DOLocalMove (new Vector3 (-292.0f, -200f, 0), 1.0f)
                .JoinIntoSequence
            (playerHealthChange.transform.DOLocalMove (new Vector3 (-292.0f, -150f, 0), 1.0f));

        yield return seq.WaitForCompletion ();*/
        float zToRevertTo = playerHealthChange.transform.position.y;
        var animation = playerHealthChange.transform.DOMove (new Vector3 (106.5f, playerHealth.transform.position.y - 20.0f, 0), 2.0f).JoinIntoSequence (
                            opponentHealthChange.transform.DOMove (new Vector3 (907.9f, playerHealth.transform.position.y - 20.0f, 0), 2.0f));

        yield return animation.WaitForCompletion ();
        Debug.Log (playerHealthChange.transform.position.y);
        Debug.Log (playerHealth.transform.position.y);
        playerHealthChange.transform.position = new Vector3 (106.5f, zToRevertTo, 0);
        opponentHealthChange.transform.position = new Vector3 (907.9f, zToRevertTo, 0);
        playerHealthChange.text = "";
        opponentHealthChange.text = "";

        healthTracker.PlayerHealth -= result.PlayerDamageTaken;
        healthTracker.OpponentHealth -= result.OpponentDamageTaken;

        ShowPlayerHealth ();
        var healthSequence = playerHealth.transform.DOScale (1.3f, 0.75f).JoinIntoSequence (
                                 opponentHealth.transform.DOScale (1.3f, 0.75f));
        yield return healthSequence.WaitForCompletion ();

        yield return playerHealth.transform.DOScale (1.0f, 0.75f).JoinIntoSequence (
            opponentHealth.transform.DOScale (1.0f, 0.75f)).WaitForCompletion ();

        Debug.LogFormat ("Player received {0} dmg, remaining {1} hp", result.PlayerDamageTaken, healthTracker.PlayerHealth);
        Debug.LogFormat ("Opponent received {0} dmg, remaining {1} hp", result.OpponentDamageTaken, healthTracker.OpponentHealth);
        if (!WinningConditionCheck ()) {
            DrawPlayerCard ();
            NewTurn ();
        } else {
            HandleGameEnd ();
        }
    }

    private bool WinningConditionCheck ()
    {
        return healthTracker.SomeoneDied ();
    }

    public void DrawPlayerCard ()
    {
        Debug.Log ("Ciagniemy karte");
        PlayerCard ().transform.SetParent (transform);
        PlayerCard ().transform.localPosition = Vector3.zero;
        DrawCard (indexOfCardPlayed);
    }

    private GameObject PlayerCard ()
    {
        return cards [indexOfCardPlayed];
    }

    private void HandleGameEnd ()
    {
        var playerWon = healthTracker.PlayerHealth > 0;
        var winningGamer = playerWon ? "Player" : "Opponent";
        var messageColor = playerWon ? Color.blue : Color.red;
        winningIndicator.GetComponent<WinningIndicator> ().ShowWinner (winningGamer, messageColor);
        BackToMap ();
    }

    private void BackToMap ()
    {
        StartCoroutine (BackToMapRoutine ());
               
    }

    IEnumerator BackToMapRoutine ()
    {
        yield return new WaitForSeconds (2.0f);
        store.AdvanceState (Scenes.Overworld);
    }

    void ShowPlayerHealth ()
    {
        playerHealth.text = healthTracker.PlayerHealth.ToString () + " ❤";
        playerHealth.color = healthTracker.PlayerHealth > 0 ? Color.green : Color.red;
        opponentHealth.text = healthTracker.OpponentHealth.ToString () + " ❤";
        opponentHealth.color = healthTracker.OpponentHealth > 0 ? Color.green : Color.red;
    }

    private void SetTextToIndicators (Text text, int value)
    {
        text.color = value < 0 ? Color.green : Color.red;
        text.text = (value < 0 ? "+" : "") + (-value) + " ❤";
    }
}
