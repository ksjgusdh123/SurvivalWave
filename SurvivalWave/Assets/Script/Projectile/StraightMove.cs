using UnityEngine;

public class StraightMove : MonoBehaviour, IProjectileMove
{
    public ProjectileType type { get; } = ProjectileType.RandomShot;

    Vector3 direction;
    float maxDistanceMagnitude;
    Vector3 startPosition;

    public void InitMove(Vector3 dir, float distance, Vector3 startPos)
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
