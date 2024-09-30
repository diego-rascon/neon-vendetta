using Cinemachine;
using StarterAssets;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera aimCamera;

    [SerializeField]
    private float normalSensitivity = 1f;

    [SerializeField]
    private float aimSensitivity = 0.5f;

    [SerializeField]
    private LayerMask aimColliderLayerMask = new LayerMask();

    private ThirdPersonController controller;
    private StarterAssetsInputs inputs;

    private void Awake()
    {
        controller = GetComponent<ThirdPersonController>();
        inputs = GetComponent<StarterAssetsInputs>();
    }

    private void Update()
    {
        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            mouseWorldPosition = raycastHit.point;
        }

        if (inputs.aim)
        {
            aimCamera.gameObject.SetActive(true);
            controller.SetSensitivity(aimSensitivity);

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimdirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimdirection, Time.deltaTime * 20f);
        }
        else
        {
            aimCamera.gameObject.SetActive(false);
            controller.SetSensitivity(normalSensitivity);
        }
    }
}
