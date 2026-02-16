using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class Player : MonoBehaviour
{
    public static Transform playerTransform;
    public Transform firePosition { get; private set; }
    PlayerSkillHandler skillHandler;

    private void Awake()
    {
        playerTransform = transform;
        firePosition = transform.Find("FirePosition");
    }
    void Start()
    {
        skillHandler = GetComponent<PlayerSkillHandler>();

        //skillHandler.AddSkill(new RandomShotSkill(1f, 2f, 10f, 40f));
        //skillHandler.AddSkill(new HomingSkill(1f, 20f, 10f));
        //skillHandler.AddSkill(new BoomerangSkill(1f, 2f, 10f, 40f));
        //skillHandler.AddSkill(new StrengthSkill());

        ProjectileSpawnManager.GetInstance().RegisterPlayerStat(GetComponent<PlayerStat>());
    }

    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IPickupable>(out IPickupable pickupable))
        {
            pickupable.OnGain(gameObject);
        }
    }
}
