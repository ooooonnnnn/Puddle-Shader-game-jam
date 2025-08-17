using TMPro;
using UnityEngine;

public class ShowPlayerPosition : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    private TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }


    void Update()
    {
        text.text = $"Player Position: {playerTransform.position.x:F2}, {playerTransform.position.y:F2}";
    }
}
