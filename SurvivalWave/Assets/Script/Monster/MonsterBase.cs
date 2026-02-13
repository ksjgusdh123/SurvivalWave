using UnityEngine;
using UnityEngine.AI;

public class MonsterBase : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float Damage = 10f;
    NavMeshAgent agent;

    bool isCollision;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = Player.playerTransform;
    }

    void Update()
    {
        agent.SetDestination(player.position);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player") || isCollision) return;

        Player player = other.GetComponent<Player>();
        if (null == player || !player.TakeDamage(Damage)) return;
            
        
        isCollision = true;
        Destroy(gameObject);
    }
}
