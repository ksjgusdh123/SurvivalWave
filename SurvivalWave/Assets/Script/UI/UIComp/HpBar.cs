using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    Slider bar;
    PlayerStat stat;

    float maxHp;
    void Start()
    {
        bar = GetComponent<Slider>();
        stat = Player.playerTransform.gameObject.GetComponent<PlayerStat>();
        maxHp = stat.maxHp;
        stat.ChangeHp -= ChangeHp;
        stat.ChangeHp += ChangeHp;
    }
    public void ChangeHp()
    {
        float ratio = stat.hp / maxHp;
        bar.value = ratio;
    }
}
