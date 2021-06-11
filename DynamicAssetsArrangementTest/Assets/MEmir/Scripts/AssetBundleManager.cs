using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEmir.Managers.DataManagement;

namespace MEmir.Datas.AssetBundle
{
    public class AssetBundleManager : MonoBehaviour
    {
        private void Start()
        {
            AssetData data = DataManager.Instance.GetData();
            SpawnPrefabs(data);
        }

        public void SpawnPrefabs(AssetData data)
        {
            for (int i = 0; i < data.data.positions.Count; i++)
            {
                string prefabName = data.data.positions[i].prefab;
                Vector3 pos = data.data.positions[i].position;
                Quaternion rot = data.data.positions[i].rotation;
                GameObject prefab = data.bundle.LoadAsset<GameObject>(prefabName).gameObject;

                Transform transform = Instantiate(prefab, pos, rot).transform;
            }
        }
    }
}