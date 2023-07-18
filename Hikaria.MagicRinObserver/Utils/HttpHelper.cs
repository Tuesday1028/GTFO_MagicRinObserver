using UnityEngine.Networking;
using System.Collections;
using BepInEx.Unity.IL2CPP.Utils;
using UnityEngine;

namespace MagicRinObserver.Utils
{
    internal static class HttpHelper
    {
        public static void Get(string url, Action<UnityWebRequest> OnSuccess, Action<UnityWebRequest> OnFailed = null)
        {
            if (CoroutineManager.Current == null)
            {
                Logs.LogError("CoroutineManager 为空!");
                return;
            }
            CoroutineManager.Current.StartCoroutine(getFromURL(url, OnSuccess, OnFailed));
        }

        private static IEnumerator getFromURL(string url, Action<UnityWebRequest> OnSuccess, Action<UnityWebRequest> OnFailed = null)
        {
            UnityWebRequest request = UnityWebRequest.Get(url);
            request.timeout = 10;
            yield return request.SendWebRequest();

            if (!request.isDone || request.isHttpError)
            {
                OnFailed?.Invoke(request);
                request.Dispose();
                yield break;
            }
            OnSuccess(request);
            request.Dispose();
        }
    }
}
