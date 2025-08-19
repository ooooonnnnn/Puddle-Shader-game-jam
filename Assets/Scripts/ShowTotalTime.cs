using TMPro;
using UnityEngine;

public class ShowTotalTime : MonoBehaviour
{
    [SerializeField, HideInInspector] private TMP_Text text;

    private void OnValidate()
    {
        text = GetComponent<TMP_Text>();
    }

    private void Awake()
    {
        text.text = $"Total Time: \n{GameTimer.totalGameTime:F2}";
    }
}
