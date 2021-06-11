using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using MEmir.Datas.SceneGenerator;

namespace MEmir.SceneGenerator
{
    public class SceneGenerator : MonoBehaviour
    {
        [SerializeField] private SceneDataSO sceneData;

        private void Awake()
        {
            GenerateScene(sceneData);
        }

        public void GenerateScene(SceneDataSO data)
        {
            SGObject[] objects = data.GetObjects();
            List<Transform> instantiatedObjects = new List<Transform>();
            for (int i = 0; i < objects.Length; i++)
            {
                SGObject sgObject = objects[i];
                Transform instantiatedObject = null;
                if(sgObject.Prefab)
                {
                    instantiatedObject = Instantiate(sgObject.Prefab).transform;
                }
                else
                {
                    instantiatedObject = new GameObject(sgObject.Name).transform;
                }
                instantiatedObject.name = sgObject.Name;
                instantiatedObject.position = sgObject.Pos;
                instantiatedObject.rotation = sgObject.Rot;
                if(sgObject.HasParent())
                {
                    instantiatedObject.SetParent(instantiatedObjects[sgObject.ParentID], true);
                }
                instantiatedObjects.Add(instantiatedObject);
            }
        }
    }
}