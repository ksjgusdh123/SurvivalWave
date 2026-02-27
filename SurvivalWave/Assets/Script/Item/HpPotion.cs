using UnityEngine;

public class HpPotion : ItemBase
{
    public override ItemType type { get; } = ItemType.HpPotion;
    public float amount { get; set; } = 20f;

    public override void OnGain(GameObject player)
    {
        player.GetComponent<PlayerStat>().Heal(amount);
        SoundManager.GetInstance().PlaySFX(SFXType.HpPotion);
        ItemPool.GetInstance().ReturnObject(gameObject, type);
    }
}
