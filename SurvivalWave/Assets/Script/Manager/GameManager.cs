using UnityEngine;

public class GameManager : Singleton<GameManager>
{
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
}
