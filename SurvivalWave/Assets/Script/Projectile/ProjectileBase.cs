using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    [SerializeField] public float speed { get; private set; } = 10f;
    float finalDamag;
    protected IProjectileMove move;

    public bool isPenetration { get; set; }

    public void Init(IProjectileMove type, float speed, float dmg)
    {
        move = type;
        this.speed = speed;
        finalDamag = dmg;
    }

    void Update()
    {
        move?.Move(this);
    }

    public void DestroyProjectile()
    {
        if(!isPenetration) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (LayerMask.NameToLayer("Monster") != other.gameObject.layer) return;

        Stat stat = other.GetComponent<Stat>();
        if (null == stat || !stat.TakeDamage(finalDamag)) return;

        DestroyProjectile();
    }
}
