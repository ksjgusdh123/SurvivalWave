using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillPanel : MonoBehaviour
{
    SkillSlot[] slotArray;
    Dictionary<int, SkillSlot> slots = new Dictionary<int, SkillSlot>();

    int nextSlotIndex = 0;
    private void Awake()
    {
       slotArray = GetComponentsInChildren<SkillSlot>(true);
        slotArray = slotArray.OrderBy(s => s.transform.GetSiblingIndex()).ToArray();
    }
    public void UpdateSkillPanel(SkillData skillData, int level)
    {
        if(slots.TryGetValue(skillData.skillId, out var slot))
        {
            slot.ChangeLevel(level);
        }
        else
        {
            Register(skillData);
        }
    }
    public void Register(SkillData skillData)
    {
        var slot = slotArray[nextSlotIndex++];
        slot.SetSkillData(skillData);
        slots[skillData.skillId] = slot;
    }
}
