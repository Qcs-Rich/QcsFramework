using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherActions : SingletonBase<OtherActions>
{

    /// <summary>
    /// ����ʱ��������
    /// </summary>
    /// <param name="startTimer"></param>
    /// <param name="endTimer"></param>
    /// <returns></returns>
    public int GetSubSeconds(DateTime startTimer, DateTime endTimer)
    {
        TimeSpan startSpan = new TimeSpan(startTimer.Ticks);
        TimeSpan nowSpan = new TimeSpan(endTimer.Ticks);
        TimeSpan subTimer = nowSpan.Subtract(startSpan).Duration();
        return (int)subTimer.TotalSeconds;
    }
 
} 
