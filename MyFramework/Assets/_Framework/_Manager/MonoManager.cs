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
    /// ���ⲿͨ����������Э��
    /// </summary>
    public Coroutine StartCoroutine(IEnumerator routine)
    {
        return MonoExecuter.StartCoroutine(routine);
    }

    /// <summary>
    /// ���ⲿͨ������ֹͣЭ��
    /// </summary>
    public void StopCoroutine(IEnumerator routine)
    {
        if (routine != null)
            MonoExecuter.StopCoroutine(routine);
    }

    /// <summary>
    /// ���ⲿͨ������ֹͣЭ��
    /// </summary>
    public void StopCoroutine(Coroutine routine)
    {
        if (routine != null)
            MonoExecuter.StopCoroutine(routine);
    }

    /// <summary>
    /// ���ⲿͨ������ֹͣ����Э��
    /// </summary>
    public void StopAllCoroutines()
    {
        MonoExecuter.StopAllCoroutines();
    }



    /// <summary>
    /// ���Update�¼�
    /// </summary>
    public void AddUpdate1SListener(Action call)
    {
        MonoExecuter.AddUpdate1SListener(call);
    }

    /// <summary>
    /// ���Update�¼�
    /// </summary>
    public void AddUpdateListener(Action call)
    {
        MonoExecuter.AddUpdateListener(call);
    }

    /// <summary>
    /// �Ƴ�Update�¼�
    /// </summary>
    public void RemoveUpdateListener(Action call)
    {
        MonoExecuter.RemoveUpdateListener(call);
    }

    /// <summary>
    /// �Ƴ�����Update�¼�
    /// </summary>
    public void RemoveAllUpdateListeners()
    {
        MonoExecuter.RemoveAllUpdateListeners();
    }

    /// <summary>
    /// ���FixedUpdate�¼�
    /// </summary>
    public void AddFixedUpdateListener(Action call)
    {
        MonoExecuter.AddFixedUpdateListener(call);
    }

    /// <summary>
    /// �Ƴ�FixedUpdate�¼�
    /// </summary>
    public void RemoveFixedUpdateListener(Action call)
    {
        MonoExecuter.RemoveFixedUpdateLinstener(call);
    }

    /// <summary>
    /// �Ƴ�����FixedUpdate�¼�
    /// </summary>
    public void RemoveAllFixedUpdateListeners()
    {
        MonoExecuter.RemoveAllFixedUpdateListeners();
    }


    /// <summary>
    /// �Ƴ�FixedUpdate��Update��LateUpdate�������¼�
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
                case PlayModeStateChange.EnteredPlayMode://����ʱ��������
                    playEditerEvent?.Invoke();
                    break;
                case PlayModeStateChange.ExitingPlayMode://ֹͣ������������
                    endEditorEvent?.Invoke();
                    break;
            }
        }
#endif

    }
}
