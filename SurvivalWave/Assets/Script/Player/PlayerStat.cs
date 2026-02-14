using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerStat : Stat
{
    PlayerController controller;
    Renderer[] renderers;
    WaitForSeconds blinkWait;
    WaitForSeconds invincibleFinish;

    [SerializeField] float blinkWaitTime = 0.3f;
    [SerializeField] float invincibleFinishTime = 1f;
    bool isInvincible;

    float exp;


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

    public override bool TakeDamage(float dmg)
    {
        if (isInvincible) return false;

        base.TakeDamage(dmg);

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
        while (isInvincible)
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
        {
            r.enabled = value;
        }
    }

    public void GainExp(float amount)
    {
        exp += amount;
    }

}
