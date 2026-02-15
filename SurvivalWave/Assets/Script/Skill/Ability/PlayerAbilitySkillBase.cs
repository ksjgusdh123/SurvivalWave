using Unity.VisualScripting;
using UnityEngine;

public class PlayerAbilitySkillBase : PlayerSkillBase
{
    protected PlayerAbilitySkillBase(int id)
         : base(id)
    {
    }

    public override void OnEquip(GameObject ownerObj)
    {
        var player = ownerObj.GetComponent<Player>();
        if (null == player) return;
        UpgradeStat();
    }

    public override void TickEvent(float deltaTime)
    {
    }

    protected override bool TryFire()
    {
        return true;
    }

    public override void UpgradeStat()
    {
    }
}
