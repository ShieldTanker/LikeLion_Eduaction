using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinCos : MonoBehaviour
{
    public Transform orgin;

    public float angles;
    public float speed;
    public float oribitRadius;

    public float SinA;
    public float CosA;

    private void Update()
    {
         angles += speed * Time.deltaTime;

        CosA = Mathf.Cos(angles * Mathf.Deg2Rad);
        SinA = Mathf.Sin(angles * Mathf.Deg2Rad);

        float x = Mathf.Cos(angles) * oribitRadius;
        float y = Mathf.Sin(angles) * oribitRadius;

        transform.position = new Vector3(x, y, 0) + orgin.position;
    }
}
