using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillHandler : MonoBehaviour
{
    List<PlayerSkillBase> skills = new List<PlayerSkillBase>();


    public void AddSkill(PlayerSkillBase skill)
    {
        skill.OnEquip(gameObject);
        skills.Add(skill);
        UIManager.GetInstance().UpdateSkillPanel(skill.skillData, 1);
    }

    void Update()
    {
        float deltaTime = Time.deltaTime;
        for (int i = 0; i < skills.Count; i++)
        {
            skills[i].TickEvent(deltaTime);
        }
    }

    public void LevelUp(int skillId)
    {
        foreach (var skill in skills)
        {
            if(skillId == skill.skillId)
            {
                skill.LevelUp();
                return;
            }
        }

        switch ((SkillItemType)skillId)
        {
            case SkillItemType.RandomShot:
                AddSkill(new RandomShotSkill(1f, 2f, 10f, 40f));
                break;
            case SkillItemType.Homing:
                AddSkill(new HomingSkill(1f, 20f, 10f));
                break;
            case SkillItemType.Boomerang:
                AddSkill(new BoomerangSkill(1f, 2f, 10f, 40f));
                break;
            case SkillItemType.WideShot:
                AddSkill(new WideShotSkill(1f, 2f, 10f, 40f, 16));
                break;
            case SkillItemType.Strength:
                AddSkill(new StrengthSkill());
                break;
            case SkillItemType.Speed:
                AddSkill(new SpeedSkill());
                break;
            case SkillItemType.MaxHp:
                AddSkill(new MaxHpSkill());
                break;
            default:
                Debug.Log("Not Yet");
                break;
        }
    }

    public int GetSkillLevel(int skillId)
    {
        foreach (var skill in skills)
        {
            if(skillId == skill.skillId)
            {
                return skill.level;
            }
        }
        return -1;
    }
}
