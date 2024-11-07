using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAiming : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera followCamera;
    [SerializeField]
    private CinemachineVirtualCamera aimCamera;

    private RaycastWeapon weapon;
    private PlayerInput playerInput;
    private InputAction attackAction;
    private InputAction secondaryAction;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        attackAction = playerInput.actions["Attack"];
        secondaryAction = playerInput.actions["Secondary"];
    }

    void OnEnable()
    {
        attackAction.started += OnAttackStarted;
        attackAction.canceled += OnAttackCanceled;

        secondaryAction.started += OnAimingStarted;
        secondaryAction.canceled += OnAimingCanceled;
    }

    void OnDisable()
    {
        attackAction.started -= OnAttackStarted;
        attackAction.canceled -= OnAttackCanceled;

        secondaryAction.started -= OnAimingStarted;
        secondaryAction.canceled -= OnAimingCanceled;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        weapon = GetComponentInChildren<RaycastWeapon>();
    }

    void Update()
    {
        if (weapon.isFiring)
        {
            weapon.UpdateFiring(Time.deltaTime);
        }
    }

    private void OnAttackStarted(InputAction.CallbackContext context)
    {
        weapon.StartFiring();
    }

    private void OnAttackCanceled(InputAction.CallbackContext context)
    {
        weapon.StopFiring();
    }

    private void OnAimingStarted(InputAction.CallbackContext context)
    {
        followCamera.Priority = 5;
        aimCamera.Priority = 10;
    }
    private void OnAimingCanceled(InputAction.CallbackContext context)
    {
        followCamera.Priority = 10;
        aimCamera.Priority = 5;
    }
}