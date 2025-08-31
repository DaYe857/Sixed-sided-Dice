using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class BlueDice : NetworkBehaviour,IDice
{
    public void OnTriggerEnter(Collider other)
    {
        if (isClient)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<CubePlayer>().RollCurrentDirection();
                gameObject.SetActive(false);
            }
        }
    }
}
