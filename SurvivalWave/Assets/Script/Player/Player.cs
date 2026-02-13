using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class Player : MonoBehaviour
{
    public float hp { get; private set; } = 100f;
    public float maxHp { get; private set; } = 100f;

    PlayerController controller;
    bool isInvincible;


    void Start()
    {
        controller = GetComponent<PlayerController>();
    }

    void Update()
    {
    }

    public bool TakeDamage(float dmg)
    {
        if (isInvincible) return false;

        hp -= dmg;

        if (hp < 0)
        {

        }
        else
        {
            controller.Damaged();
        }
        isInvincible = true;    
        return true;
    }
}
