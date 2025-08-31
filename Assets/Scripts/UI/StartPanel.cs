using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class StartPanel : MonoBehaviour
{
    [SerializeField] 
    private Button pvButton;
    
    [SerializeField]
    private Button startButton;
    
    [SerializeField]
    private Button introductionButton;
    
    [SerializeField]
    private Button quitButton;
    
    [SerializeField]
    private GameObject introductionPanel;
    
    [SerializeField]
    private VideoPlayer pvPlayer;

    [SerializeField] 
    private GameObject startImage;

    private bool isPauesd;

    private void Awake()
    {
        pvButton.onClick.AddListener((() =>
        {
            isPauesd = !isPauesd;
            if (isPauesd)
            {
                pvPlayer.Pause();
                startImage.SetActive(true);
            }
            else
            {
                pvPlayer.Play();
                startImage.SetActive(false);
            }
        }));
        startButton.onClick.AddListener((() =>
        {
            SceneManager.LoadSceneAsync("Main");
        }));
        
        introductionButton.onClick.AddListener((() =>
        {
            introductionPanel.SetActive(true);
        }));
        
        quitButton.onClick.AddListener((() =>
        {
            Application.Quit();
        }));
    }
}
