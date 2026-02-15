using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MonsterDamaged : MonoBehaviour
{
    Renderer renderComp;

    Color originColor;
    MaterialPropertyBlock mpb;

    void Start()
    {
        renderComp = GetComponentInChildren<Renderer>();
        mpb = new MaterialPropertyBlock();
        originColor = renderComp.material.color;
    }

    public IEnumerator ChangeColor()
    {
        mpb.SetColor("_BaseColor", Color.red);

        float duration = 1f;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            Color current = Color.Lerp(Color.red, originColor, t);

            renderComp.GetPropertyBlock(mpb);
            mpb.SetColor("_BaseColor", current);
            renderComp.SetPropertyBlock(mpb);

            yield return null;
        }
    }
}
