using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    protected float health;
    protected float speed;
    protected float strength;
    protected float acceleration;
    protected float attackRange;
    protected float attackDelay;
    protected float recoveryDelay;
    protected float knockBackPower;
    protected bool windingUp;
    protected bool inAttack;
    protected bool dead;
    protected Rigidbody rb;

    void Update()
    {
        if (!IsAlive() && !dead)
        {
            Die();
        }
        if (dead)
        {
            rb.velocity = Vector3.zero;
        }
    }

    // ABSTRACTION: Entities windup, attack, and recover, but this is all taken place in the attack method
    protected virtual void Attack(Entity other)
    {
        if(!inAttack)
        {
            StartCoroutine(PerformAttack(other));
        }
    }

    protected virtual IEnumerator PerformAttack(Entity other)
    {
        inAttack = true;
        GetComponent<Renderer>().material.color = Color.yellow;
        windingUp = true;
        yield return new WaitForSeconds(attackDelay);
        windingUp = false;
        if(inRange(other))
        {
            other.reduceHealth(strength);
        }
        Vector3 knockBackDirection = (other.gameObject.transform.position - transform.position).normalized;
        other.gameObject.GetComponent<Rigidbody>().AddForce(knockBackDirection * knockBackPower, ForceMode.Impulse);
        GetComponent<Renderer>().material.color = Color.cyan;
        yield return new WaitForSeconds(recoveryDelay);
        GetComponent<Renderer>().material.color = Color.red;
        inAttack = false;
    }

    protected bool inRange(Entity other)
    {
        float dist = Vector3.Distance(other.gameObject.transform.position, transform.position);
        dist -= transform.localScale.x + other.gameObject.transform.localScale.x;
        return dist <= attackRange;
    }

    // ENCAPSULATION: Health can be acessed and reduced via a method, but not otherwise set
    public void reduceHealth(float damage)
    {
        if(windingUp)
        {
            damage *= 2;
        }
        health -= damage;
        Debug.Log(health);
    }

    public float getHealth()
    {
        return health;
    }

    public bool IsAlive()
    {
        return health > 0;
    }

    public abstract void Die();
}
