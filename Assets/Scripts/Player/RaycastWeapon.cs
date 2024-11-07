using UnityEngine;

public class RaycastWeapon : MonoBehaviour
{
    class Bullet
    {
        public float time;
        public Vector3 initialPosition;
        public Vector3 initialVelocity;
    }

    public bool isFiring = false;
    public int fireRate = 29;

    public ParticleSystem muzzleFlash;
    public ParticleSystem hitEffect;

    public TrailRenderer tracerEffect;

    public Transform raycastOrigin;
    //public Transform raycastDestinationTarget;

    Ray ray;
    RaycastHit hitInfo;
    float accumulatedTime;

    public void StartFiring()
    {
        isFiring = true;
        accumulatedTime = 0.0f;
        FireBullet();
    }

    public void UpdateFiring(float deltaTime)
    {
        accumulatedTime += deltaTime;
        float fireInterval = 1.0f / fireRate;

        while (accumulatedTime >= 0.0f)
        {
            FireBullet();
            accumulatedTime -= fireInterval;
        } 
    }

    private void FireBullet()
    {
        muzzleFlash.Emit(1);

        ray.origin = raycastOrigin.position;
        //ray.direction = raycastDestinationTarget.position - raycastOrigin.position;

        var tracer = Instantiate(tracerEffect, ray.origin, Quaternion.identity);
        tracer.AddPosition(ray.origin);

        if (Physics.Raycast(ray, out hitInfo))
        {
            Instantiate(hitEffect, hitInfo.point + hitInfo.normal * 0.0001f, Quaternion.LookRotation(hitInfo.normal));
            tracer.transform.position = hitInfo.point;
        }
    }

    public void StopFiring()
    {
        isFiring = false;
    }
}
