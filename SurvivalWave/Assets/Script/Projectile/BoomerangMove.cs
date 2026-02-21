using UnityEngine;

public class BoomerangMove : MonoBehaviour, IProjectileMove
{
    public ProjectileType type { get; } = ProjectileType.Boomerang;

    float maxDist = 6f;
    float returnRadius = 0.6f;

    Vector3 startPos;
    Vector3 dir;
    Transform owner;
    bool returning;

    public BoomerangMove(Vector3 dir, float maxDist)
    {
        dir.y = 0f;
        dir.Normalize();
        this.maxDist = maxDist * maxDist;
        this.dir = dir;
        owner = Player.playerTransform;
        startPos = owner.position;
        startPos.y = 1f;
    }

    public void Move(ProjectileBase projectile)
    {
        if (owner == null) return;

        if (!returning)
        {
            projectile.transform.position += dir * projectile.speed * Time.deltaTime;

            Vector2 a = new Vector2(startPos.x, startPos.z);
            Vector2 b = new Vector2(projectile.transform.position.x, projectile.transform.position.z);

            float finalDist = (a - b).sqrMagnitude;

            if (finalDist >= maxDist)
                returning = true;
        }
        else
        {
            Vector3 toOwner = (owner.position - projectile.transform.position).normalized;
            toOwner.y = 0f;
            toOwner.Normalize();
            projectile.transform.position += toOwner * projectile.speed * Time.deltaTime;

            Vector2 a = new Vector2(owner.position.x, owner.position.z);
            Vector2 b = new Vector2(projectile.transform.position.x, projectile.transform.position.z);

            float finalDist = (a - b).sqrMagnitude;

            if (finalDist <= returnRadius)
            {
                projectile.isPenetration = false;
                projectile.DestroyProjectile();
            }
        }
        projectile.transform.Rotate(Vector3.up * 360f * Time.deltaTime);
    }
}
