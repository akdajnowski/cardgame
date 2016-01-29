public static class IntExt
{
    public static int NonNegative(this int i)
    {
        return i <= 0 ? 0 : i;
    }
}
