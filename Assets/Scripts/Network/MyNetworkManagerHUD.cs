using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Mirror;
using Mirror.Discovery;

public class MyNetworkManagerHUD : MonoBehaviour
{
    private NetworkManager networkManager; // 创建 NetworkManager 对象
    private GameObject networkDiscovery;
    //启动网络主机按钮\启动网络客户端按钮\停止网络主机或客户端按钮
    private GameObject startHost, startClient, stopHost;
    void Awake()
    {
        networkManager = GetComponent<NetworkManager>();
        networkDiscovery = GameObject.Find("NetworkDiscovery");
        startHost = GameObject.Find("StartHost");
        //startClient = GameObject.Find("StartClient");
        stopHost = GameObject.Find("StopHost");

        startHost.GetComponent<Button>().onClick.AddListener(OnStartHost);
        //startClient.GetComponent<Button>().onClick.AddListener(StartClient);
        stopHost.GetComponent<Button>().onClick.AddListener(StopHost);

        stopHost.SetActive(false);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Test") 
        {
            if (networkDiscovery.GetComponent<MyNetworkDiscoveryHUD>().discoveredServers.Count > 0)
            {
                startHost.GetComponent<Button>().enabled = false;
                startHost.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.5f);
            }
            else if (networkDiscovery.GetComponent<MyNetworkDiscoveryHUD>().discoveredServers.Count == 0)
            {
                startHost.GetComponent<Button>().enabled = true;
                startHost.GetComponent<Image>().color = new Color(255f, 255f, 255f, 1f);
            }
        }
    }

    private void OnStartHost()
    {
        Debug.Log("OnStartHost");
        networkDiscovery.GetComponent<MyNetworkDiscoveryHUD>().discoveredServers.Clear();
        networkManager.StartHost(); // 启动网络主机
        networkDiscovery.GetComponent<NetworkDiscovery>().AdvertiseServer();

        //startClient.GetComponent<Button>().enabled = false;
        //startClient.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.5f);
        stopHost.SetActive(true);

    }
    private void StartClient()
    {
        Debug.Log("StartClient");
        networkManager.StartClient(); // 启动网络客户端
    }
    private void StopHost()
    {
        networkDiscovery.GetComponent<MyNetworkDiscoveryHUD>().discoveredServers.Clear();
        networkManager.StopServer(); // 停止网络主机
        networkDiscovery.GetComponent<NetworkDiscovery>().StopDiscovery();

        startClient.GetComponent<Button>().enabled = true;
        startClient.GetComponent<Image>().color = new Color(255f, 255f, 255f, 1f);
        stopHost.SetActive(false);
    }
}
