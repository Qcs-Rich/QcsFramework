using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// </summary>
/// http请求管理器
/// </summary>
public class HttpManager : SingletonBase<HttpManager>
{

    private static HttpExecuter executer;
    private static HttpExecuter Executer
    {
        get
        {
            if (executer == null)
            {
                executer = GameObject.FindObjectOfType<HttpExecuter>();
                if (executer == null)
                {
                    executer = new GameObject("HttpExcauter").AddComponent<HttpExecuter>();
                }
            }
            return executer;
        }
    }

    public void Get(string url, Dictionary<string, string> headers, Action<string> onSuccess, Action<string> onError = null)
    {
        Executer.Get(url, headers, onSuccess, onError);
    }


    public void Post(string url, string body, Dictionary<string, string> headers, Action<string> onSuccess, Action<string> onError = null)
    {
        Executer.Post(url, body, headers, onSuccess, onError);
    }


    /// <summary>
    /// Http执行器,继承mono
    /// </summary>
    protected class HttpExecuter : MonoBehaviour
    {

        Queue<IEnumerator> requestQueue = new Queue<IEnumerator>();
        bool isProcessing;

        public void Get(string url, Dictionary<string, string> headers, Action<string> onSuccess, Action<string> onError = null)
        {
            var request = UnityWebRequest.Get(url);
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.SetRequestHeader(header.Key, header.Value);
                }
            }
            requestQueue.Enqueue(ProcessRequest(request, onSuccess, onError));
            ProcessQueue();
        }

        public void Post(string url, string body, Dictionary<string, string> headers, Action<string> onSuccess, Action<string> onError = null)
        {
            var request = UnityWebRequest.Post(url, body);
            byte[] badyRaw = System.Text.Encoding.UTF8.GetBytes(body);

            request.uploadHandler = new UploadHandlerRaw(badyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.SetRequestHeader(header.Key, header.Value);
                }
            }
            requestQueue.Enqueue(ProcessRequest(request, onSuccess, onError));
            ProcessQueue();
        }

        public IEnumerator ProcessRequest(UnityWebRequest request, Action<string> onSuccess, Action<string> onError = null)
        {
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                onError?.Invoke(request.error);
            }
            else
            {
                onSuccess?.Invoke(request.downloadHandler.text);
            }
            isProcessing = false;
            ProcessQueue();
        }

        private void ProcessQueue()
        {
            if (!isProcessing && requestQueue.Count > 0)
            {
                isProcessing = true;
                MonoManager.Instance.StartCoroutine(requestQueue.Dequeue());
            }
        }
    }
}