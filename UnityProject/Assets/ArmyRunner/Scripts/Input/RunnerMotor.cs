using UnityEngine;
using UnityEngine.InputSystem;

namespace ArmyRunner
{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class RunnerMotor : MonoBehaviour
    {
        [SerializeField, Min(0f)] private float forwardSpeed = 7f;
        [SerializeField, Min(0f)] private float horizontalSpeed = 10f;
        [SerializeField, Min(0f)] private float horizontalLimit = 4.5f;
        [SerializeField, Min(0f)] private float pointerSensitivity = 0.012f;
        private Rigidbody body;
        private float horizontalInput;

        public float ForwardSpeed { get => forwardSpeed; set => forwardSpeed = Mathf.Max(0f, value); }
        public float HorizontalLimit { get => horizontalLimit; set => horizontalLimit = Mathf.Max(0f, value); }

        private void Awake()
        {
            body = GetComponent<Rigidbody>();
            body.useGravity = false;
            body.isKinematic = true;
            body.interpolation = RigidbodyInterpolation.Interpolate;
        }

        private void Update() => horizontalInput = ReadHorizontalInput();

        private void FixedUpdate()
        {
            Vector3 position = body.position;
            position.z += forwardSpeed * Time.fixedDeltaTime;
            position.x = Mathf.Clamp(position.x + horizontalInput * horizontalSpeed * Time.fixedDeltaTime,
                -horizontalLimit, horizontalLimit);
            body.MovePosition(position);
        }

        private float ReadHorizontalInput()
        {
            float value = 0f;
            if (Keyboard.current != null)
            {
                if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) value -= 1f;
                if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) value += 1f;
            }
            if (Gamepad.current != null) value = Gamepad.current.leftStick.ReadValue().x;
            if (Pointer.current != null && Pointer.current.press.isPressed)
                value = Mathf.Clamp(Pointer.current.delta.ReadValue().x * pointerSensitivity, -1f, 1f);
            return Mathf.Clamp(value, -1f, 1f);
        }
    }
}
