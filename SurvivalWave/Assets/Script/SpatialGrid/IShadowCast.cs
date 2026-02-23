using UnityEngine;

public interface IShadowCast 
{
    bool isOn { get; }
    void SetNearShadow(bool on);
}
