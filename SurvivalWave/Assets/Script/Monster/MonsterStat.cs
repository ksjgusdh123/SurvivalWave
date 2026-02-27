using System;
using UnityEngine;



public class MonsterStat : Stat
{
    [SerializeField] MonsterStatSO monsterStatSO;
    MonsterAnimation anim;
    MonsterType type;
    private void Awake()
    {
        anim = GetComponent<MonsterAnimation>();
        type = GetComponent<MonsterBase>().type;
        InitStat();
    }

    public void InitStat()
    {
        int level = GameManager.GetInstance().gameLevel;
        SetStat(monsterStatSO.maxHp + monsterStatSO.increaseHpAmount * level, monsterStatSO.attack + monsterStatSO.increaseAttackAmount * level, monsterStatSO.speed, true);
    }
    public override bool TakeDamage(float dmg)
    {
        if (hp <= 0) return true;
        base.TakeDamage(dmg);
        if (hp <= 0)
        {
            anim.NotifyIsDeath(true);
            string name = type.ToString() + "Die";
            SFXType sound = Enum.Parse<SFXType>(name);
            SoundManager.GetInstance().PlaySFX(sound);
        }
        return true;
    }
}   
