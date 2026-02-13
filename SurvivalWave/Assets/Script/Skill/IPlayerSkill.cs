using UnityEngine;

public interface IPlayerSkill
{
    void OnEquip(GameObject owner);
    void TickEvent(float deltaTime);
    void LevelUp();
}
