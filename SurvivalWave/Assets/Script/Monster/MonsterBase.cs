using UnityEngine;
using UnityEngine.AI;

public class MonsterBase : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float Damage = 10f;

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
            Destroy(gameObject);
        }

        agent.SetDestination(player.position);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player") || isCollision) return;

        PlayerStat player = other.GetComponent<PlayerStat>();
        if (null == player || !player.TakeDamage(Damage)) return;
            
        
        isCollision = true;
        Destroy(gameObject);
    }
}
