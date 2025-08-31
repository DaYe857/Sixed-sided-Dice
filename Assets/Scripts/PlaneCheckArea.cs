using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneCheckArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<CubePlayer>().WinGame();
            Time.timeScale = 0f;
        }
    }
}
