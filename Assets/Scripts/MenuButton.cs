using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string sceneName;
    [SerializeField] private bool quitGame = false;
    [SerializeField] private Button thisButton;
    [SerializeField] private RectTransform imageTrasform;
    [SerializeField] private float amplitude;
    [SerializeField] private float frequency;
    private float startTime;
    private bool animate = false;
    private Vector3 initialImageScale;

    private void OnValidate()
    {
        thisButton = GetComponent<Button>();
    }
    private void Awake()
    {
        thisButton.onClick.AddListener(HandleButtonClicked);
        initialImageScale = imageTrasform.localScale;
    }

    private void HandleButtonClicked()
    {
        if (quitGame)
        {
            Application.Quit();
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
    }

    private void Update()
    {
        if (animate)
        {
            imageTrasform.localScale = initialImageScale * (1 + amplitude * Mathf.Sin(frequency * (Time.time - startTime)));
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        startTime = Time.time;
        animate = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animate = false;
        imageTrasform.localScale = initialImageScale; // Reset scale when pointer exits 
    }
}
