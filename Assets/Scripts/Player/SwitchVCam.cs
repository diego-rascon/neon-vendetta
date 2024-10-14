using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchVCam : MonoBehaviour
{
    [SerializeField]
    private PlayerInput playerInput;
    [SerializeField]
    private int PriorityBoostAmount = 10;

    private CinemachineVirtualCamera virtualCamera;
    private InputAction secondaryAction;

    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        secondaryAction = playerInput.actions["Secondary"];
    }

    private void OnEnable()
    {
        secondaryAction.performed += _ => StartSecondary();
        secondaryAction.canceled += _ => CancelSecondary();
    }

    private void OnDisable()
    {
        secondaryAction.performed -= _ => StartSecondary();
        secondaryAction.canceled -= _ => CancelSecondary();
    }

    private void StartSecondary()
    {
        virtualCamera.Priority += PriorityBoostAmount;
    }

    private void CancelSecondary()
    {
        virtualCamera.Priority -= PriorityBoostAmount;
    }
}
