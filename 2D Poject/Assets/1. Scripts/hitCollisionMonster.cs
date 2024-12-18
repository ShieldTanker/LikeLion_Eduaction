using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollisionMonster : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D rb = other.GetComponent<HitCollision>().parentRigidBody;

        Vector3 backPosition = rb.transform.position - transform.position;
        backPosition.Normalize();
        backPosition.x *= 3;
        rb.AddForce(backPosition * 800, ForceMode2D.Force);
    }
}
