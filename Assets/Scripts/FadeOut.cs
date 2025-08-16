using System.Collections;
using TMPro;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    [SerializeField] private float aliveTime;
    [SerializeField] private float fadeoutDuration;
    [SerializeField] private TMP_Text textComponent;

    private void OnValidate()
    {
        textComponent = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        textComponent.faceColor = Color.black;
        StartCoroutine(WaitAndFadeOut());
    }

    private IEnumerator WaitAndFadeOut()
    {
        yield return new WaitForSeconds(aliveTime);
        float startTime = Time.time;
        while (Time.time < startTime + fadeoutDuration)
        {
            float t = (Time.time - startTime) / fadeoutDuration;
            textComponent.faceColor = Color.Lerp(Color.black, Color.clear, t);
            yield return null;
        }
    }
}
