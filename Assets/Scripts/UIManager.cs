using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : NetworkBehaviour
{
    [SerializeField] 
    private GameObject winPanel;
    
    [SerializeField]
    private GameObject losePanel;
    
    [SerializeField]
    private List<Slider> sliders;
    
    public CubePlayer[] players;
    
    public bool canCount = false;

    public void StartCount(PlayersManager manager)
    {
        players = manager.players;
        canCount = true;
    }

    private void Update()
    {
        if (canCount)
        {
            for (int i = 0; i < players.Length; i++)
            {
                sliders[i].value = players[i].GetEndDistance() / 46f;
            }
        }
    }

    [ClientRpc]
    private void RpcUpdateSliders()
    {
        ShowLosePanel();
    }

    [Command(requiresAuthority = false)]
    private void CmdUpdateSliders()
    {
        for (int i = 0; i < players.Length; i++)
        {
            sliders[i].value = (23f - players[i].GetEndDistance()) / 23f;
        }
    }

    public void ShowWinPanel()
    {
        CmdShowLosePanel();
        losePanel.SetActive(false);
        winPanel.SetActive(true);
        Time.timeScale = 0f;
        
    }

    [Command(requiresAuthority = false)]
    private void CmdShowLosePanel()
    {
        RpcShowLosePanel();
    }

    [ClientRpc]
    private void RpcShowLosePanel()
    {
        winPanel.SetActive(false);
        losePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ShowLosePanel()
    {
        winPanel.SetActive(false);
        losePanel.SetActive(true);
    }
    /*private void OnEnable()
    {
        EventHandler.WinDiceEvent += () => winPanel.SetActive(true);
        EventHandler.LoseDiceEvent += () => losePanel.SetActive(true);
    }

    private void OnDisable()
    {
        EventHandler.WinDiceEvent -= () => winPanel.SetActive(true);
        EventHandler.LoseDiceEvent -= () => losePanel.SetActive(true);
    }*/
}
