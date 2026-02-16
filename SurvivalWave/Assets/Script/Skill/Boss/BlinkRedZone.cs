using UnityEngine;

public class BlinkRedZone : MonoBehaviour
{
    public float duration = 0.8f;
    public float minAlpha = 0f;
    public float maxAlpha = 1f;

    Renderer renderComp;
    MaterialPropertyBlock mpb;
    int baseColorId;
    float timer;

    void Awake()
    {
        renderComp = GetComponent<Renderer>();
        mpb = new MaterialPropertyBlock();

        baseColorId = Shader.PropertyToID("_BaseColor");
        SetAlpha(minAlpha);
    }

    void Update()
    {
        timer += Time.deltaTime;
        float a = Mathf.Lerp(minAlpha, maxAlpha, Mathf.Clamp01(timer / duration));
        SetAlpha(a);
    }

    void SetAlpha(float a)
    {
        renderComp.GetPropertyBlock(mpb);

        Color c = Color.red;
        if (renderComp.material.HasProperty(baseColorId))
        {
            c = renderComp.material.GetColor(baseColorId);
        }

        c.a = a;

        mpb.SetColor(baseColorId, c);
        renderComp.SetPropertyBlock(mpb);
    }
}
