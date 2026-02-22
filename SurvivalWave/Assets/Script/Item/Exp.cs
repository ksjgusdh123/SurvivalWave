using UnityEngine;

public class Exp : MonoBehaviour, IPickupable
{
    public ItemType type { get; } = ItemType.Exp;
    public float amount { get; set; }

    void Start()
    {

    }

    void Update()
    {

    }

    public void OnGain(GameObject player)
    {   
        player.GetComponent<PlayerStat>().GainExp(amount);
        ItemPool.GetInstance().ReturnObject(gameObject, type);
    }
}
