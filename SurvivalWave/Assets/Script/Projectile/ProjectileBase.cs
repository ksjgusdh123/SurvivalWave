using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    [SerializeField] public float speed { get; private set; } = 10f;
    protected IProjectileMove move;

    public void SetMovingType(IProjectileMove type)
    {
        move = type;
    }

    void Update()
    {
        move?.Move(this);
    }
}
