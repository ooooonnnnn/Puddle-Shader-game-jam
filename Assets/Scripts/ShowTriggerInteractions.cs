using TMPro;
using UnityEngine;
using System;

public class ShowTriggerInteractions : MonoBehaviour
{
    public TMP_Text text;

    private void Awake()
    {
        TriggerInteractionDaddy.shower = this;
    }
}

public static class TriggerInteractionDaddy
{
    public static ShowTriggerInteractions shower;

    public static void NotifyInteraction(PipeBehavior pipeEntrance)
    {
        if (shower == null || shower.text == null)
            throw new Exception("ShowTriggerInteractions is not set up correctly.");

        string previousText = shower.text.text;
        shower.text.text = string.Concat($"{pipeEntrance.gameObject.name} triggered at time {Time.time}", "\n", previousText);
    }
}