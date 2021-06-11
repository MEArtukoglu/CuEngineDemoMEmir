using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MEmir.MExtensions
{
    public static class Extensions
    {
        public static List<Transform> GetAllChildren(this Transform parent, List<Transform> transformList = null)
        {
            if (transformList == null) transformList = new List<Transform>();

            foreach (Transform child in parent)
            {
                transformList.Add(child);
                child.GetAllChildren(transformList);
            }
            return transformList;
        }

        public static List<Transform> GetAllSceneTransforms(this Scene scene)
        {
            List<Transform> allObjects = new List<Transform>();
            GameObject[] rootObjs = scene.GetRootGameObjects();
            
            for (int i = 0; i < rootObjs.Length; i++)
            {
                allObjects.Add(rootObjs[i].transform);
                List<Transform> childs = rootObjs[i].transform.GetAllChildren();
                foreach (Transform child in childs)
                {
                    allObjects.Add(child);
                }
            }
            return allObjects;
        }
    }
}