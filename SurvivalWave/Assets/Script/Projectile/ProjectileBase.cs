using UnityEngine;

public class ProjectileBase : MonoBehaviour, IPoolEvent
{
    [SerializeField] public float speed { get; private set; } = 10f;

    TrailRenderer trailRenderer;
    public Transform muzzle { get; private set; }
    protected IProjectileMove move;

    float finalDamag;
    public bool isPenetration { get; set; }

    void Awake()
    {
        muzzle = transform.Find("MuzzlePos");
        trailRenderer = GetComponent<TrailRenderer>();
    }

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

    public void ReturnProjectile()
    {
        if (!isPenetration)
        {
            ProjectilePool.GetInstance().ReturnObject(gameObject, move.type);
            //Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (LayerMask.NameToLayer("Monster") != other.gameObject.layer) return;

        Stat stat = other.GetComponent<Stat>();
        if (null == stat || !stat.TakeDamage(finalDamag)) return;
        other.GetComponent<MonsterBase>().DamagedEvent();

        ReturnProjectile();
    }

    public void OnSpawnPool()
    {
    }
    public void OnReturnPool()
    {
        if (null != trailRenderer)
        {
            trailRenderer.Clear();
        }
    }
}
