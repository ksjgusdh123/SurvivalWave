using UnityEngine;
using UnityEngine.UI;

public class OpenButton : MonoBehaviour
{
    [SerializeField] BoxPanelUI ui;
    Button button;


    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(ClickedButton);
    }
    void ClickedButton()
    {
        ui.StartBoxAnimation();
        gameObject.SetActive(false);
    }
}
