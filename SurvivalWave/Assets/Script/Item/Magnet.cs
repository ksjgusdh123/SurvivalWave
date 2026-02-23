using UnityEngine;

public class Magnet : ItemBase
{
    public override ItemType type { get; } = ItemType.Magnet;

    public override void OnGain(GameObject player)
    {
        player.GetComponent<Player>().GetMagnetItem();
        ItemPool.GetInstance().ReturnObject(gameObject, type);
    }

}
