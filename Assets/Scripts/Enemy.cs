using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int rutina;
    public float cronometro;
    public Animator animator;
    public Quaternion angulo;
    public float grado;

    public GameObject target;
    public bool attacking;

    private readonly string walkAnimation = "Walk";
    private readonly string runAnimaiton = "Run";
    private readonly string attackAnimation = "Attack";

    void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.Find("PlayerModel");
    }

    void Update()
    {
        EnemyBehavior();
    }

    public void EnemyBehavior()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > 10)
        {
            animator.SetBool(runAnimaiton, false);
            cronometro += 1 * Time.deltaTime;
            if (cronometro >= 4)
            {
                rutina = Random.Range(0, 2);
                cronometro = 0;
            }
            switch (rutina)
            {
                case 0:
                    animator.SetBool(walkAnimation, false);
                    break;
                case 1:
                    grado = Random.Range(0, 360);
                    angulo = Quaternion.Euler(0, grado, 0);
                    rutina++;
                    break;
                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                    transform.Translate(Vector3.forward * 1 * Time.deltaTime);
                    animator.SetBool(walkAnimation, true);
                    break;
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, target.transform.position) > 2 && !attacking)
            {
                var lookPosition = target.transform.position - transform.position;
                lookPosition.y = 0;

                var rotation = Quaternion.LookRotation(lookPosition);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 3);
                animator.SetBool(walkAnimation, false);

                animator.SetBool(runAnimaiton, true);
                transform.Translate(Vector3.forward * 2 * Time.deltaTime);
                animator.SetBool(attackAnimation, false);
            }
            else
            {
                animator.SetBool(walkAnimation, false);
                animator.SetBool(runAnimaiton, false);
                animator.SetBool(attackAnimation, true);
                attacking = true;
            }
        }
    }
    public void EndAttack()
    {
        animator.SetBool(attackAnimation, false);
        attacking = false;
    }
}
