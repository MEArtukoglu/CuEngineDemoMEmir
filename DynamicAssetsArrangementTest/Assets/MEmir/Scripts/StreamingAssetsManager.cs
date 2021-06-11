using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace MEmir.Managers.StreamingAssets
{
    public class StreamingAssetsManager : MonoBehaviour
    {
        public static StreamingAssetsManager Instance { get; private set; }


        private void Awake()
        {
            if(Instance)
            {
                Debug.LogError("Multipile StreamingAssetsManager Detected!");
                return;
            }
            Instance = this;
        }

    }
}
