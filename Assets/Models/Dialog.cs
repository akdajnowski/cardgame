using System.Collections.Generic;

public class Result
{
    public int[] Hp { get; set; }
    public int[] Crew { get; set; }
    public string Comment { get; set; }
}

public class DialogOptionOutcome
{
    public string Type { get; set; }
    public decimal? Chance { get; set; }
    public Result Loss { get; set; }
    public Result Gain { get; set; }

}
public class DialogOption
{
    public string Label { get; set; }
    public DialogOptionOutcome Outcome { get; set; }

    public override string ToString()
    {
        return Label;
    }
}

public class Dialog
{
    public string Id { get; set; }
    public string Description { get; set; }
    public List<DialogOption> Options { get; set; }

    public override string ToString()
    {
        return Description;
    }
}


public class DialogsRoot
{
    public List<Dialog> Dialogs { get; set; }
}
