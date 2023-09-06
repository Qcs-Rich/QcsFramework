using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// 整体流程控制
/// </summary>
public class AppController : SingletonMonoBase<AppController>
{

    public void Init()
    {


        //初始化各个管理器
        SocketManager.Instance.Init();

        


    }



}
