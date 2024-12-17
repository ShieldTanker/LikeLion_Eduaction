


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    CharController charController;

    // Start is called before the first frame update
    void Start()
    {
        charController = GetComponentInParent<CharController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D");
        charController.grounded = true;
        charController.GetComponent<Animator>().Play("Idle");
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("OnTriggerExit2D");
        charController.grounded = false;
    }
}
