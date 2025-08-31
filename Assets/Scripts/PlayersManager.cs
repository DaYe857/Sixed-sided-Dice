using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class PlayersManager : MonoBehaviour
{
    public CubePlayer[] players;

    private void Start()
    {
        StartCoroutine(InitPlayers());
    }

    IEnumerator InitPlayers()
    {
        yield return new WaitForSeconds(2f);
        players = FindObjectsOfType<CubePlayer>();
        FindObjectOfType<UIManager>().StartCount(this);
        Debug.Log(players.Length);
    }
    
    public CubePlayer GetPlayer(int number) => players[number]; 
    
    public NetworkIdentity GetNetworkIdentity(int num) => players[num].netIdentity; 
}
