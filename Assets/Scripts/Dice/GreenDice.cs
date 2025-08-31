using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class GreenDice : NetworkBehaviour,IDice
{
    public void OnTriggerEnter(Collider other)
    {
        if (isClient)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<CubePlayer>().RollExchangeState();
                Debug.Log(other.name);
                gameObject.SetActive(false);
            }
        }
    }
}
