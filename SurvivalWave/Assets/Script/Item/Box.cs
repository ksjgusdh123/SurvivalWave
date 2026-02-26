using UnityEngine;

public class Box : ItemBase
{
    public override ItemType type { get; } = ItemType.Box;

    public override void OnGain(GameObject player)
    {
        UIManager.GetInstance().Show(EUIType.BoxPanel);
        ItemPool.GetInstance().ReturnObject(gameObject, type);
    }
}
