using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void Init()
    {
        UIManager.GetInstance().Show(EUIType.Main);
    }
}
