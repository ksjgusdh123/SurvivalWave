using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    Slider bar;
    PlayerStat stat;

    void Start()
    {
        bar = GetComponent<Slider>();
        stat = Player.playerTransform.gameObject.GetComponent<PlayerStat>();
        stat.ChangeHp -= ChangeHp;
        stat.ChangeHp += ChangeHp;
    }
    public void ChangeHp()
    {
        float ratio = stat.hp / stat.maxHp;
        bar.value = ratio;
    }
}
