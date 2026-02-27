using JetBrains.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public enum BGMType
{
    Game,
    Max
}
public enum SFXType
{
    LevelUp,
    BoxOpening,
    BoxOpen,
    BoxSpin,
    BoxShake,
    Magnet,
    HpPotion,
    Exp,
    BossAttack,
    BossDie,
    SlimeDie,
    TurtleDie,
    PlayerDamaged,
    PlayerDie,
    Jump,
    Max
}

public class SoundManager : Singleton<SoundManager>
{
    AudioSource BGMSource;
    AudioSource SFXSource;

    Dictionary<BGMType, AudioClip> BGMDic = new Dictionary<BGMType, AudioClip>();
    Dictionary<SFXType, AudioClip> SFXDic = new Dictionary<SFXType, AudioClip>();

    protected override void Awake()
    {
        BGMSource = gameObject.AddComponent<AudioSource>();
        SFXSource = gameObject.AddComponent<AudioSource>();

        BGMSource.loop = true;
        SFXSource.loop = false;
    }
    public async Task InitSound()
    {
        int bgmCnt = (int)BGMType.Max;
        for (int i = 0; i < bgmCnt; ++i)
        {
            BGMDic[(BGMType)i] = await Addressables.LoadAssetAsync<AudioClip>($"Sound/BGM/{(BGMType)i}").Task;
        }

        int effectCnt = (int)SFXType.Max;
        for (int i = 0; i < effectCnt; ++i)
        {
            SFXDic[(SFXType)i] = await Addressables.LoadAssetAsync<AudioClip>($"Sound/SFX/{(SFXType)i}").Task;
        }
    }

    public void PlayBGM(BGMType type)
    {
        if (!BGMDic.TryGetValue(type, out var clip)) return;
        BGMSource.clip = clip;
        BGMSource.Play();
    }
    public void PlaySFX(SFXType type)
    {
        if (!SFXDic.TryGetValue(type, out var clip)) return;
        SFXSource.PlayOneShot(clip);
    }
    public void PauseBGM()
    {
        BGMSource.Pause();
    }
    public void UnPauseBGM()
    {
        BGMSource.UnPause();
    }
}
