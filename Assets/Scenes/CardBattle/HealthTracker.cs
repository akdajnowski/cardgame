﻿public class HealthTracker
{
	public int PlayerHealth { get; set; }
	public int OpponentHealth { get; set; }

	public HealthTracker()
	{
		PlayerHealth = 10;
		OpponentHealth = 8;
	}

    public bool SomeoneDied()
    {
        return OpponentHealth <= 0 || PlayerHealth <= 0;
    }
}
