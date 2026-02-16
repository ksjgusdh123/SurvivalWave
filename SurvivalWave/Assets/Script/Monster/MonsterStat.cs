using UnityEngine;



public class MonsterStat : Stat
{
    [SerializeField] MonsterStatSO monsterStatSO;

    private void Awake()
    {
        int level = GameManager.GetInstance().gameLevel;
        SetStat(monsterStatSO.maxHp + monsterStatSO.increaseHpAmount * level, monsterStatSO.attack + monsterStatSO.increaseAttackAmount * level, monsterStatSO.speed, true);
    }
}   
