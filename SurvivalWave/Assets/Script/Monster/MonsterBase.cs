using UnityEngine;
using UnityEngine.AI;

public class MonsterBase : MonoBehaviour
{
    Transform player;
    Stat stat;
    MonsterDamaged damagedEventComp;
    NavMeshAgent agent;

    bool isCollision;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        stat = GetComponent<Stat>();
        player = Player.playerTransform;
        damagedEventComp = GetComponent<MonsterDamaged>();
    }

    void Update()
    {
        if(stat.hp <= 0f)
        {
            if (!agent.isStopped)   
            {
                agent.isStopped = true;
                agent.ResetPath();
            }
            return;
        }

        if (!agent || !agent.isActiveAndEnabled) return;
        if (!agent.isOnNavMesh) return;

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
        Destroy(gameObject);
    }

    void PickExp(Vector3 spawnPos)
    {
        spawnPos.y += 1f;

        GameObject go = Instantiate(Resources.Load<GameObject>("Prefab/Item/Exp"), spawnPos, Quaternion.identity);
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
            GameObject go = Instantiate(Resources.Load<GameObject>("Prefab/Item/HpPotion"), spawnPos, Quaternion.identity);
        }
        else
        {

        }
    }
}
