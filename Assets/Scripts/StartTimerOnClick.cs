using UnityEngine;
using UnityEngine.UI;

public class StartTimerOnClick : MonoBehaviour
{
    [SerializeField, HideInInspector] private Button button;
    private void OnValidate()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(GameTimer.StartTimer);
    }
}
