using UnityEngine;

[CreateAssetMenu(menuName = "Stats/Monster")]
public class MonsterStatSO : ScriptableObject
{
    public float maxHp = 100f;
    public float attack = 10f;
    public float speed = 3f;
    public float increaseHpAmount = 10f;
    public float increaseAttackAmount = 5f;
}
