using System;
using UnityEngine;

public static class GameTimer
{
    public static string totalGameTime
    {
        get
        {
            var elapsed = DateTime.UtcNow - _startTime;
            return $"{(int)elapsed.TotalSeconds}.{elapsed.Milliseconds / 10:00}";
        }
    }
    private static DateTime _startTime;
    public static void StartTimer()
    {
        _startTime = DateTime.UtcNow;
    }
}
