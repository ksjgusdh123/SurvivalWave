using UnityEngine;
using UnityEngine.UI;

public class CloseButton : MonoBehaviour
{
    [SerializeField] GameObject openButton;
    Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(ClickedButton);
    }
    void ClickedButton()
    {
        openButton.SetActive(true);
        gameObject.SetActive(false);
        UIManager.GetInstance().Show(EUIType.Main);
    }
}
