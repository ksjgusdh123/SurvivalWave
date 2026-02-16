using UnityEngine;

public class Box : MonoBehaviour, IPickupable
{
    public void OnGain(GameObject player)
    {
        UIManager.GetInstance().Show(EUIType.LevelUp);
        Destroy(gameObject);
    }
}
