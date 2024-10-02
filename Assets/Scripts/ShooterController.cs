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

    [SerializeField]
    private Transform pfBulletProjectile;

    [SerializeField]
    private Transform spawnBulletPosition;

    [SerializeField]
    private Transform vfxHitWall;

    [SerializeField]
    private Transform vfxHitEnemy;

    private Animator animator;

    private ThirdPersonController controller;
    private StarterAssetsInputs inputs;

    private void Awake()
    {
        controller = GetComponent<ThirdPersonController>();
        inputs = GetComponent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);

        Transform hitTransform = null;

        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            mouseWorldPosition = raycastHit.point;
            hitTransform = raycastHit.transform;
        }

        if (inputs.aim)
        {
            aimCamera.gameObject.SetActive(true);
            controller.SetSensitivity(aimSensitivity);
            controller.SetRotateOnMove(false);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimdirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimdirection, Time.deltaTime * 20f);
        }
        else
        {
            aimCamera.gameObject.SetActive(false);
            controller.SetSensitivity(normalSensitivity);
            controller.SetRotateOnMove(true);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
        }

        if (inputs.shoot)
        {
            if (hitTransform != null)
            {
                var position = raycastHit.point + raycastHit.normal * 0.0001f;
                var rotation = Quaternion.LookRotation(raycastHit.normal);

                if (hitTransform.GetComponent<Enemy>() != null)
                {

                    Instantiate(vfxHitEnemy, position, rotation);
                }
                else
                {
                    Instantiate(vfxHitWall, position, rotation);
                }
            }
            Vector3 aimDirection = (mouseWorldPosition - spawnBulletPosition.position).normalized;
            Instantiate(pfBulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDirection, Vector3.up));
            inputs.shoot = false;
        }
    }
}
