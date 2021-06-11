using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MEmir.UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }
        [SerializeField] private List<Panel> panels;

        
        private void Awake()
        {
            if(Instance)
            {
                Debug.LogError("UIManager: Multipile Instances Detected!");
                return;
            }
            Instance = this;
        }

        public void SwitchPanel(int ID)
        {
            for (int i = 0; i < panels.Count; i++)
            {
                panels[i].GetPanel().gameObject.SetActive((i == ID) ? true : false);
            }
            panels[ID].TriggerEvents();
        }
    
        [System.Serializable]
        struct Panel
        {
            [SerializeField] private RectTransform panel;
            [SerializeField] private List<UnityEvent> events;

            public RectTransform GetPanel() => panel;
            
            public void TriggerEvents()
            {
                foreach (UnityEvent _event in events)
                {
                    _event?.Invoke();
                }
            }
        }
    }
}
