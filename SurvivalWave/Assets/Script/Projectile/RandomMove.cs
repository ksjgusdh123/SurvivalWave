using UnityEngine;

public class RandomMove : IProjectileMove
{
    Vector3 direction;

    public RandomMove(Vector3 dir)
    {
        direction = dir;
    }

    public void Move(ProjectileBase projectile)
    {
        projectile.transform.position += direction * projectile.speed * Time.deltaTime;
    }
}
