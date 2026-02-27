using System.Collections;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [SerializeField] Transform mainCamera;
    WaitForSeconds damageAnimTIme;
    TextMeshProUGUI damageText;
    Animator animator;

    private void Awake()
    {
        mainCamera = GameObject.Find("Main Camera").transform;
        animator = GetComponentInChildren<Animator>();
        damageText = GetComponentInChildren<TextMeshProUGUI>();
        damageAnimTIme = new WaitForSeconds(1f);
    }
    private void Start()
    {
    }
    private void LateUpdate()
    {
        transform.forward = mainCamera.forward;
    }
    public void SpawnDamageText(Vector3 position, float damage)
    {
        transform.position = position;
        damageText.text = damage.ToString();
        animator.Play("damageTextSpawn");
        StartCoroutine(StartDamageCount());
    }
    IEnumerator StartDamageCount()
    {
        yield return damageAnimTIme;
        gameObject.SetActive(false);
    }
}
