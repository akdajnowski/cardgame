using System;

//because fu thats why!
public static class FP
{
    public static Func<int> Inc(int start = 0)
    {
        var i = start;
        return  () => i++;
    }
}

