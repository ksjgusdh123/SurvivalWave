using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    [SerializeField] public float speed { get; private set; } = 10f;
    protected IProjectileMove move;

    public void Init(IProjectileMove type, float speed)
    {
        move = type;
        this.speed = speed;
    }

    void Update()
    {
        move?.Move(this);
    }

    public void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
