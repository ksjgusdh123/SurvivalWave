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
    public Action ChangeStamina;
    public Action ChangeExp;

    PlayerController controller;
    Renderer[] renderers;
    WaitForSeconds blinkWait;
    WaitForSeconds invincibleFinish;

    [SerializeField] float blinkWaitTime = 0.3f;
    [SerializeField] float invincibleFinishTime = 1f;
    bool isInvincible;

    public float exp;
    public float maxExp = 100;
    int level = 1;
    int maxLevel = 30;

    public float maxStamina { get; private set; } = 100f;
    public float stamina { get; private set; }

    void Start()
    {
        controller = GetComponent<PlayerController>();
        renderers = GetComponentsInChildren<Renderer>();
        blinkWait = new WaitForSeconds(blinkWaitTime);
        invincibleFinish = new WaitForSeconds(invincibleFinishTime);
        controller.speed = speed;
        stamina = maxStamina;
    }

    void Update()
    {
        CheckStaminaEvent();
    }

    void CheckStaminaEvent()
    {
        if (PlayerState.Falling == controller.state)
        {
            stamina -= Time.deltaTime * 10f;
            ChangeStamina();
            if (stamina <= 0f)
            {
                controller.KickOffAir();
            }
        }
    }

    public override bool TakeDamage(float dmg)
    {
        if (isInvincible || hp <= 0) return false;

        base.TakeDamage(dmg);

        if (hp <= 0)
        {
            controller.Die();
            isInvincible = false;
        }
        else
        {
            controller.Damaged();
            isInvincible = true;
        }
        ChangeHp?.Invoke();
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
            maxExp = ((level - 1) * 50)  + 100;
            ++level;
            UIManager.GetInstance().Show(EUIType.LevelUp);
        }
        ChangeExp.Invoke();
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

    public void Heal(float amount)
    {
        hp = Math.Min(hp + amount, maxHp);
        ChangeHp?.Invoke();
    }

}
