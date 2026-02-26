using UnityEngine;
using UnityEngine.UI;

public class OpenButton : MonoBehaviour
{
    [SerializeField] BoxPanelUI ui;
    [SerializeField] GameObject closeButton;
    Button button;


    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(ClickedButton);
    }
    void ClickedButton()
    {
        ui.EndBoxPanelUIAnimation();
        closeButton.SetActive(true);
        gameObject.SetActive(false);
    }
}
