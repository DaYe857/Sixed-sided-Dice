using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForDisappear : MonoBehaviour
{
    [SerializeField] 
    private float seconds = 2f;

    private void Start()
    {
        StartCoroutine(WaitDisappear());
    }

    IEnumerator WaitDisappear()
    {
        yield return new WaitForSeconds(seconds);
        gameObject.SetActive(false);
    }
}
