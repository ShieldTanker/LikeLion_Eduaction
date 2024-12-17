using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpSpeed;
    [SerializeField] float groundDis;
    public bool grounded = true;
    
    InputAction moveInput;
    InputAction jumpInput;
    
    [SerializeField] Animator animator;

    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    [SerializeField] private Camera mainCam;
    [SerializeField] float camSpeed = 4.0f;
    
    [SerializeField] Vector3 camOffSet;
    
    [SerializeField] float maxDis = 4.0f;


    IEnumerator Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    
        PlayerInput Input = GetComponent<PlayerInput>();
        // "Move", "Jump" 라는 이름을 가진 Action 을 가져옴(ActionMap이 아님)
        moveInput = Input.actions["Move"];
        jumpInput = Input.actions["Jump"];

        camOffSet =  mainCam.transform.position - transform.position;

        yield return new WaitForSeconds(3.0f);
        
        Debug.Log(LayerMask.LayerToName(GetComponentInChildren<ItemGetter>().gameObject.layer));
    }

    void Update()
    {
        Vector2 moveValue = moveInput.ReadValue<Vector2>();

        if (moveValue.x != 0)
            spriteRenderer.flipX = moveValue.x < 0;

        animator.SetFloat("Speed", Mathf.Abs(moveValue.x));
        
        // transform.position += new Vector3(moveValue.x * moveSpeed, 0, 0) * Time.deltaTime;
        rb.velocity = new Vector2(moveValue.x * moveSpeed, rb.velocity.y);

        if (jumpInput.triggered && grounded)
        {
            rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            animator.Play("Jump");
        }
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