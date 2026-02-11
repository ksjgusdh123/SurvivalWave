using UnityEngine;

public class Player : MonoBehaviour
{
    PlayerAnimation anim;
    void Start()
    {
        anim = GetComponent<PlayerAnimation>();
    }

    void Update()
    {
        anim.UpdateSpeed(Time.deltaTime);
    }


}
