using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting.InputSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class Player : MonoBehaviour
{
    public static Transform playerTransform;

    private void Awake()
    {
        playerTransform = transform;
    }
    void Start()
    {
    }

    void Update()
    {
    }
}
