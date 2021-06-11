using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace MEmir.Managers.Download
{
    public class DownloadManager : MonoBehaviour
    {
        public static DownloadManager Instance { get; private set; }

        private void Awake()
        {
            if(Instance)
            {
                Debug.LogError("DownloadManager: Multipile Instances Detected!");
                return;
            }
            Instance = this;
        }

        public void DownloadTextures(string[] urls, Action<Texture2D[]> Callback)
        {
            StartCoroutine(DownloadTexturesCoroutine(urls, Callback));
        }

        private IEnumerator DownloadTexturesCoroutine(string[] urls, Action<Texture2D[]> result)
        {
            Texture2D[] textures = new Texture2D[urls.Length];

            for(int i = 0; i < textures.Length; i++)
            {
                using (UnityWebRequest webUnityRequrest = UnityWebRequestTexture.GetTexture(urls[i]))
                {
                    yield return webUnityRequrest.SendWebRequest();

                    if(!webUnityRequrest.isNetworkError || !webUnityRequrest.isHttpError)
                    {
                        DownloadHandlerTexture dhs = webUnityRequrest.downloadHandler as DownloadHandlerTexture;
                        textures[i] = dhs.texture;
                    }
                }
            }

            result?.Invoke(textures);
        }
    }
}