using System.Collections.Generic;
using UnityEngine;

namespace MEmir.Datas
{
    [System.Serializable]
    public class TheJsonData
    {
        public List<string> URL;
        public List<Position> positions;

        public TheJsonData() { URL = new List<string>(); positions = new List<Position>(); }
    }

    [System.Serializable]
    public struct Position
    {
        public string prefab;
        public Vector3 position;
        public Quaternion rotation;
    }
}