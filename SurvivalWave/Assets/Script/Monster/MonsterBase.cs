using UnityEngine;
using UnityEngine.AI;

public class MonsterBase : MonoBehaviour
{
    [SerializeField] Transform player;

    Stat stat;

    NavMeshAgent agent;

    bool isCollision;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        stat = GetComponent<Stat>();
        player = Player.playerTransform;
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

    void EndDieAnimation()
    {
        Destroy(gameObject);
    }
}
