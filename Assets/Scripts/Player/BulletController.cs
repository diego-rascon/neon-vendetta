using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private readonly float speed = 50f;
    [SerializeField]
    private readonly float timeToDestroy = 3f;

    public Vector3 target;
    public bool hit;

    void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            Destroy(gameObject);
        }
    }

    public void SetTarget(Vector3 targetPosition, bool didHit)
    {
        target = targetPosition;
        hit = didHit;
    }
}
