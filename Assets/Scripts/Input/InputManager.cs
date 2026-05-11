using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public Vector2 MousePosition { get; private set; }
    public bool IsLeftClicking { get; private set; }
    public bool IsEscapePressed { get; private set; }

    private PlayerInput _playerInput;
    private InputAction _mousePositionAction;
    private InputAction _clickAction;
    private InputAction _escapeAction;

    public static InputManager Instance { get; private set; }

    private void Awake()
    {
        InitializeInputs();

        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        MousePosition = _mousePositionAction.ReadValue<Vector2>();
        IsLeftClicking = _clickAction.WasPressedThisFrame();
        IsEscapePressed = _escapeAction.WasPressedThisFrame();
    }

    private void InitializeInputs()
    {
        _playerInput = GetComponent<PlayerInput>();
        _mousePositionAction = _playerInput.actions["MousePosition"];
        _mousePositionAction.Enable();
        _clickAction = _playerInput.actions["Click"];
        _clickAction.Enable();
        _escapeAction = _playerInput.actions["Pause"];
        _escapeAction.Enable();
    }
}
