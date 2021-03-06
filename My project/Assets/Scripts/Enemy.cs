using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    protected GameObject target;
    protected Vector3 direction;
    protected float deathTime = 5;

    protected virtual void Accelerate()
    {
        direction = target.transform.position - transform.position;
        direction.y = 0;
        rb.AddForce(direction * acceleration * Time.deltaTime, ForceMode.Acceleration);
    }

    public override void Die()
    {
        dead = true;
        rb.constraints = RigidbodyConstraints.None;
        StartCoroutine(Disappear());
    }

    protected virtual IEnumerator Disappear()
    {
        yield return new WaitForSeconds(deathTime);
        Destroy(gameObject);
    }

    public void SetTarget(GameObject entity)
    {
        target = entity;
    }
    
    void OnCollisionStay(Collision collision)
    {
        GameObject other = collision.collider.gameObject;
        if (other.CompareTag("Player") && !inAttack)
        {
            Attack(other.GetComponent<Entity>());
        }
    }
}
