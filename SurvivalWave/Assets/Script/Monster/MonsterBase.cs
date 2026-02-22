using UnityEngine;
using UnityEngine.AI;

public class MonsterBase : MonoBehaviour, IPoolEvent
{
    public MonsterType type;
    Transform player;
    MonsterStat stat;
    MonsterDamaged damagedEventComp;
    NavMeshAgent agent;

    bool isCollision;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        stat = GetComponent<MonsterStat>();
        player = Player.playerTransform;
        damagedEventComp = GetComponent<MonsterDamaged>();
    }

    void Update()
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

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player") || isCollision || stat.hp <= 0) return;

        PlayerStat player = other.GetComponent<PlayerStat>();
        if (null == player || !player.TakeDamage(stat.attack)) return;
            
        
        isCollision = true;
    }

    public void DamagedEvent()
    {
        if(damagedEventComp) StartCoroutine(damagedEventComp.ChangeColor());
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

        if (rand < 9f) return;

        spawnPos.x += 5f;

        if(rand < 9.7f)
        {
            GameObject go = ItemPool.GetInstance().PopObject(ItemType.HpPotion);
            go.transform.position = spawnPos;
        }
        else
        {

        }
    }

    public void OnSpawnPool()
    {
        stat.InitStat();
    }
    public void OnReturnPool()
    {
    }
}
