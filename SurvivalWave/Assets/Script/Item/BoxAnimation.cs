using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UI.Image;

public class BoxAnimation : MonoBehaviour
{
    [SerializeField] Transform cap;
    [SerializeField] LightningBar bar;

    public Action pickRandomItem;

    Quaternion oringCapRotation;
    Quaternion originRotation;
    Vector3 originCapPosition;

    public bool isShake;
    public bool isSpin;
    bool isWait;
    bool isOpen;

    [SerializeField] float amplitude = 25f;
    [SerializeField] float frequency = 35f;
    float originSpeed = 1f;
    float spinSpeed = 1f;
    float openSpeed;
    float shakeDuration = 1f;
    float pauseDuration = 0.25f;
    float spinDuration = 2f;
    float openDuration = 1f;
    float shakeTimer = 0f;
    WaitForSecondsRealtime spinCoroutine;
    WaitForSecondsRealtime openCoroutine;
    void Start()
    {
        originRotation = transform.rotation;
        originCapPosition = cap.position;
        oringCapRotation = cap.rotation;
        openCoroutine = new WaitForSecondsRealtime(openDuration);
        spinCoroutine = new WaitForSecondsRealtime(spinDuration);
        openSpeed = originSpeed * 2f;
    }
    void Update()
    {
        float t = Time.unscaledDeltaTime;
        if(isShake)
        {
            if(isWait)
            {
                shakeTimer += t;
                if (shakeTimer >= pauseDuration)
                {
                    shakeTimer = 0f;
                    isWait = false; 
                }
                return;
            }
            shakeTimer += Time.unscaledDeltaTime;
        
            float n = Mathf.Clamp01(shakeTimer / shakeDuration);
            float damp = 1f - n; 
            float ax = Mathf.Sin(shakeTimer * frequency * 7f) * amplitude * damp;
            float az = Mathf.Sin((shakeTimer * frequency * 0.7f) * 7f + 1.2f) * (amplitude * 0.6f) * damp;
            transform.localRotation = originRotation * Quaternion.Euler(ax, 0f, az);

            if (shakeTimer >= shakeDuration)
            {
                shakeTimer = 0f;
                transform.localRotation = originRotation;
                isWait = true;
            }
        }
        else if (isSpin)
        {
            spinSpeed += t;
            cap.Rotate(Vector3.up, 360f * t * spinSpeed);
        }
        else if (isOpen)
        {
            Shader.SetGlobalFloat("_unscaledTime", Time.unscaledTime);
            cap.position += Vector3.up * t * openSpeed;
            cap.Rotate(Vector3.up, 360f * t * spinSpeed);
        }
    }
    public void StartBoxShakeAnimation()
    {
        isShake = true;
        cap.rotation = oringCapRotation;
        cap.position = originCapPosition;
    }
    public void FinishBoxShake()
    {
        isShake = false;
        shakeTimer = 0f;
        transform.localRotation = originRotation;
    }
    public void StartBoxAnimation()
    {
        FinishBoxShake();
        spinSpeed = originSpeed;
        isSpin = true;
        isOpen = false;
        StartCoroutine(FinishSpin());
    }
    IEnumerator FinishSpin()
    {
        yield return spinCoroutine;
        isOpen = true;
        isSpin = false;
        pickRandomItem?.Invoke();
        bar.gameObject.SetActive(true);
        StartCoroutine(OpenEvent());
    }
    IEnumerator OpenEvent()
    {
        yield return openCoroutine;
        isOpen = false;
    }
    public void Hide()
    {
        bar.gameObject.SetActive(false);
    }
}
