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
    int checkStamp { get; set; }
    void Tick(float delta);
}
