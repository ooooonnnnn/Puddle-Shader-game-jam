using UnityEngine;
using System.Collections;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private MonoBehaviour activatable;
    [SerializeField] private Transform movingPart;
    [SerializeField] private float pressedHeight;
    [SerializeField] private float releasedHeight;
    [SerializeField] private float moveTime;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        (activatable as IActivatable).Activate();
        StopAllCoroutines();
        StartCoroutine(MoveIn(pressedHeight));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        StopAllCoroutines();
        StartCoroutine(MoveIn(releasedHeight));
    }

    private IEnumerator MoveIn(float targetHeight)
    {
        float startTime = Time.time;
        float startPosition = movingPart.localPosition.y;
        while (Time.time - startTime < moveTime)
        {
            float t = (Time.time - startTime) / moveTime;
            float newY = Mathf.Lerp(startPosition, targetHeight, t);
            movingPart.localPosition = new Vector3(movingPart.localPosition.x, newY, movingPart.localPosition.z);
            yield return null;
        }

        movingPart.localPosition = new Vector3(movingPart.localPosition.x, targetHeight, movingPart.localPosition.z);
    }
}
