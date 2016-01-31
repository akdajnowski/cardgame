using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public delegate void OnStateChangeHandler ();
public class GameStateStore
{
    private static GameStateStore _instance;

    public OverworldState OverworldState { get; set; }

    public CardDeck CardDeck { get; set; }

    public List<CardDescriptor> CardInformation;

    public static GameStateStore Instance {
        get {
            if (_instance == null) {
                Debug.Log ("KURWIX games tore");
                _instance = new GameStateStore {
                    OverworldState = new OverworldState (),
                    CardDeck = new CardDeck ()
                };
                Debug.Log ("KURWIX games tore after");
            }
            return _instance;
        }
    }

    public OnStateChangeHandler OnStateCHange;

    public Scenes ReturnScene { get; internal set; }

    public void AdvanceState (Scenes state)
    {
        this.ReturnScene = state;
        Debug.Log ("Going to " + state);

        SceneManager.LoadScene ((int)state);

        if (OnStateCHange != null) {
            OnStateCHange ();
        }
    }

    private GameStateStore ()
    {
        ReturnScene = Scenes.Intro;
    }

    public void DialogResolution (int hp, int crew, bool negative)
    {
        //throw new NotImplementedException();
    }

    public List<CardDescriptor> GetRandomCards (int number, CardDescriptor.CardRarity rarity)
    {
        var cards = new List<CardDescriptor> ();
        var rnd = new System.Random ();
        for (int i = 0; i < number; i++) {
            var cardsPool = CardInformation.Where (c => c.Rarity == rarity).ToList ();
            var idx = rnd.Next (0, cardsPool.Count () - 1);
            cards.Add (cardsPool [idx]);
        }
        return cards;
    }
}
