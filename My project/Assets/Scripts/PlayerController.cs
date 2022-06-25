using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Entity
{
    int rotationSpeed = 30;
    void Start()
    {
        attackRange = 0.5f;
        health = 100;
        speed = 5;
        strength = 30;
        acceleration = 3000;
        attackDelay = 0.4f;
        recoveryDelay = 0.1f;
        knockBackPower = 8;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (!dead)
        {
            TwoDirectionalMotion();
        }
    }

    private void TwoDirectionalMotion()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        if (rb.velocity.magnitude < speed)
        {
            rb.AddForce(Vector3.forward * Time.deltaTime * acceleration * verticalInput);
            transform.Rotate(Vector3.up, Time.deltaTime * rotationSpeed * horizontalInput);   
        }
    }

    override public void Die()
    {
        Debug.Log("I'm dead!");
        dead = true;
    }
}
