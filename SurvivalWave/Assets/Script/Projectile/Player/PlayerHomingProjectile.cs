using UnityEngine;

public class PlayerHomingProjectile : ProjectileBase
{
    public void InitHomingTarget(Transform target)
    {
        move = new HomingMove(target);
        SetMovingType(move);
    }

}
