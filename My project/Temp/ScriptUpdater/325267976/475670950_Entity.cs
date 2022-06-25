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
    protected bool windingUp;
    protected bool inAttack;
    protected Rigidbody rb;

    // ABSTRACTION: Entities windup, attack, and recover, but this is all taken place in the attack method
    protected virtual IEnumerator Attack(Entity other)
    {
        if (inAttack)
        {
            return;
        }
        inAttack = true;
        GetComponent<Renderer>().material.color = Color.yellow;
        windingUp = true;
        yield return new WaitForSeconds(attackDelay);
        windingUp = false;
        if(inRange(other))
        {
            other.reduceHealth(strength);
        }
        GetComponent<Renderer>().material.color = Color.cyan;
        yield return new WaitForSeconds(recoveryDelay);
        GetComponent<Renderer>().material.color = Color.red;
        inAttack = false;
    }

    protected bool inRange(Entity other)
    {
        float dist = Vector3.Distance(other.gameObject.position, transform.position);
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
    }

    public float getHealth()
    {
        return health;
    }

    public void IsAlive()
    {
        return health > 0;
    }

    public abstract void Die();
}
