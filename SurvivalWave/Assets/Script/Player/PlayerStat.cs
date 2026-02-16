using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public enum StatType
{
    Attack,
    Speed,
    MaxHp
}

public class PlayerStat : Stat
{
    public Action ChangeHp;

    PlayerController controller;
    Renderer[] renderers;
    WaitForSeconds blinkWait;
    WaitForSeconds invincibleFinish;

    [SerializeField] float blinkWaitTime = 0.3f;
    [SerializeField] float invincibleFinishTime = 1f;
    bool isInvincible;

    float exp;
    [SerializeField] float maxExp = 100;
    int level = 1;
    int maxLevel = 30;


    void Start()
    {
        controller = GetComponent<PlayerController>();
        renderers = GetComponentsInChildren<Renderer>();
        blinkWait = new WaitForSeconds(blinkWaitTime);
        invincibleFinish = new WaitForSeconds(invincibleFinishTime);
        controller.speed = speed;
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
        ChangeHp?.Invoke();
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
        if(exp >= maxExp && maxLevel > level)
        {
            exp -= maxExp;
            maxExp = 1; // ((level - 1) * 50)  + 100;
            UIManager.GetInstance().Show(EUIType.LevelUp);
        }
    }

    public void ChangeSpeed(float amount)
    {
        speed = amount;
        controller.speed = speed;
    }

    public void UpgradeMaxHp(float amount)
    {
        maxHp = amount;
        ChangeHp?.Invoke();
    }

}
