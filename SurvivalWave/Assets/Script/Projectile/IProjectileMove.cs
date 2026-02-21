using UnityEngine;

public interface IProjectileMove
{
    ProjectileType type { get; }
    void Move(ProjectileBase projectile);
}
