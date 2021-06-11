using UnityEngine;
using MEmir.Mobile;

namespace MEmir.FirstPerson
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        private CharacterController controller;
        [SerializeField] private Transform camera;
        [SerializeField] private float mouseSensitivity = 8;
        [SerializeField] private float mobileSensitivity = .1f;
        [SerializeField] private float moveSpeed = 8;
        [SerializeField] private float gravity = -9f;
        private MobileInput mobileInput;


        private void Awake()
        {
            controller = GetComponent<CharacterController>();
        }


        private void Start()
        {
            mobileInput = MobileInput.Instance;
        }

        Vector3 velocity;
        float xRotation;
        bool isGrounded;
        private void Update()
        {
            Vector2 moveInput = GetMovementInput();
            Vector2 rotationInput = GetMouseInput();


            rotationInput.x *= mouseSensitivity;
            rotationInput.y *= mouseSensitivity;

            xRotation -= rotationInput.y;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            camera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            transform.Rotate(Vector3.up * rotationInput.x);

            if(controller.isGrounded && velocity.y < 0)
                velocity.y = -2;

            Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
            controller.Move(move * moveSpeed * Time.deltaTime);

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }

        Vector2 GetMovementInput()
        {
            Vector2 output = Vector2.zero;
            if (mobileInput.IsJoyAvaliable(1, out output))
            {
                if (output != Vector2.zero)
                    return output;
            }
            return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }

        Vector2 GetMouseInput()
        {
            Vector2 output = Vector2.zero;
            if(mobileInput.IsJoyAvaliable(0, out output))
            {
                if (output != Vector2.zero)
                    return output * mobileSensitivity;
                else if (mobileInput.IsJoyAvaliable(1, out output))
                {
                    if (output != Vector2.zero)
                        return new Vector2(output.x, 0) * mobileSensitivity;
                }
            }
            else if(Input.GetMouseButton(0))
                return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            return output;
        }
    }
}