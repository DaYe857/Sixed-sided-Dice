using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Mirror;
using Mirror.Discovery;
public class MyNetworkDiscoveryHUD : MonoBehaviour
{
    public readonly Dictionary<long, ServerResponse> discoveredServers = new Dictionary<long, ServerResponse>();
    Vector2 scrollViewPos = Vector2.zero;
    private NetworkDiscovery networkDiscovery;

    private void Awake()
    {
        networkDiscovery = GetComponent<NetworkDiscovery>();
        discoveredServers.Clear();
        networkDiscovery.StartDiscovery();
    }

    private void OnGUI()
    {
        GUILayout.Label($"Discovered Servers [{discoveredServers.Count}]:");

        // servers
        scrollViewPos = GUILayout.BeginScrollView(scrollViewPos);

        foreach (ServerResponse info in discoveredServers.Values)
            if (GUILayout.Button(info.EndPoint.Address.ToString()))
                Connect(info);

        GUILayout.EndScrollView();
    }

    public void OnDiscoveredServer(ServerResponse info)
    {
        // Note that you can check the versioning to decide if you can connect to the server or not using this method
        discoveredServers[info.serverId] = info;
    }

    void Connect(ServerResponse info)
    {
        networkDiscovery.StopDiscovery();
        NetworkManager.singleton.StartClient(info.uri);
    }
}
