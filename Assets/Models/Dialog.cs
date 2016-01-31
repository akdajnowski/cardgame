using System.Collections.Generic;

public class Dialog
{
    public string Id { get; set; }
    public string Description { get; set; }
    public bool? ToBattle { get; set; }
    public bool? RemoveOnResolve { get; set; }

    public override string ToString()
    {
        return Description;
    }
}

public class DialogsRoot
{
    public List<Dialog> Dialogs { get; set; }
}
