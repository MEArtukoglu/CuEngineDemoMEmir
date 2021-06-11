using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace MEmir.Mobile
{
    public class Joystick : MonoBehaviour, IDragHandler, IEndDragHandler
    {
        [SerializeField] private float maxOffset;
        Vector3 beginPos;

        void Awake()
        {
            beginPos = transform.localPosition;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;

            Vector2 localPos = transform.localPosition;
            localPos.x = Mathf.Clamp(localPos.x, -maxOffset, maxOffset);
            localPos.y = Mathf.Clamp(localPos.y, -maxOffset, maxOffset);
            transform.localPosition = localPos;
        }

        public void OnEndDrag(PointerEventData eventData) => transform.localPosition = beginPos;

        public Vector2 GetOutput() => new Vector2(transform.localPosition.x/maxOffset, transform.localPosition.y/maxOffset);
    }
}