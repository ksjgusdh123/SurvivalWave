using UnityEngine;

public class PlayerRandomProjectile : ProjectileBase
{
    public void InitRandomDirection(Vector3 dir)
    {
        move = new RandomMove(dir, 50f, gameObject.transform.position);
        SetMovingType(move);
    }


}
