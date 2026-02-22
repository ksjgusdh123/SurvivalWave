using UnityEngine;

public class HpPotion : MonoBehaviour, IPickupable
{
    public ItemType type { get; } = ItemType.HpPotion;
    public float amount { get; set; } = 20f;

    public void OnGain(GameObject player)
    {
        player.GetComponent<PlayerStat>().Heal(amount);
        ItemPool.GetInstance().ReturnObject(gameObject, type);
    }
}
