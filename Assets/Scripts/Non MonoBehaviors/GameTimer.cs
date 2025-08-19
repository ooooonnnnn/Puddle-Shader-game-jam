using UnityEngine;

public static class GameTimer
{
    public static float totalGameTime => Time.time - startTime;
    private static float startTime;
    public static void StartTimer()
    {
        startTime = Time.time;
    }
}
