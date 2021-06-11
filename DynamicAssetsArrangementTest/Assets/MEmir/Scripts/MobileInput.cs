using System.Collections.Generic;
using UnityEngine;

namespace MEmir.Mobile
{
    public class MobileInput : MonoBehaviour
    {
        public static MobileInput Instance { private set; get; }
        [SerializeField] private List<Joystick> joys;

        private void Awake()
        {
            if (Instance)
            {
                Debug.LogError("MobileInput: Multipile Instances Detected!");
                return;
            }
            Instance = this;
        }

        public bool IsJoyAvaliable(int joyID, out Vector2 joyOutput)
        {
            if (joys[joyID])
            {
                joyOutput = joys[joyID].GetOutput();
                return true;
            }
            else
            {
                joyOutput = Vector2.zero;
                return false;
            }
        }
    }
}