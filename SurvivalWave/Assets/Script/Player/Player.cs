using System.Collections;
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
    Renderer[] renderers;
    WaitForSeconds blinkWait;
    WaitForSeconds invincibleFinish;

    [SerializeField] float blinkWaitTime = 0.3f; 
    [SerializeField] float invincibleFinishTime = 1f; 
    bool isInvincible;


    void Start()
    {
        controller = GetComponent<PlayerController>();
        renderers = GetComponentsInChildren<Renderer>();
        blinkWait = new WaitForSeconds(blinkWaitTime);
        invincibleFinish = new WaitForSeconds(invincibleFinishTime);
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

    public void FinishDamaged()
    {
        StartCoroutine(Blink());
        StartCoroutine(FinishInvincible());
    }

    IEnumerator FinishInvincible()
    {
        yield return invincibleFinish;

        isInvincible = false;
        SetVisible(true);
    }

    IEnumerator Blink()
    {
        while(isInvincible)
        {
            SetVisible(false);
            yield return blinkWait;

            SetVisible(true);
            yield return blinkWait;
        }   
    }

    void SetVisible(bool value)
    {
        foreach (var r in renderers)
            r.enabled = value;
    }
}
