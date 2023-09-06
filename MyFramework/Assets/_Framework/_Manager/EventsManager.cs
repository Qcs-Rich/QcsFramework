using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;



/// <summary>
/// �¼����Ĺ�����
/// </summary>
public class EventsManager : SingletonBase<EventsManager>
{
    //��������  + eventsInfo
    Dictionary<string, IEventInfo> eventsDic = new Dictionary<string, IEventInfo>();

    /// <summary>
    /// ����¼�
    /// </summary>
    public void Addlistener(object command, Action call)
    {
        string key = command.ToString();
        if (eventsDic.ContainsKey(key))
        {
            (eventsDic[key] as EventInfo).action += call;
        }
        else
        {
            eventsDic.Add(key, new EventInfo(call));
        }
    }

    public void Addlistener<T>(object command, Action<T> call)
    {
        string key = command.ToString() + "WithArgs";
        if (eventsDic.ContainsKey(key))
        {
            (eventsDic[key] as EventInfo<T>).action += call;
        }
        else
        {
            eventsDic.Add(key, new EventInfo<T>(call));
        }
    }


    /// <summary>
    /// ��������
    /// </summary>
    /// <param name="command"></param>
    public void Dispatch(object command)
    {
        string key = command.ToString();
        if (eventsDic.ContainsKey(key))
        {
            (eventsDic[key] as EventInfo).action?.Invoke();
        }
    }
    public void Dispatch<T>(object command,T parameter)
    {
        string key = command.ToString() + "WithArgs";
        if (eventsDic.ContainsKey(key))
        {
            (eventsDic[key] as EventInfo<T>).action?.Invoke(parameter);
        }
    }



    /// <summary>
    /// �Ƴ��¼�
    /// </summary>
    public void RemoveListener(object commond, Action call)
    {
        string key = commond.ToString();
        if (eventsDic.ContainsKey(key))
            (eventsDic[key] as EventInfo).action -= call;
    }
    public void RemoveListener<T>(object commond, Action<T> call)
    {
        string key = commond.ToString() + "WithArgs";
        if (eventsDic.ContainsKey(key))
        {
            (eventsDic[key] as EventInfo<T>).action -= call;
        }
    }

    /// <summary>
    /// �Ƴ�һ������������¼�
    /// </summary>
    public void RemoveListeners(object command)
    {
        string key = command.ToString();
        if (eventsDic.ContainsKey(key))
        {
            (eventsDic[key] as EventInfo).action = null;
        }
    }

    public void RemoveListeners<T>(object command)
    {
        string key = command.ToString() + "WithArgs";
        if (eventsDic.ContainsKey(key))
        {
            (eventsDic[key] as EventInfo<T>).action = null;
        }
    }
 

    public interface IEventInfo
    {

    }

    /// <summary>
    /// �������¼�
    /// </summary>
    /// <typeparam name="T"></typeparam>
    private class EventInfo<T> : IEventInfo
    {
        public Action<T> action;
        public EventInfo(Action<T> call)
        {
            action += call;
        }
    }

    /// <summary>
    /// ���������¼�
    /// </summary>
    private class EventInfo : IEventInfo
    {
        public Action action;
        public EventInfo(Action call)
        {
            action += call;
        }
    }

    
}
