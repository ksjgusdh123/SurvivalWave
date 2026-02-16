using UnityEngine;

public class HpPotion : MonoBehaviour, IPickupable
{
    public float amount { get; set; } = 20f;

    public void OnGain(GameObject player)
    {
        player.GetComponent<PlayerStat>().Heal(amount);
        Destroy(gameObject);
    }
}
