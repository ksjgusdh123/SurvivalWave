using UnityEngine;

public class ProjectileBase : MonoBehaviour, IPoolEvent, ITickUpdate
{
    public Vector3 Position => transform.position;
    public UpdatePolicy Policy => UpdatePolicy.Always;
    public float TickInterval => 0f;
    public int checkStamp { get; set; }
    [SerializeField] public float speed { get; private set; } = 10f;

    TrailRenderer trailRenderer;
    public Transform muzzle { get; private set; }
    protected IProjectileMove move;

    float finalDamage;
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
        finalDamage = dmg;
    }

    public void Tick(float delta)
    {
        move?.Move(this);
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
        if (null == stat || stat.hp <= 0f || !stat.TakeDamage(finalDamage)) return;
        other.GetComponent<MonsterBase>().DamagedEvent(finalDamage);

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
