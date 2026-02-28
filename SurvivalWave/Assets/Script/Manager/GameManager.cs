using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    PlayerStat playerStat;
    PlayerSkillHandler skillHandler;
    MonsterSpawner spawner;
    public int gameLevel = 0;
    public float survivalTime { get; private set; }
    WaitForSeconds gameLevelTimerHandle;

    protected override void Awake()
    {
        spawner = FindFirstObjectByType<MonsterSpawner>();
        skillHandler = FindFirstObjectByType<PlayerSkillHandler>();
        playerStat = FindFirstObjectByType<PlayerStat>();
    }

    private void Start()
    {
        gameLevelTimerHandle = new WaitForSeconds(60f);
        StartCoroutine(ChangeLevel());

        //int cnt = 1000;
        //float y = Player.playerTransform.position.y + 1.5f;
        //for (int i = 0; i < cnt; ++i)
        //{
        //    Vector3 spawnPos = new Vector3(0f, y, i);
        //    GameObject go = ItemPool.GetInstance().PopObject(ItemType.Exp);
        //    go.transform.position = spawnPos;
        //}
    }

    IEnumerator ChangeLevel()
    {
        while (true)
        {
            yield return gameLevelTimerHandle;
            ++gameLevel;
            spawner.StartFocusSpawn();
            // spawn Boss
        }
    }

    public void PickLevelUpUI(int skillId)
    {
        skillHandler.LevelUp(skillId);
    }

    public void UpgradeAbility(StatType type, float amount)
    {
        switch (type)
        {
            case StatType.Attack:
            {
                playerStat.attack = amount * 10f;   
            }
            break;
            case StatType.Speed:
            {
                playerStat.ChangeSpeed(amount);
            }
            break;
            case StatType.MaxHp:
            {
                playerStat.UpgradeMaxHp(amount);
            }
            break;
        }
    }

    public int GetSkillLevel(int skillId)
    {
        return skillHandler.GetSkillLevel(skillId);
    }
    public void EndGame()
    {
        var go = GameObject.FindFirstObjectByType<Timer>();
        survivalTime = go.survivalTime;
        UIManager.GetInstance().Show(EUIType.Result);
    }
}
