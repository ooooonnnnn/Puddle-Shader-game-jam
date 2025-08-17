using TMPro;
using UnityEngine;
using System;
using System.Linq;

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
        {
            Debug.LogWarning("DebugLogShower is not set up correctly.");
            return;
        }

        string previousText = shower.text.text;
        string newText = string.Concat(message, "\n", previousText);
        newText = newText.Length > 400 ? newText.Substring(0, 400) : newText; // Limit to 400 characters
        shower.text.text = newText;
    }
}