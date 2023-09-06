using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfigManager : SingletonBase<ConfigManager>
{
    public void Init()
    {
        //加载出所有配置文件   将信息储存在实体类中






    }


    private void LoadSocketCfg()
    {


    }


    IEnumerator LoadSocketCfgCroutine()
    {
        WWW www = new WWW(Application.streamingAssetsPath + "/SocketCfg.json");
        yield return www;

        SocketInitData data = Information.Instance.GetEntityFromJson<SocketInitData>(www.text);

        for (int i = 0; i < data.socketList.Count; i++)
        {

            SocketInfo info;
            info.host = data.socketList[i].host;
            info.port = data.socketList[i].port;
            if (data.socketList[i].type.ToLower() == "tcp")
            {
                info.type = E_SocketType.Tcp;
            }
            else
            {
                info.type= E_SocketType.Udp;
            }
            SocketManager.Instance.sockets.Add(data.socketList[i].code,info);
        }


    }




}

[Serializable]
public class SocketInitData
{

    public List<data> socketList;

    [Serializable]
    public class data
    {
        public int code;
        public string type;
        public string host;
        public int port;
    }

}