using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombatController : MonoBehaviour
{
    [SerializeField]
    private Transform bulletParent;
    [SerializeField]
    private float maxShootDistance = 100f;
    [SerializeField]
    private Transform spawnBulletPosition;
    [SerializeField]
    private GameObject pfBulletProjectile;
    [SerializeField]
    private Transform vfxHitWall;
    [SerializeField]
    private Transform vfxHitEnemy;

    private PlayerInput playerInput;
    private Transform cameraTransform;

    private InputAction attackAction;

    private Animator animator;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
        cameraTransform = Camera.main.transform;

        attackAction = playerInput.actions["Attack"];
    }

    private void OnEnable()
    {
        attackAction.performed += _ => Attack();
    }

    private void OnDisable()
    {
        attackAction.performed -= _ => Attack();
    }

    private void Attack()
    {
        Vector3 targetPoint;
        bool didHit;

        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hit, maxShootDistance))
        {
            targetPoint = hit.point;
            didHit = true;

            var decalToInstantiate = hit.collider.tag switch
            {
                "Enemy" => vfxHitEnemy,
                _ => vfxHitWall
            };
            Instantiate(decalToInstantiate, hit.point + hit.normal * 0.001f, Quaternion.LookRotation(hit.normal));

            if (hit.collider.CompareTag("Enemy"))
            {
                // Aplicar daño
            }
        }
        else
        {
            targetPoint = cameraTransform.position + cameraTransform.forward * maxShootDistance;
            didHit = true;
        }

        Vector3 direction = (targetPoint - spawnBulletPosition.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction);

        GameObject bullet = Instantiate(pfBulletProjectile, spawnBulletPosition.position, rotation, bulletParent);
        BulletController bulletController = bullet.GetComponent<BulletController>();
        bulletController.SetTarget(targetPoint, didHit);
    }
}
