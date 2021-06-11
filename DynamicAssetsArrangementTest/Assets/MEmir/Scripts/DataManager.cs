using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEmir.Datas;
using System.IO;
using MEmir.Managers.Download;

namespace MEmir.Managers.DataManagement
{
    public class DataManager : MonoBehaviour
    {
        public static DataManager Instance { get; private set; }
        [SerializeField] private AssetData AssetData;
        public AssetData GetData() => AssetData;

        private void Awake()
        {
            if(Instance)
            {
                Debug.LogError("AssetBundleManager: Multipile Instances Detected!");
                return;
            }
            Instance = this;
            AssetData.ParseDataFromJson("json-for-test.json");
            AssetData.LoadAssetBundle("testbundle");
        }
    }

    [System.Serializable]
    public class AssetData
    {
        public TheJsonData data;
        public AssetBundle bundle;

        public void ParseDataFromJson(string fileName)
        {
            string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
            if (File.Exists(filePath))
            {
                string dataAsJson = File.ReadAllText(filePath);
                data = JsonUtility.FromJson<AssetData>(dataAsJson).data;
            }
            else
                Debug.LogError("AssetData: Couldn't find file at " + filePath);
        }

        public void LoadAssetBundle(string fileName)
        {
            bundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, fileName));
        }
        
    }
}