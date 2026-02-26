using UnityEngine;

public class LightningBar : MonoBehaviour
{
    void Update()
    {
        Shader.SetGlobalFloat("_unscaledTime", Time.unscaledTime);
    }
}
