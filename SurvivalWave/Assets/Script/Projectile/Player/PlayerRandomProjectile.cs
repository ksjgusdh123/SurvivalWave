using UnityEngine;

public class PlayerRandomProjectile : ProjectileBase
{
    public void InitRandomDirection(Vector3 dir)
    {
        move = new RandomMove(dir);
        SetMovingType(move);
    }


}
