using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    PlayerSkillHandler skillHandler;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void InitBeforeScene()
    {
        SkillDataManager.GetInstance().LoadData();  
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void InitAfterScene()
    {
        UIManager.GetInstance().Show(EUIType.Main);
    }

    protected override void Awake()
    {
        skillHandler = FindFirstObjectByType<PlayerSkillHandler>();
    }

    public void PickLevelUpUI(int skillId)
    {
        skillHandler.LevelUp(skillId);
    }

}
