using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageField : MonoBehaviour
{
    public string MyOwnerTag;
    public float power;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(MyOwnerTag))
        {
            Debug.Log(collision.name);
            collision.GetComponent<Monster>().HitDamage(power);
        }
    }
}
