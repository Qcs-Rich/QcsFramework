using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// �������̿���
/// </summary>
public class AppController : SingletonMonoBase<AppController>
{

    public void Init()
    {


        //��ʼ������������
        SocketManager.Instance.Init();

        


    }



}
