using UnityEngine;
using UnityEngine.AI;

public class MonsterBase : MonoBehaviour
{
    [SerializeField] Transform player;
    NavMeshAgent agent;

    bool isCollision;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        agent.SetDestination(player.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision");
        if (!other.CompareTag("Player") || isCollision) return;

        Player player = other.GetComponent<Player>();
        if (null == player || !player.TakeDamage(10f)) return;
            
        
        isCollision = true;
        Destroy(gameObject);
    }
}
