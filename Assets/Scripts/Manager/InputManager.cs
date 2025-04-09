using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    InputSystem_Actions _inputActions;
    public Action<GameObject> crewSelectAction;
    public Action CrewDeselectAction;
    public Action<GameObject> cellSelectAction;
    public Action<GameObject> enemySlotSelectAction;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        Init();
    }

    void Init()
    {
        _inputActions = new InputSystem_Actions();
        _inputActions.Enable();
        _inputActions.Control.LeftClick.performed += OnLeftClick;
        _inputActions.Control.RightClick.performed += OnRightClick;
    }

    void OnLeftClick(InputAction.CallbackContext context)
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Crew"))
            {
                crewSelectAction.Invoke(hit.collider.gameObject);
            }
        }
        else
        {
            Debug.Log("No object was hit by the raycast.");
        }
    }

    void OnRightClick(InputAction.CallbackContext context)
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Cell") && CrewManager.Instance.SelectedCrew != null)
            {
                cellSelectAction.Invoke(hit.collider.gameObject);
            }
            else if (hit.collider.gameObject.CompareTag("EnemySlot"))
            {
                enemySlotSelectAction.Invoke(hit.collider.gameObject);
            }
        }
        else
        {
            Debug.Log("No object was hit by the raycast.");
            if (CrewManager.Instance.SelectedCrew != null)
            {
                CrewDeselectAction.Invoke();
            }

        }
    }

    private void OnDestroy()
    {
        if (_inputActions != null)
        {
            _inputActions.Control.LeftClick.performed -= OnLeftClick;
            _inputActions.Control.LeftClick.performed -= OnRightClick;
            _inputActions.Disable();
        }
    }
}
