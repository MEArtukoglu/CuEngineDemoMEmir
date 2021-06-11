using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MEmir.Cameras
{
    public class OrbitCamera : MonoBehaviour
    {
        [SerializeField] private float minZoom = 35, maxZoom = 45, disFromTarget = 65, zoomSmoothnes = 15;
        [SerializeField] private float rotateSensitivty = 2;
        private float curZoom = 0;
        [SerializeField] private Transform target;

        private void Start()
        {
            transform.position = target.position;
        }

        private void Update()
        {
            Rotation();
            Zoom();
        }

        private void Zoom()
        {
            if (Input.touchCount < 2)
            {
                curZoom += Input.GetAxis("Mouse ScrollWheel") * zoomSmoothnes;
            }
            else
            {
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                float deltaMagDiff = prevTouchDeltaMag - touchDeltaMag;

                curZoom += deltaMagDiff * zoomSmoothnes;
            }
            curZoom = Mathf.Clamp(curZoom, minZoom, maxZoom);
            transform.position = transform.forward * (curZoom - disFromTarget);
        }

        Vector2 rotation;
        private void Rotation()
        {
            Vector2 InputRot = Vector2.zero; //{ x = Input.GetAxis("Mouse X"), y = Input.GetAxis("Mouse Y") };

            if(Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                InputRot = touch.deltaPosition;
            }
            else
            {
                InputRot.x = Input.GetAxis("Mouse X");
                InputRot.y = Input.GetAxis("Mouse Y");
            }

            rotation.x += InputRot.x;
            rotation.y -= InputRot.y;
            Vector3 targetRotation = new Vector3(rotation.y * rotateSensitivty, rotation.x * rotateSensitivty);
            transform.eulerAngles = targetRotation;
        }
    }
}