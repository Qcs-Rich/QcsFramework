using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

public class MonoManager : SingletonBase<MonoManager>
{

    private MonoManager() { }

    /// <summary>
    /// MonoContrller
    /// </summary>
    private MonoController monoExecuter;
    private MonoController MonoExecuter
    {
        get
        {
            if (monoExecuter == null)
            {
                GameObject go = new GameObject(typeof(MonoController).Name);
                monoExecuter = go.AddComponent<MonoController>();
            }
            return monoExecuter;
        }
    }



    /// <summary>
    /// 让外部通过它来开启协程
    /// </summary>
    public Coroutine StartCoroutine(IEnumerator routine)
    {
        return MonoExecuter.StartCoroutine(routine);
    }

    /// <summary>
    /// 让外部通过它来停止协程
    /// </summary>
    public void StopCoroutine(IEnumerator routine)
    {
        if (routine != null)
            MonoExecuter.StopCoroutine(routine);
    }

    /// <summary>
    /// 让外部通过它来停止协程
    /// </summary>
    public void StopCoroutine(Coroutine routine)
    {
        if (routine != null)
            MonoExecuter.StopCoroutine(routine);
    }

    /// <summary>
    /// 让外部通过它来停止所有协程
    /// </summary>
    public void StopAllCoroutines()
    {
        MonoExecuter.StopAllCoroutines();
    }



    /// <summary>
    /// 添加Update事件
    /// </summary>
    public void AddUpdate1SListener(Action call)
    {
        MonoExecuter.AddUpdate1SListener(call);
    }

    /// <summary>
    /// 添加Update事件
    /// </summary>
    public void AddUpdateListener(Action call)
    {
        MonoExecuter.AddUpdateListener(call);
    }

    /// <summary>
    /// 移除Update事件
    /// </summary>
    public void RemoveUpdateListener(Action call)
    {
        MonoExecuter.RemoveUpdateListener(call);
    }

    /// <summary>
    /// 移除所有Update事件
    /// </summary>
    public void RemoveAllUpdateListeners()
    {
        MonoExecuter.RemoveAllUpdateListeners();
    }

    /// <summary>
    /// 添加FixedUpdate事件
    /// </summary>
    public void AddFixedUpdateListener(Action call)
    {
        MonoExecuter.AddFixedUpdateListener(call);
    }

    /// <summary>
    /// 移除FixedUpdate事件
    /// </summary>
    public void RemoveFixedUpdateListener(Action call)
    {
        MonoExecuter.RemoveFixedUpdateLinstener(call);
    }

    /// <summary>
    /// 移除所有FixedUpdate事件
    /// </summary>
    public void RemoveAllFixedUpdateListeners()
    {
        MonoExecuter.RemoveAllFixedUpdateListeners();
    }


    /// <summary>
    /// 移除FixedUpdate、Update、LateUpdate的所有事件
    /// </summary>
    public void RemoveAllListeners()
    {
        MonoExecuter.RemoveAllListeners();
    }


    public void AddQuitlistener(Action call)
    {
        MonoExecuter.AddQuitlistener( call);
    }

    public void RemoveQuitlistener(Action call)
    {
        MonoExecuter.RemoveQuitlistener(call);
    }


#if UNITY_EDITOR


    public void AddPlaylistener(Action call)
    {
        MonoExecuter.AddPlaylistener(call);
    }

    public void AddEndlistener(Action call)
    {
        MonoExecuter.AddEndlistener(call);
    }



#endif








    public class MonoController : MonoBehaviour
    {
        event Action updateEvent;
        event Action fixedUpdateEvent;
        event Action update1sEvent;
        event Action quitEvent;
        event Action endEditorEvent;
        event Action playEditerEvent;

        float timer = 0;



        private void Start()
        {
            EditorApplication.playModeStateChanged += EditorState;
        }
        private void Update()
        {
            updateEvent?.Invoke();
            timer += Time.deltaTime;

            if (timer >= 1)
            {
                timer = 0;
                update1sEvent?.Invoke();
            }
        }


        public void AddUpdate1SListener(Action call)
        {
            update1sEvent += call;
        }


        private void FixedUpdate()
        {
            fixedUpdateEvent?.Invoke();
        }

        public void AddUpdateListener(Action call)
        {
            updateEvent += call;
        }

        public void RemoveUpdateListener(Action call)
        {
            updateEvent -= call;
        }

        public void RemoveAllUpdateListeners()
        {
            updateEvent = null;
        }

        public void AddFixedUpdateListener(Action call)
        {
            fixedUpdateEvent += call;
        }

        public void RemoveFixedUpdateLinstener(Action call)
        {
            fixedUpdateEvent -= call;
        }

        public void RemoveAllFixedUpdateListeners()
        {
            fixedUpdateEvent = null;
        }

        public void RemoveAllListeners()
        {
            RemoveAllFixedUpdateListeners();
            RemoveAllUpdateListeners();
        }

        private void OnApplicationQuit()
        {
            quitEvent?.Invoke();
        }


        public void AddQuitlistener(Action call)
        {
            quitEvent += call;
        }

        public void RemoveQuitlistener(Action call)
        {
            quitEvent -= call;
        }


#if UNITY_EDITOR

        public void AddPlaylistener(Action call)
        {
            playEditerEvent += call;
        }

        public void AddEndlistener(Action call)
        {
            endEditorEvent += call;
        }


        private void EditorState(PlayModeStateChange obj)
        {
            switch (obj)
            {
                case PlayModeStateChange.EnteredPlayMode://播放时立即监听
                    playEditerEvent?.Invoke();
                    break;
                case PlayModeStateChange.ExitingPlayMode://停止播放立即监听
                    endEditorEvent?.Invoke();
                    break;
            }
        }
#endif

    }
}
