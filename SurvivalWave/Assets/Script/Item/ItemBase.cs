using UnityEngine;

public class ItemBase : MonoBehaviour, IPickupable, ITickUpdate
{
    public Vector3 Position => transform.position;
    public UpdatePolicy Policy => UpdatePolicy.Check;
    public virtual float TickInterval => 0f;
    public int checkStamp { get; set; }
    public virtual ItemType type { get; } = ItemType.Max;

    public virtual void OnGain(GameObject player)
    {
    }

    public virtual void Tick(float delta)
    {
    }
}
