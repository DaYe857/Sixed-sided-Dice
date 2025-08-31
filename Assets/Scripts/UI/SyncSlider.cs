using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class SyncSlider : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnSliderValueChanged))] // ͬ������ + ֵ�仯����
    private float syncSliderValue;

    private Slider slider; // ��UI�������

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        CmdUpdateSlider(0f);
    }

    // Hook������ͬ�������仯ʱ����
    private void OnSliderValueChanged(float oldValue, float newValue) {
        slider.value = newValue; // ���пͻ��˸��»���UI
    }

    // �ͻ����޸Ļ���ʱ���ã�������Ҳ�����
    public void UpdateSliderValue(float value) {
        if(!isLocalPlayer){return;}
        CmdUpdateSlider(value); // ������������޸�����
    }

    [Command(requiresAuthority = false)] // �ͻ��� -> ������
    private void CmdUpdateSlider(float value) {
        syncSliderValue = value; // �������޸�ͬ������
    }
}
