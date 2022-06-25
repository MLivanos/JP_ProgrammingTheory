using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector3(0,0,1);
        attackRange = 0.2f;
        health = 80;
        speed = 3;
        strength = 25;
        acceleration = 2000;
        attackDelay = 0.5f;
        recoveryDelay = 0.2f;
        knockBackPower = 5;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (rb.velocity.magnitude < speed)
        {
            Accelerate();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.collider.gameObject;
        if (other.CompareTag("Spider"))
        {
            Attack(other.GetComponent<Entity>());
        }
    }
}
