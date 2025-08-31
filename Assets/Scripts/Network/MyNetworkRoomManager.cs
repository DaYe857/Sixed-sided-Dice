using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MyNetworkRoomManager : NetworkRoomManager
{
    /*private int currentNum;
    [SerializeField]
    private List<GameObject> playerPrefabs;
    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        int newNum;
        do
        {
            newNum = Random.Range(-1, 3);
        }while(newNum == currentNum);
        currentNum = newNum;
        Transform startPos = GetStartPosition();
        GameObject player = startPos != null ? Instantiate(playerPrefabs[currentNum], startPos.position, startPos.rotation):Instantiate(playerPrefabs[currentNum]);
        
        NetworkServer.AddPlayerForConnection(conn, player);
        playerPrefabs.Remove(playerPrefabs[currentNum]);
        Debug.Log(playerPrefabs[currentNum].name);
    }*/

    public override void OnClientConnect()
    {
        base.OnClientConnect();
        Debug.Log("Connect to Sever");
    }
}
