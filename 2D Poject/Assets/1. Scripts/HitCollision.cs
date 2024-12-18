using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollision : MonoBehaviour
{
    [NonSerialized] public Rigidbody2D parentRigidBody;

    private void Start()
    {
        parentRigidBody = GetComponentInParent<Rigidbody2D>();
    }
}
