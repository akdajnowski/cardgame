using UnityEngine;

public class DialogEngine
{
    private System.Random rnd;

    public DialogEngine()
    {
        rnd = new System.Random();
    }
    public void Handle(Dialog dialog, DialogOption opt)
    {
        //we default to true, if Chance is not defined
        var success = opt.Outcome.Chance.HasValue ? Resolve(opt.Outcome.Chance.Value) : true;

        var resulution = Resolve(success ? opt.Outcome.Gain : opt.Outcome.Loss, success);
        Debug.Log(resulution);
    }

    private Resolution Resolve(Result result, bool success)
    {
        var hp = result.Hp == null ? 0: Resolve(result.Hp);
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
}
