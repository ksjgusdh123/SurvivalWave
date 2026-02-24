using System.Collections;
using TMPro;
using UnityEngine;

public class LoadingDot : MonoBehaviour
{
    public string baseText { get; set; } = "DownLoading";
    [SerializeField] TMP_Text text;
    [SerializeField] float interval = 0.25f;
    [SerializeField] int maxDots = 3;

    Coroutine coroutine;

    void OnEnable()
    {
        coroutine = StartCoroutine(Run());
    }
    void OnDisable()
    {
        if (coroutine != null) StopCoroutine(coroutine); coroutine = null;
    }

    IEnumerator Run()
    {
        int dots = 0;
        int dir = 1; 

        while (true)
        {
            text.text = baseText + new string('.', dots);

            yield return new WaitForSecondsRealtime(interval);

            dots += dir;

            if (dots >= maxDots) { dots = maxDots; dir = -1; }
            else if (dots <= 0) { dots = 0; dir = 1; }
        }
    }
}
