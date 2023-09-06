using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : SingletonBase<UIManager>
{
    protected UIManager() { }

    private static UIController executer;
    private static UIController Executer
    {
        get
        {
            //if (executer == null)
            //{
            //    executer = GameObject.FindObjectOfType<UIController>();
            //    if (executer == null)
            //    {
            //        executer = new GameObject(typeof(UIController).Name).AddComponent<UIController>();
            //    }
            //}
            executer = UIController.Instance;
            return executer;
        }
    }


    public void Init()
    {
        //��������
        Executer.CreateOverlayCanvas();

        //����EventSystem
        Executer.CreateEventSystem();
    }


    /// <summary>
    /// Canvas  
    /// </summary>
    protected class UIController : SingletonMonoBase<UIController>
    {

        private Transform rear;
        public Transform Rear
        {
            get
            {
                if (rear == null)
                {
                    rear = transform.Find("Rear");
                }
                return rear;
            }
        }

        private Transform middle;
        public Transform Middle
        {
            get
            {
                if (middle == null)
                {
                    middle = transform.Find("Middle");
                }
                return middle;
            }
        }

        private Transform front;
        public Transform Front
        {
            get
            {
                if (front == null)
                {
                    front = transform.Find("Front");
                }
                return front;
            }
        }
        Dictionary<GameObject, GameObject> panelDic = new Dictionary<GameObject, GameObject>();


        /// <summary>
        /// ���� Cancas
        /// </summary>
        public void CreateOverlayCanvas()
        {
            gameObject.layer = LayerMask.NameToLayer("UI");
            //�������Canvas
            Canvas canvas = gameObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 100;

            //CanvasScaler

            CanvasScaler scaler = gameObject.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(Screen.width, Screen.height);
            scaler.matchWidthOrHeight = Screen.width > Screen.height ? 1 : 0;

            //Graphic Raycaster
            GraphicRaycaster raycaster = gameObject.AddComponent<GraphicRaycaster>();

            //��������� ��Ϊ��ʾ��ĸ�����
            GameObject rear = new GameObject("Rear");
            rear.transform.SetParent(transform, false);

            GameObject middle = new GameObject("Middle");
            middle.transform.SetParent(transform, false);

            GameObject front = new GameObject("Front");
            front.transform.SetParent(transform, false);
        }

        public void CreateEventSystem()
        {
            if (GameObject.FindObjectOfType<EventSystem>())
                return;
            GameObject eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();
            DontDestroyOnLoad(eventSystem);
        }

        public void ShowPanel(GameObject panelPrefab, E_PanelDisPlayer layer = E_PanelDisPlayer.Middle)
        {
            //ȷ������ж�Ӧ��Ԥ����
            if (panelPrefab != null)
            {
                print("-----Prefab is Null-----");
                return;
            }
            //�����ǰ�Ѿ���ʾ�����
            if (panelDic.ContainsKey(panelPrefab))
            {
                return;
            }
            //����ָ�����������
            GameObject panel = Instantiate(panelPrefab);
            panel.name = panelPrefab.name;
            panelDic.Add(panelPrefab, panel);
            Transform parent = null;

            switch (layer)
            {
                case E_PanelDisPlayer.Rear:
                    parent = Rear;
                    break;
                case E_PanelDisPlayer.Middle:
                    parent = Middle;
                    break;
                case E_PanelDisPlayer.Front:
                    parent = Front;
                    break;
                default:
                    break;
            }
            //����λ��
            panel.transform.SetParent(transform, false);
            panel.transform.SetParent(parent);
        }



        /// <summary>
        /// �������
        /// </summary>
        /// <param name="panelPrefab"></param>
        public void HidePanel(GameObject panelPrefab)
        {
            //���Ҫ���ص����Ϊnull����������Ч��
            if (panelPrefab == null)
            {
                return;
            }
            //���Ҫ���ص���岻���ڣ�ֱ�ӷ��ء�
            if (!panelDic.ContainsKey(panelPrefab))
            {
                return;
            }
            Destroy(panelDic[panelPrefab]);
            panelDic.Remove(panelPrefab);
        }


        /// <summary>
        /// �������
        /// </summary>
        /// <param name="panelPrefabName"></param>
        public void HidePanel(string panelPrefabName)
        {
            foreach (var item in panelDic.Keys)
            {
                if (item.name == panelPrefabName)
                {
                    HidePanel(item);
                    return;
                }
            }
        }

        /// <summary>
        /// ����Ƿ���ʾ
        /// </summary>
        public bool IsPanelShowed(GameObject panelPrefab)
        {
            if (panelPrefab == null)
            {
                return false;
            }

            return panelDic.ContainsKey(panelPrefab);
        }

        /// <summary>
        /// ����Ƿ���ʾ
        /// </summary>
        public bool IsPanelShowed(string panelPrefabName)
        {
            foreach (var item in panelDic.Keys)
            {
                if (item.name == panelPrefabName)
                    return true;
            }
            return false;
        }
    }
}
