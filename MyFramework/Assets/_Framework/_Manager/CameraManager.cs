using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

/// <summary>
/// ���������
/// </summary>
public class CameraManager : SingletonBase<CameraManager>
{

    protected CameraManager() { }

    private static CameraController camera;
    private static CameraController Camera
    {
        get
        {
            if (camera == null)
            {
                camera = GameObject.Find("Main Camera").AddComponent<CameraController>();
            }
            return camera;
        }
    }

    public void LockCamera(bool isLock)
    {
        Camera.LockCamera(isLock);
    }

    public void MoveAndRotateToTarget(Transform target)
    {
        Camera.MoveAndRotateToTarget(target);
    }

    protected class CameraController : MonoBehaviour
    {
        bool isLocked = false;
        Transform target;
        //�ӽ����� 
        public void LockCamera(bool b = true)
        {
            isLocked = b;
        }

        //�ƶ���Ŀ�����꣬���ҳ���һ��
        public void MoveAndRotateToTarget(Transform _target)
        {
            if (!isLocked)
            {
                target = _target;
            }
        }

        void Update()
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, Time.deltaTime * 2);
            transform.localPosition = Vector3.Lerp(transform.localPosition, target.position, Time.deltaTime * 2);
        }
    }
}
