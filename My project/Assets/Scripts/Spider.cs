using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITENCE: Spider is a kind of enemy
public class Spider : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        // POLYMORPHISM: Changing various aspects of the enemy
        attackRange = 0.1f;
        health = 50;
        speed = 6;
        strength = 10;
        acceleration = 4000;
        attackDelay = 0.3f;
        recoveryDelay = 0.5f;
        knockBackPower = 1;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (rb.velocity.magnitude < speed && !dead)
        {
            Accelerate();
        }
    }
}
