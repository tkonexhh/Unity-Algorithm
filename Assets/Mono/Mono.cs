using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mono : MonoBehaviour
{
    private void Awake()
    {
        Debug.LogError("Awake");
    }

    private void OnEnable()
    {
        Debug.LogError("OnEnable");
    }

    private void Start()
    {
        Debug.LogError("Start");
    }

    private void Update()
    {
        Debug.LogError("Update");
    }

    private void FixedUpdate()
    {
        Debug.LogError("FixedUpdate");
    }

    private void LateUpdate()
    {
        Debug.LogError("LateUpdate");
    }

    private void OnDisable()
    {
        Debug.LogError("OnDisable");
    }
}
