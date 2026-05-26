using UnityEngine;

/// <summary>
/// Cámara libre tipo "fly". 
/// WASD = moverse, Mouse = mirar, E/Q = subir/bajar, Tab = liberar cursor.
/// </summary>
public class FreeFlyCamera : MonoBehaviour
{
    [Header("Velocidades")]
    public float moveSpeed = 10f;
    public float lookSpeed = 2f;
    public float ascendSpeed = 5f;

    private float yaw = 0f;
    private float pitch = 0f;
    private bool cursorLocked = true;

    private void Start()
    {
        // Bloquea el cursor al iniciar
        SetCursorLocked(true);

        // Suscribirse al toggle del cursor desde InputManager
        if (InputManager.Instance != null)
            InputManager.Instance.OnToggleCursor += ToggleCursor;
    }

    private void OnDestroy()
    {
        if (InputManager.Instance != null)
            InputManager.Instance.OnToggleCursor -= ToggleCursor;
    }

    private void Update()
    {
        if (!cursorLocked) return;

        HandleLook();
        HandleMove();
    }

    private void HandleLook()
    {
        Vector2 look = InputManager.Instance.LookDelta;

        yaw   += look.x * lookSpeed;
        pitch -= look.y * lookSpeed;
        pitch  = Mathf.Clamp(pitch, -89f, 89f);

        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }

    private void HandleMove()
    {
        Vector2 move   = InputManager.Instance.MoveInput;
        float ascend   = InputManager.Instance.AscendInput;

        Vector3 direction = new Vector3(move.x, 0f, move.y);
        direction = transform.TransformDirection(direction);
        direction.y += ascend;

        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    private void ToggleCursor()
    {
        SetCursorLocked(!cursorLocked);
    }

    private void SetCursorLocked(bool locked)
    {
        cursorLocked = locked;
        Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible   = !locked;
    }
}
