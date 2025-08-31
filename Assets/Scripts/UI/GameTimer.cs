using UnityEngine;
using System;
using Mirror;
using UnityEngine.UI;

/// <summary>
/// ��Ϸ��ʱ�������࣬���ڹ�����Ϸ�е�ʱ�䡣
/// </summary>
public class GameTimer : MonoBehaviour
{
    public float gameTime;
    private Text timelineText;
    private float currentTime = 0f; // ��ǰ������ʱ��
    private bool isRunning = false; // ��ʱ���Ƿ���������
    private float pauseTime = 0f;   // ��ͣʱ��ʱ��
    
    public string GameLeftTime => FormatTime(gameTime-currentTime);
    bool isPasued=false;
    bool isPlay = false;

    private void Awake()
    {
        timelineText = transform.GetChild(0).GetComponent<Text>();
    }

    public void Start()
    {
        StartTimer();
    }

    /// <summary>
    /// ��ʼ��ʱ����
    /// </summary>
    public void StartTimer()
    {
        isPasued = false;
        isRunning = true;
    }
    /// <summary>
    /// ��ͣ��ʱ����
    /// </summary>
    public void PauseTimer()
    {
        isRunning = false;
        isPasued = true;
        pauseTime = currentTime;
    }

    /*public void ResumeTimer()
    {
        if (isPasued)
        {
            isRunning = true;
            isPasued = false;
            currentTime = pauseTime;
        }
    }*/

    private void end( )
    {   
        isPasued=true;
        isRunning = false;
    }

    /// <summary>
    /// Unity�������ڷ�����ÿ֡����һ�Ρ�
    /// </summary>
    private void Update()
    {
        if (isRunning)
        {
            currentTime += Time.deltaTime;
            timelineText.text = GameLeftTime;
        }
        else
        {
            if (isPasued)
            {
                PauseTimer();
            }
        }

        if ((gameTime - currentTime) < 5f && isPlay == false)
        {
            isPlay = true;
        }

        if (currentTime > gameTime) 
        {
            //EventHandler.LoseGame();
            currentTime = 0;
            Debug.Log(1);
        }
    }


    /// <summary>
    /// ��������ʱ�䣨����Ϊ��λ����ʽ��Ϊ����:�롱��ʽ��
    /// </summary>
    /// <param name="timeInSeconds">Ҫ��ʽ����ʱ�䣬����Ϊ��λ��</param>
    /// <returns>��ʽ�����ʱ���ַ�����</returns>
    private string FormatTime(float timeInSeconds)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(timeInSeconds);
        return string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
    }

    public string GetLeftTime() => FormatTime(currentTime);
    public void SetGameTime(float time) => gameTime = time;
}