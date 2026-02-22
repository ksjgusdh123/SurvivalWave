using UnityEngine;

public interface IPickupable 
{
    ItemType type { get; }
    void OnGain(GameObject player);
}
