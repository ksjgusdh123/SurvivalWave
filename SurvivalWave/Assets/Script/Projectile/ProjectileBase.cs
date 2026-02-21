using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    [SerializeField] public float speed { get; private set; } = 10f;
    float finalDamag;
    protected IProjectileMove move;
    public Transform muzzle { get; private set; }

    public bool isPenetration { get; set; }

    void Awake()
    {
        muzzle = transform.Find("MuzzlePos");    
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

    public void DestroyProjectile()
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

        DestroyProjectile();
    }
}
