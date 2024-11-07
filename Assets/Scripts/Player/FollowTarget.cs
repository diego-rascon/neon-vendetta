using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private Transform followTarget;

    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float topClamp = 70f;
    [SerializeField] private float bottomClamp = -40f;

    private float cinemachineTargetPitch;
    private float cinemachineTargetYaw;

    private PlayerInput playerInput;
    private InputAction lookAction;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        lookAction = playerInput.actions["Look"];
    }

    private void LateUpdate()
    {
        CameraLogic();
    }

    private void CameraLogic()
    {
        Vector2 input = lookAction.ReadValue<Vector2>();

        float mouseX = GetMouseInput(input.x);
        float mouseY = GetMouseInput(-input.y);

        cinemachineTargetPitch = UpdateRotation(cinemachineTargetPitch, mouseY, bottomClamp, topClamp, true);
        cinemachineTargetYaw = UpdateRotation(cinemachineTargetYaw, mouseX, float.MinValue, float.MaxValue, false);

        ApplyRotation(cinemachineTargetPitch, cinemachineTargetYaw);
    }

    private void ApplyRotation(float pitch, float yaw)
    {
        followTarget.rotation = Quaternion.Euler(pitch, yaw, followTarget.eulerAngles.z);
    }

    private float UpdateRotation(float currentRotation, float input, float min, float max, bool isXAxis)
    {
        currentRotation += isXAxis ? -input : input;
        return Mathf.Clamp(currentRotation, min, max);
    }

    private float GetMouseInput(float axis)
    {
        return axis * rotationSpeed * Time.deltaTime;
    }
}
