using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    Slider bar;
    PlayerStat stat;

    void Start()
    {
        bar = GetComponent<Slider>();
        stat = Player.playerTransform.gameObject.GetComponent<PlayerStat>();
        stat.ChangeStamina -= ChangeStamina;
        stat.ChangeStamina += ChangeStamina;
    }
    public void ChangeStamina()
    {
        float ratio = stat.stamina / stat.maxStamina;
        bar.value = ratio;
    }
}
