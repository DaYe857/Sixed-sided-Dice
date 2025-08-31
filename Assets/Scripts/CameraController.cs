using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target { get; set; } // Ŀ������
    public Transform gravitySource{ get; set; } // ����Դ�����������ģ�
    public float distance = 2f; // �������Ŀ��ľ���
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

        // ����Ŀ������������������ָ������Դ��
        Vector3 gravityDir = (target.position - gravitySource.up).normalized;

        // ���������Up�������������෴
        Vector3 cameraUp = -gravityDir;

        // ���������λ�ã�Ŀ��λ�� + ������������ * ����
        Vector3 targetPosition = target.position + cameraUp * distance;
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

        // ��ת�������LookAtĿ�꣬���Է�������ΪUp��
        transform.LookAt(target.position, cameraUp);*/
    }
}
