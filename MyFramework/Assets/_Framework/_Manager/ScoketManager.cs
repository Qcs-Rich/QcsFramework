using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;



public class SocketManager : SingletonBase<SocketManager>
{

    private Dictionary<SocketInfo, SocketExecuter> socketDic = new Dictionary<SocketInfo, SocketExecuter>();

    public void CreateSocket(SocketInfo info)
    {
        SocketExecuter socket;
        if (socketDic.ContainsKey(info))
        {
            socket = socketDic[info];
        }
        else
        {
            socket = new SocketExecuter();
        }
        socket.StartConnection(info);
    }

    public void CloseSocket(SocketInfo info)
    {
        if (socketDic.ContainsKey(info))
        {
            socketDic[info].StopConnection();
        }
    }

    public void SendMsg(SocketInfo info, string msg)
    {
        if (socketDic.ContainsKey(info))
        {
            socketDic[info].Send(msg);
        }
    }








    public class SocketExecuter
    {

        private Socket socket;
        private Thread receiveThread;
        private readonly object receivedDataLock = new object();
        private string receivedData;

        public void StartConnection(SocketInfo info)
        {

            IPAddress ipAddress = IPAddress.Parse(info.host);
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, info.port);

            switch (info.type)
            {
                case E_SocketType.Tcp:
                    socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                    socket.Connect(remoteEP);
                    break;
                case E_SocketType.Udp:
                    socket = new Socket(ipAddress.AddressFamily, SocketType.Dgram, ProtocolType.Udp);
                    socket.Connect(remoteEP);
                    break;
            }

            receiveThread = new Thread(Receive);
            receiveThread.IsBackground = true;
            receiveThread.Start();
        }

        public void StopConnection()
        {
            receiveThread?.Abort();
            socket?.Close();
        }

        private void Receive()
        {
            while (true)
            {
                try
                {
                    byte[] buffer = new byte[1024];
                    int bytesReceived = socket.Receive(buffer);
                    string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesReceived);

                    lock (receivedDataLock)
                    {
                        receivedData = receivedMessage;
                        if (receivedData != null)
                        {
                            EventsManager.Instance.Dispatch<string>(E_Events.ReceiveSocket, receivedData);
                            receivedData = null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Error: {ex.Message}");
                }
            }
        }

        public void Send(string message)
        {
            try
            {
                byte[] msg = Encoding.UTF8.GetBytes(message);
                socket.Send(msg);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error: {ex.Message}");
            }
        }
    }
}
