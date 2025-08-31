using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class SyncSlider : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnSliderValueChanged))] // 同步变量 + 值变化钩子
    private float syncSliderValue;

    private Slider slider; // 绑定UI滑块组件

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        CmdUpdateSlider(0f);
    }

    // Hook函数：同步变量变化时触发
    private void OnSliderValueChanged(float oldValue, float newValue) {
        slider.value = newValue; // 所有客户端更新滑块UI
    }

    // 客户端修改滑块时调用（本地玩家操作）
    public void UpdateSliderValue(float value) {
        if(!isLocalPlayer){return;}
        CmdUpdateSlider(value); // 向服务器发送修改请求
    }

    [Command(requiresAuthority = false)] // 客户端 -> 服务器
    private void CmdUpdateSlider(float value) {
        syncSliderValue = value; // 服务器修改同步变量
    }
}
