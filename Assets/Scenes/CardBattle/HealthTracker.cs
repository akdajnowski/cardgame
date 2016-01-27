public class HealthTracker
{
    public static int PlayerHealth = 10;
    public static int OpponentHealth = 8;

    public static bool SomeoneDied()
    {
        return OpponentHealth <= 0 || PlayerHealth <= 0;
    }
}
