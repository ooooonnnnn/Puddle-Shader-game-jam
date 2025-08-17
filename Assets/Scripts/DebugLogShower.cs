using TMPro;
using UnityEngine;
using System;

public class DebugLogShower : MonoBehaviour
{
    public TMP_Text text;

    private void Awake()
    {
        MyDebugLogManager.shower = this;
    }
}

public static class MyDebugLogManager
{
    public static DebugLogShower shower;

    public static void LogMessage(string message)
    {
        if (shower == null || shower.text == null)
            throw new Exception("DebugLogShower is not set up correctly.");

        string previousText = shower.text.text;
        shower.text.text = string.Concat(message, "\n", previousText);
    }
}