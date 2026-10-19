using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

/// <summary>
/// Over-the-shoulder third person camera (Uncharted / The Last of Us style).
/// Attach to Main Camera. Set Target to PlayerCameraRoot.
/// </summary>
public class ShoulderCamera : MonoBehaviour
{
    [Header("Target")]
    [Tooltip("Drag PlayerCameraRoot here (a point around head/neck height).")]
    public Transform target;

    [Header("Shoulder Position")]
    [Tooltip("Distance behind the player.")]
    public float distance = 3.0f;
    [Tooltip("Positive = camera over the RIGHT shoulder, negative = left.")]
    public float shoulderOffset = 0.6f;
    [Tooltip("Extra height above the target point.")]
    public float heightOffset = 0.2f;

    [Header("Look")]
    public float mouseSensitivity = 0.15f;
    public float gamepadSensitivity = 120f;
    public float minPitch = -35f;
    public float maxPitch = 60f;
    public bool invertY = false;
    [Tooltip("0 = instant, higher = smoother but floatier.")]
    public float rotationSmoothing = 0.03f;

    [Header("Camera")]
    public float fieldOfView = 60f;

    [Header("Cursor")]
    public bool lockCursor = true;

    private Camera _cam;
    private float _yaw;
    private float _pitch;
    private float _smoothYaw;
    private float _smoothPitch;
    private float _yawVel;
    private float _pitchVel;

    private void Start()
    {
        _cam = GetComponent<Camera>();
        if (_cam != null) _cam.fieldOfView = fieldOfView;

        Vector3 angles = transform.eulerAngles;
        _yaw = _smoothYaw = angles.y;
        _pitch = _smoothPitch = angles.x > 180f ? angles.x - 360f : angles.x;

        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void Update()
    {
        // Press Escape to free the mouse while testing; click to lock it again.
        if (EscapePressed())
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (lockCursor && LeftClickPressed() && Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void LateUpdate()
    {
        if (target == null) return;

        // ---- Look input ----
        Vector2 look = GetLookInput();
        _yaw += look.x;
        _pitch += invertY ? look.y : -look.y;
        _pitch = Mathf.Clamp(_pitch, minPitch, maxPitch);

        if (rotationSmoothing > 0f)
        {
            _smoothYaw = Mathf.SmoothDampAngle(_smoothYaw, _yaw, ref _yawVel, rotationSmoothing);
            _smoothPitch = Mathf.SmoothDampAngle(_smoothPitch, _pitch, ref _pitchVel, rotationSmoothing);
        }
        else
        {
            _smoothYaw = _yaw;
            _smoothPitch = _pitch;
        }

        Quaternion rotation = Quaternion.Euler(_smoothPitch, _smoothYaw, 0f);

        // ---- Position: pivot -> shift to shoulder -> pull back ----
        Vector3 pivot = target.position + Vector3.up * heightOffset;
        Vector3 shoulderPoint = pivot + rotation * Vector3.right * shoulderOffset;

        transform.position = shoulderPoint + rotation * Vector3.back * distance;
        transform.rotation = rotation;
    }

    // ---------------- Input helpers (work with old OR new Input System) ----------------

    private Vector2 GetLookInput()
    {
#if ENABLE_INPUT_SYSTEM
        Vector2 result = Vector2.zero;
        if (Mouse.current != null && Cursor.lockState == CursorLockMode.Locked)
            result += Mouse.current.delta.ReadValue() * mouseSensitivity;
        if (Gamepad.current != null)
            result += Gamepad.current.rightStick.ReadValue() * gamepadSensitivity * Time.deltaTime;
        return result;
#else
        if (Cursor.lockState != CursorLockMode.Locked) return Vector2.zero;
        return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * (mouseSensitivity * 10f);
#endif
    }

    private bool EscapePressed()
    {
#if ENABLE_INPUT_SYSTEM
        return Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame;
#else
        return Input.GetKeyDown(KeyCode.Escape);
#endif
    }

    private bool LeftClickPressed()
    {
#if ENABLE_INPUT_SYSTEM
        return Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame;
#else
        return Input.GetMouseButtonDown(0);
#endif
    }
}