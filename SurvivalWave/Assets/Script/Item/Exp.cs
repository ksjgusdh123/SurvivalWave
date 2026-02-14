using UnityEngine;

public class Exp : MonoBehaviour, IPickupable
{
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
        Destroy(gameObject);
    }
}
