using UnityEngine;
public enum UpdatePolicy
{
    Always,
    Check
}

public interface ITickUpdate 
{
    Vector3 Position { get; }
    float TickInterval { get; }     
    UpdatePolicy Policy { get; }
    void Tick(float delta);
}
