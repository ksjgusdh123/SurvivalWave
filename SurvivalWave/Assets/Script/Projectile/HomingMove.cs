using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class HomingMove : IProjectileMove
{
    Transform homingTarget;

    public HomingMove(Transform target)
    {
        homingTarget = target;
    }
    public void Move(ProjectileBase projectile)
    {
        if (homingTarget == null) return;

        Vector3 dir = (homingTarget.position - projectile.transform.position).normalized;
        projectile.transform.position += dir * projectile.speed * Time.deltaTime;
    }
}
