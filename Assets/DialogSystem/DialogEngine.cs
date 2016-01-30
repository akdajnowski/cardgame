using System;
using Adic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEditor;

public class DialogEngine
{
    private System.Random rnd;

    [Inject]
    public GameStateStore store;

    [Inject]
    public GameTracker tracker;

    public DialogEngine()
    {
        rnd = new System.Random();
    }
    public void Handle(Dialog dialog, DialogOption opt)
    {
        //we default to true, if Chance is not defined
        var success = opt.Outcome.Chance.HasValue ? Resolve(opt.Outcome.Chance.Value) : true;

        var resolution = Resolve(success ? opt.Outcome.Gain : opt.Outcome.Loss, success);
        if (!success)
        {
            HandleFailure(opt.Outcome);
        }
        else
        {
            HandleSuccess(opt.Outcome);
        }
    }

    private void HandleSuccess(DialogOptionOutcome outcome)
    {
        switch (outcome.Type)
        {
            case "fight": GoToBattle(outcome.Gain); break;
        }
    }

    private void HandleFailure(DialogOptionOutcome outcome)
    {
        switch (outcome.Type)
        {
            case "flee": GoToBattle(); break;
        }
    }

    private void GoToBattle(Result gain = null)
    {
        //store.DialogResolution(0, 0, false);
        store.ReturnScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene((int)Scenes.CardBattle);
    }

    private Resolution Resolve(Result result, bool success)
    {
        if (result == null) return EmptyResolution;

        var hp = result.Hp == null ? 0 : Resolve(result.Hp);
        var crew = result.Crew == null ? 0 : Resolve(result.Crew);
        return new Resolution
        {
            hp = hp,
            crew = crew,
            negative = !success
        };
    }

    private int Resolve(int[] range)
    {
        return rnd.Next(range[0], range[1]);
    }

    private bool Resolve(decimal chance)
    {
        return rnd.NextDouble() < (double)chance;
    }

    public struct Resolution
    {
        public int hp;
        public int crew;
        public bool negative;
    }

    public static Resolution EmptyResolution = new Resolution();
}
