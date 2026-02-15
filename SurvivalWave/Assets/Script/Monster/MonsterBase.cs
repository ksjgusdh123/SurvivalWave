using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.Rendering.DebugUI.Table;

public class MonsterBase : MonoBehaviour
{
    [SerializeField] Transform player;
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
        StartCoroutine(damagedEventComp.ChangeColor());
    }
    void EndDieAnimation()
    {
        Vector3 spawnPos = transform.position + new Vector3(0f, 1f, 0f);

        GameObject go = Instantiate(Resources.Load<GameObject>("Prefab/Item/Exp"), spawnPos, Quaternion.identity);
        Exp exp = go.GetComponent<Exp>();
        Renderer renderer = go.GetComponent<Renderer>();

        GetRandomExp(exp, renderer);
        Destroy(gameObject);
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
}
