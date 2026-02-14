using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class HomingMove : IProjectileMove
{
    Transform homingTarget;
    float currentSpeed = 0f;
    public HomingMove(Transform target)
    {
        homingTarget = target;
    }
    public void Move(ProjectileBase projectile)
    {
        if (homingTarget == null)
        {
            projectile.DestroyProjectile();
            return;
        }
        currentSpeed += 3f * Time.deltaTime;
        currentSpeed = Mathf.Min(currentSpeed, projectile.speed);

        Vector3 dir = (homingTarget.position - projectile.transform.position).normalized;
        projectile.transform.position += dir * currentSpeed * Time.deltaTime;
        projectile.transform.Rotate(Vector3.forward * 360f * Time.deltaTime);
    }

}
