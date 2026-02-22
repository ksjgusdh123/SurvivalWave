using UnityEngine;



public class MonsterStat : Stat
{
    [SerializeField] MonsterStatSO monsterStatSO;
    MonsterAnimation anim;
    private void Awake()
    {
        anim = GetComponent<MonsterAnimation>();
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
        if (hp <= 0) anim.NotifyIsDeath(true);
        return true;
    }
}   
