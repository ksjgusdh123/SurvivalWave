using UnityEngine;

public class UIBase : MonoBehaviour
{
    public EUIType type { get; set; }

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}
