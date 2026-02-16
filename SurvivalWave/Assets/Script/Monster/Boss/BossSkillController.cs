using System.Collections.Generic;
using UnityEngine;

public class BossSkillController : MonoBehaviour
{
    [SerializeField] List<BossSkillBase> skills = new List<BossSkillBase>();
    List<float> cooltimes = new List<float>();
    Transform target;

    int size = 0;
    public float range { get; private set; } = 10f;
    float bossCool = 5f;
    float bossCoolTimer = 0f;
    private void Start()
    {
        size = skills.Count;
        for (int i = 0; i < size; i++) cooltimes.Add(0f);
        target = Player.playerTransform;
    }

    private void Update()
    {
        UpdateCooltimes();
        if(bossCoolTimer <= 0f)
        {
            int idx = PickRandomIndex();
            if (idx < 0) return;

            bossCoolTimer = bossCool;
            skills[idx].Casting(target);
            cooltimes[idx] = skills[idx].cooltime;
        }
    }

    void UpdateCooltimes()
    {
        float delta = Time.deltaTime;

        if (bossCoolTimer > 0f) bossCoolTimer -= delta;

        for (int i = 0; i < size; ++i)
        {
            if(cooltimes[i] > 0f) cooltimes[i] -= delta;
        }
    }

    int PickRandomIndex()
    {
        int count = 0;
        int chosenIndex = -10;

        for (int i = 0; i < cooltimes.Count; i++)
        {
            if (cooltimes[i] > 0f) continue;

            count++;
            if (Random.Range(0, count) == 0)
                chosenIndex = i;
        }

        return chosenIndex;

    }
}
