using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target { get; set; } // 目标物体
    public Transform gravitySource{ get; set; } // 重力源（如星球中心）
    public float distance = 2f; // 摄像机与目标的距离
    public float smoothSpeed = 0.3f;

    public void InitCamera(Transform target,Transform gravitySource)
    {
        this.target = target;
        this.gravitySource = gravitySource;
    }

    void LateUpdate()
    {
        transform.LookAt(target.forward);
        /*if (target == null || gravitySource == null) return;

        // 计算目标物体所受重力方向（指向重力源）
        Vector3 gravityDir = (target.position - gravitySource.up).normalized;

        // 设置摄像机Up轴与重力方向相反
        Vector3 cameraUp = -gravityDir;

        // 计算摄像机位置：目标位置 + 反向重力方向 * 距离
        Vector3 targetPosition = target.position + cameraUp * distance;
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

        // 旋转摄像机：LookAt目标，并以反向重力为Up轴
        transform.LookAt(target.position, cameraUp);*/
    }
}
