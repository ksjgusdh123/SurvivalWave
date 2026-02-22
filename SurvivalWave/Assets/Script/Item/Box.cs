using UnityEngine;

public class Box : MonoBehaviour, IPickupable
{
    public ItemType type { get; } = ItemType.Box;

    public void OnGain(GameObject player)
    {
        UIManager.GetInstance().Show(EUIType.LevelUp);
        ItemPool.GetInstance().ReturnObject(gameObject, type);
    }
}
