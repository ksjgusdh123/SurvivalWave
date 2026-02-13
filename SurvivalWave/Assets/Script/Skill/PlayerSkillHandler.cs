using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillHandler : MonoBehaviour
{
    List<PlayerSkillBase> skills = new List<PlayerSkillBase>();


    public void AddSkill(PlayerSkillBase skill)
    {
        skill.OnEquip(gameObject);
        skills.Add(skill);
    }

    void Update()
    {
        float deltaTime = Time.deltaTime;
        for (int i = 0; i < skills.Count; i++)
        {
            skills[i].TickEvent(deltaTime);
        }
    }
}
