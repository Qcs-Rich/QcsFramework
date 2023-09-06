using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;

public class Test : MonoBehaviour
{
    SocketInfo socketInfo;

    private void Start()
    {

        socketInfo.host = "127.0.0.1";
        socketInfo.port = 8899;
        socketInfo.type = E_SocketType.Tcp;


        MonoManager.Instance.AddEndlistener(Loose);
        EventsManager.Instance.Addlistener<string>(E_Events.ReceiveSocket, Receive);
        //SocketManager.Instance.CreateSocket(socketInfo);
        //SocketManager.Instance.SendMsg(socketInfo, "123123");

    }


    public void Receive(string msg)
    {
        Debug.Log("MSG:" + msg);
    }



    public void Loose()
    {
        //SocketManager.Instance.CloseSocket(socketInfo);
    }


}