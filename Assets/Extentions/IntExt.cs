public static class IntExt
{
    public static int NonNegative(this int i)
    {
        return i <= 0 ? 0 : i;
    }

    public static double NonNegative(this double i)
    {
        return i <= 0 ? 0 : i;
    }

    public static float NonNegative(this float i)
    {
        return i <= 0 ? 0 : i;
    }

    public static float Restrict(this float i, float min, float max)
    {
        if (i <= min) return min;
        if (i >= max) return max;
        return i;
    }

}
