using UnityEngine;

public class BoomerangMove : IProjectileMove
{
    float maxDist = 6f;
    float returnRadius = 1.6f;

    Vector3 startPos;
    Vector3 dir;
    Transform owner;
    bool returning;

    public BoomerangMove(Vector3 dir, float maxDist)
    {
        dir.y = 0f;
        dir.Normalize();
        this.maxDist = maxDist;
        this.dir = dir;
        owner = Player.playerTransform;
        startPos = owner.position;
    }

    public void Move(ProjectileBase projectile)
    {
        if (owner == null) return;

        if (!returning)
        {
            projectile.transform.position += dir * projectile.speed * Time.deltaTime;

            if (Vector3.Distance(startPos, projectile.transform.position) >= maxDist)
                returning = true;
        }
        else
        {
            Vector3 toOwner = (owner.position - projectile.transform.position).normalized;
            toOwner.y = 0f;
            toOwner.Normalize();
            projectile.transform.position += toOwner * projectile.speed * Time.deltaTime;

            if (Vector3.Distance(owner.position, projectile.transform.position) <= returnRadius)
            {
                projectile.isPenetration = false;
                projectile.DestroyProjectile();
            }
        }
        projectile.transform.Rotate(Vector3.up * 360f * Time.deltaTime);
    }
}
