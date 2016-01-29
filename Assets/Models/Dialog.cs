using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum OutcomeType
{
    Flee,
    Fight,
    Negotiations
}

public class Result
{
    public int[] Hp { get; set; }
    public int[] Crew { get; set; }
}

public class DialogOptionOutcome
{
    public OutcomeType Type { get; set; }
    public decimal Chance { get; set; }
    public Result Loss { get; set; }
    public Result Gain { get; set; }

}
public class DialogOption
{
    public string Label { get; set; }
    public DialogOptionOutcome Outcome { get; set; }

}

public class Dialog
{
    public string Description { get; set; }
    public List<DialogOption> Options { get; set; }
}
