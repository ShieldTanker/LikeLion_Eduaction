using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpSpeed;
    [SerializeField] float groundDis;
    bool grounded = true;
    
    InputAction moveInput;
    InputAction jumpInput;
    
    [SerializeField] Animator animator;

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    [SerializeField] private Camera mainCam;
    [SerializeField] float camSpeed = 4.0f;
    
    [SerializeField] Vector3 camOffSet;
    
    [SerializeField] float maxDis = 4.0f;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    
        rb = GetComponent<Rigidbody2D>();
        UnityEngine.InputSystem.PlayerInput Input = GetComponent<UnityEngine.InputSystem.PlayerInput>();
        // "Move" 라는 이름을 가진 Action 을 가져옴(ActionMap이 아님)
        moveInput = Input.actions["Move"];
        jumpInput = Input.actions["Jump"];

        camOffSet =  mainCam.transform.position - transform.position;
    }

    void Update()
    {
        Vector2 moveValue = moveInput.ReadValue<Vector2>();
        Debug.Log(moveValue);

        spriteRenderer.flipX = moveValue.x < 0;
        
        animator.SetFloat("Speed", Mathf.Abs(moveValue.x));

        transform.position += new Vector3(moveValue.x * moveSpeed, 0, 0) * Time.deltaTime;

        if (jumpInput.triggered && grounded)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, 1 << LayerMask.NameToLayer("Ground"));

            if(hit.distance <= groundDis)
            {
                Debug.Log(hit.distance);

                rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
                animator.Play("Jump");
                StartCoroutine(JumpEndCheck());
            }
        }
    }

    IEnumerator JumpEndCheck() 
    {
        grounded = false;

        yield return new WaitForFixedUpdate();

        while (true)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, 1 << LayerMask.NameToLayer("Ground"));

            if (hit.distance <= groundDis)
            {
                animator.Play("Idle");
                break;
            }
            yield return null;
        }

        grounded = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.GetContact(0).normal == Vector2.up)
        {
            animator.SetBool("Grounded", true);
        }
    }

    void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundDis);
    }
    private void LateUpdate()
    {
        var CharPosition = transform.position + camOffSet;
        float speed = camSpeed;

        Vector3 newPosition = Vector3.zero;
        
        if (Vector3.Distance(CharPosition, mainCam.transform.position) >= maxDis)
        {
            Vector3 Gap = ((mainCam.transform.position) - CharPosition).normalized * maxDis;
            newPosition = CharPosition + Gap;
        }
        else
        {
            newPosition = Vector3.MoveTowards(mainCam.transform.position, 
                CharPosition, 
                speed * Time.deltaTime);
        }
        
        mainCam.transform.position = newPosition;
        
        /*Vector3 charPosition = transform.position + camOffSet;
        float speed = camSpeed;
        
        Vector3 newPos = Vector3.MoveTowards(mainCam.transform.position,
            charPosition,
            camSpeed * Time.deltaTime);

        mainCam.transform.position = newPos;*/
    }
}