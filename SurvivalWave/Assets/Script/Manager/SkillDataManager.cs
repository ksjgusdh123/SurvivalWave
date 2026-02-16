using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public enum SkillItemType
{
    RandomShot,
    Homing,
    Boomerang,
    // ´É·ÂÄ¡
    Strength,
    Speed,
    MaxHp
}

public class SkillDataManager : Singleton<SkillDataManager>
{
    List<SkillData> datas = new List<SkillData>();

    public void LoadData()
    {
        string path = "Data/SkillData";

        TextAsset csvFile = Resources.Load<TextAsset>(path);

        string[] lines = csvFile.text.Split('\n');

        int cnt = lines.Length;

        for (int i = 1; i < cnt; ++i)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            string[] values = lines[i].Split(',');

            SkillData skill = new SkillData();

            skill.skillId = int.Parse(values[0]);
            skill.skillName = values[1];
            skill.description = values[2];  
            skill.launchInterval = float.Parse(values[3]);
            skill.damageRatio = float.Parse(values[4]);
            skill.decreaseLaunchInterval = float.Parse(values[5]);
            skill.increaseDamageRatio = float.Parse(values[6]);

            datas.Add(skill);
        }
    }

    public List<SkillData> PickRandomSkill(int cnt)
    {
        List<SkillData> result = new List<SkillData>();

        List<SkillData> pool = new List<SkillData>(datas);

        for (int i = 0; i < cnt; i++)
        {
            int rand = Random.Range(0, pool.Count);
            result.Add(pool[rand]);
            pool.RemoveAt(rand);
        }

        return result;
    }

    public SkillData GetSkillData(SkillItemType type)
    {
        return datas[(int)type];
    }
}
