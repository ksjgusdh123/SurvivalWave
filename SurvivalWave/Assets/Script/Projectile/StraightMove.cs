using UnityEngine;

public class StraightMove : IProjectileMove
{
    Vector3 direction;
    float maxDistanceMagnitude;
    Vector3 startPosition;

    public StraightMove(Vector3 dir, float distance, Vector3 startPos)
    {
        direction = dir;
        maxDistanceMagnitude = distance * distance;
        startPosition = startPos;
    }

    public void Move(ProjectileBase projectile)
    {
        projectile.transform.position += direction * projectile.speed * Time.deltaTime;

        float movedDistance = (projectile.transform.position - startPosition).sqrMagnitude;

        if (movedDistance >= maxDistanceMagnitude)
        {
            projectile.DestroyProjectile();
        }
    }
}
