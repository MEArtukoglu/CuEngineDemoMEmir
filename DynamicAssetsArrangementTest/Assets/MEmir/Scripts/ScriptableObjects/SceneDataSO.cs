using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using MEmir.MExtensions;
using UnityEngine.SceneManagement;
using UnityEditor.Experimental.SceneManagement;

namespace MEmir.Datas.SceneGenerator
{
    [CreateAssetMenu(fileName = "SceneData", menuName = "ScrtipableObjects/SceneData")]
    public class SceneDataSO : ScriptableObject
    {
        [SerializeField] private List<SGObject> objects;
        public SGObject[] GetObjects() => objects.ToArray();

        

        public void GetSceneObjects()
        {
            objects = new List<SGObject>();
            List<Transform> allSceneObjects = SceneManager.GetActiveScene().GetAllSceneTransforms();
            List<Transform> usedSceneObjects = new List<Transform>();
            for (int i = 0; i < allSceneObjects.Count; i++)
            {
                int parentID = allSceneObjects[i].parent ? allSceneObjects.IndexOf(allSceneObjects[i].parent) : -1;
                
                if (parentID != -1)
                {
                    if(PrefabUtility.IsPartOfRegularPrefab(allSceneObjects[i])
                        && PrefabUtility.IsPartOfRegularPrefab(allSceneObjects[parentID]))
                    {
                        continue;
                    }
                }
                
                objects.Add(new SGObject(allSceneObjects[i], (parentID != -1)  ? usedSceneObjects.IndexOf(allSceneObjects[i].parent) : -1));
                usedSceneObjects.Add(allSceneObjects[i]);
            }
        }

        
    }

    [CustomEditor(typeof(SceneDataSO))]
    public class SceneDataSOEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            SceneDataSO dataSO = (SceneDataSO)target;
            EditorUtility.SetDirty(target);

            if(GUILayout.Button("Get Scene Objects"))
            {
                dataSO.GetSceneObjects();
            }
        }
    }

    [System.Serializable]
    public struct SGObject
    {
        public string Name;
        public GameObject Prefab;
        public int ParentID;
        public Vector3 Pos;
        public Vector3 LocalScale;
        public Quaternion Rot;
        public bool HasParent() => (ParentID==-1) ? false : true;

        public SGObject(Transform instance, int parentID = -1)
        {
            
            if (PrefabUtility.IsPartOfRegularPrefab(instance))
            {
                Prefab = PrefabUtility.GetCorrespondingObjectFromOriginalSource(instance)?.gameObject;
            }
            else
            {
                Prefab = PrefabUtility.GetCorrespondingObjectFromSource(instance)?.gameObject;
            }

            ParentID = parentID;
            Name = instance.name;
            Pos = instance.position;
            Rot = instance.rotation;
            LocalScale = instance.localScale;
        }
    }
}