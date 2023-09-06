using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;

public class Test : MonoBehaviour
{

    private void Start()
    {

        SocketInfo socketInfo;
        socketInfo.host = "127.0.0.1";
        socketInfo.port = 6677;
        socketInfo.type = E_SocketType.Tcp;


        SocketManager.Instance.CreateSocket(socketInfo);
        
        EventsManager.Instance.Addlistener<string>(E_Events.ReceiveSocket, Receive);
        MonoManager.Instance.AddEndlistener(Loose);
        SocketManager.Instance.SendMsg(socketInfo,"123123");






    }


    public void Receive(string msg)
    {
        Debug.Log("MSG:" + msg);
    }



    public void Loose()
    {
        //SocketManager.Instance.StopConnection();
    }


}