using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;




/// <summary>
/// Scoket信息存储
/// </summary>
public struct SocketInfo
{
    public E_SocketType type;
    public string host;
    public int port;
}

public class SocketManager:SingletonMonoBase<SocketManager>
{


    public Dictionary<int, SocketInfo> sockets = new Dictionary<int, SocketInfo>();
    private Dictionary<SocketInfo, SocketExecutor> socketDic = new Dictionary<SocketInfo, SocketExecutor>();
    

    public void Init()
    {

    }

    /// <summary>
    /// 创建socket
    /// </summary>
    /// <param name="info"></param>
    public void CreateSocket(SocketInfo info)
    {
        if (!socketDic.ContainsKey(info))
        {
            GameObject socketObj = new GameObject(info.host + "_" + info.port);
            socketObj.transform.parent = transform;
            SocketExecutor executor = socketObj.AddComponent<SocketExecutor>();
            socketDic.Add(info, executor);
            executor.InitSocket(info);
        }
        else
        {
            Debug.Log("已经存在该Socetk，无需重新创建");
        }
    }

    /// <summary>
    /// 发送信息
    /// </summary>
    /// <param name="info"></param>
    /// <param name="msg"></param>
    public void Send(SocketInfo info,string msg)
    {
        if (socketDic.ContainsKey(info))
        {
            socketDic[info].SendData("msg");
        }
        else
        {
            Debug.Log("ip端口下没有绑定的socket");
        }
    }

    /// <summary>
    /// 释放socket
    /// </summary>
    /// <param name="info"></param>
    public void CloseSocet(SocketInfo info)
    {
        if (socketDic.ContainsKey(info))
        {
            GameObject.Destroy(socketDic[info].gameObject);
            socketDic.Remove(info);
        }
    }


    /// <summary>
    /// 释放所有socket
    /// </summary>
    /// <param name="info"></param>
    public void CloseAllSocet(SocketInfo info)
    {
        if (socketDic.ContainsKey(info))
        {
            foreach (Transform executor in transform)
            {
                Destroy(executor.gameObject);
            }
            socketDic.Clear();
        }
    }
}



public class SocketExecutor : MonoBehaviour
{

    private Thread socketThread, receiveThread;
    private Socket socket;
    SocketInfo socketInfo;

    public void InitSocket(SocketInfo info)
    {
        socketInfo = info;
        socketThread = new Thread(StartSocket);
        socketThread.IsBackground = true;
        socketThread.Start();
    }

    void StartSocket()
    {
        try
        {
            switch (socketInfo.type)
            {
                case E_SocketType.Tcp:
                    socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);
                    socket.Connect(new IPEndPoint(IPAddress.Parse(socketInfo.host), socketInfo.port));
                    break;
                case E_SocketType.Udp:
                    socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, System.Net.Sockets.ProtocolType.Udp);
                    break;
            }
            Debug.Log("Socket connected");
            receiveThread = new Thread(ReceiveData);
            receiveThread.IsBackground = true;
            receiveThread.Start();
        }
        catch (Exception e)
        {
            Debug.LogError("Socket error: " + e.Message);
        }
    }

    void ReceiveData()
    {
        while (socket != null && socket.Connected)
        {
            try
            {
                byte[] buffer = new byte[1024];
                int receivedBytes = socket.Receive(buffer);
                if (receivedBytes > 0)
                {
                    string receivedMessage = Encoding.UTF8.GetString(buffer, 0, receivedBytes);
                    //Debug.Log("Received message: " + receivedMessage);
                    EventsManager.Instance.Dispatch<string>(E_Events.ReceiveSocket,receivedMessage);
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Receive error: " + e.Message);
            }
        }
    }

    public void SendData(string message)
    {
        if (socket != null && socket.Connected)
        {
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                socket.Send(data);
            }
            catch (Exception e)
            {
                Debug.LogError("Send error: " + e.Message);
            }
        }
    }


    /// <summary>
    /// 如果不用对象池来创建
    /// </summary>
    void OnDestroy()
    {
        if (socket != null)
        {
            socket.Close();
        }

        if (socketThread != null)
        {
            socketThread.Abort();
        }

        if (receiveThread != null)
        {
            receiveThread.Abort();
        }
    }
}
