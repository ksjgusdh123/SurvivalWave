using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class MonsterBase : MonoBehaviour, IPoolEvent, ITickUpdate, IShadowCast
{
    public Vector3 Position => transform.position;
    public UpdatePolicy Policy => UpdatePolicy.Check;
    public float TickInterval => 0.2f;
    public int checkStamp { get; set; }
    public bool isOn { get; set; }

    public MonsterType type;
    Renderer[] renderers;
    Transform player;
    MonsterStat stat;
    MonsterAnimation anim;
    MonsterDamaged damagedEventComp;
    NavMeshAgent agent;
    PlayerStat target;
    LayerMask groundMask;

    int playerLayer;
    bool isCollision;

    protected virtual void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>(true);
        anim = GetComponent<MonsterAnimation>();
        agent = GetComponent<NavMeshAgent>();
        stat = GetComponent<MonsterStat>();
        player = Player.playerTransform;
        damagedEventComp = GetComponent<MonsterDamaged>();
        playerLayer = LayerMask.NameToLayer("Player");
        groundMask = LayerMask.GetMask("Ground");
    }

    public void Tick(float delta)
    {
        if (!agent || !agent.isActiveAndEnabled) return;
        if (!agent.isOnNavMesh) return;

        if (stat.hp <= 0f)
        {
            if (!agent.isStopped)
            {
                agent.enabled = false;
            }
            return;
        }

        agent.SetDestination(player.position);
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (!isCollision || null == target) return;

        target.TakeDamage(stat.attack);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != playerLayer) return;
        PlayerStat player = other.GetComponent<PlayerStat>();
        if (null == player) return;
        target = player;
        isCollision = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != playerLayer) return;
        isCollision = false;
        target = null;
    }
    public void DamagedEvent(float damage)
    {
        if (damagedEventComp) StartCoroutine(damagedEventComp.ChangeColor());
        var go = DamageTextPool.GetInstance().PopObject(DamageTextEnum.DamageText);
        go.GetComponent<DamageText>().SpawnDamageText(transform.position, damage);
    }
    protected virtual void EndDieAnimation()
    {
        Vector3 spawnPos = transform.position;
        PickExp(spawnPos);
        PickRandomItem(spawnPos);
        MonsterPool.GetInstance().ReturnObject(gameObject, type);
    }

    void PickExp(Vector3 spawnPos)
    {
        spawnPos.y += 1f;

        GameObject go = ItemPool.GetInstance().PopObject(ItemType.Exp);
        go.transform.position = spawnPos;
        Exp exp = go.GetComponent<Exp>();
        Renderer renderer = go.GetComponent<Renderer>();

        GetRandomExp(exp, renderer);
    }

    void GetRandomExp(Exp exp, Renderer renderer)
    {
        float rand = Random.Range(0f, 10f);

        if (rand < 8f)
        {
            renderer.material.color = Color.green;
            exp.amount = 1f;
        }
        else if (rand < 9.5f)
        {
            renderer.material.color = Color.blue;
            exp.amount = 10f;
        }
        else
        {
            renderer.material.color = Color.red;
            exp.amount = 30f;
        }
    }

    void PickRandomItem(Vector3 spawnPos)
    {
        float rand = Random.Range(0f, 10f);

        //if (rand < 9f) return;


        GameObject go;
        if (rand < 5f)
        {
            go = ItemPool.GetInstance().PopObject(ItemType.HpPotion);
        }
        else
        {
            go = ItemPool.GetInstance().PopObject(ItemType.Magnet);
        }
        Vector3 finalPos = Utility.FixPositionOnGround(spawnPos, groundMask);
        finalPos.y += 0.5f;
        go.transform.position = finalPos;
    }

    public void OnSpawnPool()
    {
        stat.InitStat();
        anim.NotifyIsDeath(false);
    }
    public void OnReturnPool()
    {
    }

    public void SetNearShadow(bool on)
    {
        if (on == isOn) return;
        isOn = on;

        var mode = on ? ShadowCastingMode.On : ShadowCastingMode.Off;
        for (int i = 0; i < renderers.Length; ++i)
        {
            if (renderers[i] == null) continue;
            renderers[i].shadowCastingMode = mode;
        }
    }
}
