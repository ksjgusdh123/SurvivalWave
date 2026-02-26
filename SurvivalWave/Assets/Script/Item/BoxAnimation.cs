using System.Collections;
using UnityEngine;

public class BoxAnimation : MonoBehaviour
{
    [SerializeField] Transform cap;

    Quaternion oringRotation;
    Vector3 originPosition;

    public bool isSpin;
    bool isOpen;
    float originSpeed = 1f;
    float spinSpeed = 1f;
    float openSpeed;
    float spinDuration = 2f;
    float openDuration = 1f;
    WaitForSecondsRealtime spinCoroutine;
    WaitForSecondsRealtime openCoroutine;
    void Start()
    {
        originPosition = cap.position;
        oringRotation = cap.rotation;
        openCoroutine = new WaitForSecondsRealtime(openDuration);
        spinCoroutine = new WaitForSecondsRealtime(spinDuration);
        openSpeed = originSpeed * 2f;
    }
    void Update()
    {
        float t = Time.unscaledDeltaTime;
        if (isSpin)
        {
            spinSpeed += t;
            cap.Rotate(Vector3.up, 360f * t * spinSpeed);
        }
        else if (isOpen)
        {
            cap.position += Vector3.up * t * openSpeed;
            cap.Rotate(Vector3.up, 360f * t * spinSpeed);
        }
    }
    public void StartBoxAnimation()
    {
        spinSpeed = originSpeed;
        cap.rotation = oringRotation;
        cap.position = originPosition;
        isSpin = true;
        isOpen = false;
        StartCoroutine(StartSpin());
    }
    IEnumerator StartSpin()
    {
        yield return spinCoroutine;
        isOpen = true;
        isSpin = false;
        StartCoroutine(OpenEvent());
    }
    IEnumerator OpenEvent()
    {
        yield return openCoroutine;
        //UIManager.GetInstance().Show(EUIType.LevelUp);
        isOpen = false;
    }
}
