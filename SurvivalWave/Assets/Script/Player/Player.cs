using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class Player : MonoBehaviour
{
    public static Transform playerTransform;
    PlayerSkillHandler skillHandler;

    private void Awake()
    {
        playerTransform = transform;
    }
    void Start()
    {
        skillHandler = GetComponent<PlayerSkillHandler>();

        skillHandler.AddSkill(new RandomShotSkill(1f, 2f, 10f, 40f));
    }

    void Update()
    {
    }
}
