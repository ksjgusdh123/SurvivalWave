using System;
using UnityEditor.ShortcutManagement;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Exp : ItemBase
{
    public override ItemType type { get; } = ItemType.Exp;
    public float amount { get; set; }
    public float maxDist { get; set; } = 1f;

    [SerializeField] float speed = 10f;
    [SerializeField] float magnetPower = 20f;

    Transform player;
    bool isMagent;
    private void Awake()
    {
        player = Player.playerTransform;
    }
    private void OnEnable()
    {
        player.GetComponent<Player>().GetMagnet -= ChangeDistance;
        player.GetComponent<Player>().GetMagnet += ChangeDistance;
        maxDist = 1f;
        isMagent = false;
    }
    private void OnDisable()
    {
        if(null != player) player.GetComponent<Player>().GetMagnet -= ChangeDistance;
    }
    public override void OnGain(GameObject player)
    {   
        player.GetComponent<PlayerStat>().GainExp(amount);
        ItemPool.GetInstance().ReturnObject(gameObject, type);
    }

    private void Update()
    {
        Vector3 pos, to;
        float dist;
        CalculateDistance(out pos, out to, out dist);
        if (!isMagent && maxDist < dist) return;
        isMagent = true;

        Vector3 dir = to / dist;
        Vector3 next = pos + dir * speed * Time.deltaTime;
        transform.position = next;
    }
    public override void Tick(float delta)
    {

    }
    public void ChangeDistance()
    {
        if (isMagent) return;

        Vector3 pos, to;
        float dist;
        CalculateDistance(out pos, out to, out dist);
        if (maxDist * magnetPower < dist) return;
        if (!isMagent) isMagent = true;
    }
    void CalculateDistance(out Vector3 pos, out Vector3 to, out float distance)
    {
        pos = transform.position;
        to = player.position - pos;
        to.y += 1;
        distance = to.magnitude;
    }
}
