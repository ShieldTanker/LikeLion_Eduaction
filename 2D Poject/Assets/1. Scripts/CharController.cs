using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public struct DamageFieldData
{
    public float distance;
}

public class CharController : MonoBehaviour
{
    private const float jumpTestValue = 0.3f;

    // 해시값으로 변환해서 해당 해시값으로 애니메이션을 찾게 할수 있음(최적화)
    private static readonly int Speed1 = Animator.StringToHash("Speed");
    private static readonly int Attack = Animator.StringToHash("Attack");

    private static readonly int Ground = Animator.StringToHash("Ground");

    [SerializeField] float moveSpeed;
    [SerializeField] float jumpSpeed;
    [SerializeField] float groundDis;
    public bool grounded = true;
    
    InputAction moveInput;
    InputAction jumpInput;

    int damageFieldIdx;
    public List<CButton> buttons;
    public List<DamageField> damageFields;
    public List<DamageFieldData> damageFieldDatas;

    [SerializeField] Animator animator;

    public Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    [SerializeField] private Camera mainCam;
    [SerializeField] float camSpeed = 4.0f;
    
    [SerializeField] Vector3 camOffSet;
    
    [SerializeField] float maxDis = 4.0f;

    public Camera camera;
    public Canvas canvas;
    public HPBar hpBar;
    public Transform hpBarPos;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    
        PlayerInput Input = GetComponent<PlayerInput>();
        // "Move", "Jump" 라는 이름을 가진 Action 을 가져옴(ActionMap이 아님)
        moveInput = Input.actions["Move"];
        jumpInput = Input.actions["Jump"];

        camOffSet =  mainCam.transform.position - transform.position;

        foreach (var button in buttons)
        {
            // button.AddListener(() => { }) : 임시로 아무내용없는 함수 추가
            button.AddListener(() => FireSkill(button.idx));
        }

        GameObject go = Instantiate(hpBar.gameObject, canvas.transform);
        go.GetComponent<HPBar>().UpdateOwner(hpBarPos, camera);
    }

    void Update()
    {
        Vector2 moveValue = moveInput.ReadValue<Vector2>();

        if(!canMove)
            moveValue = Vector2.zero;

        if (moveValue.x != 0)
            spriteRenderer.flipX = moveValue.x < 0;

        // 문자열 말고 미리 해시값을 계산한 후 그 해시값으로 찾기에 더 최적화됨
        animator.SetFloat(Speed1, Mathf.Abs(moveValue.x));
        
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

    bool canMove = true;

    void CanMove(int bMove)
    {
        canMove = bMove == 1;
    }

    void FireSkill(int idx)
    {
        damageFieldIdx = idx;
        animator.Rebind();
        // Attack변수에 있는 해쉬값으로 애니메이션 스테이트를 찾음
        animator.Play(Attack);
    }

    void FireDamageField(int idx)
    {
        GameObject go = Instantiate(damageFields[damageFieldIdx].gameObject);
        go.GetComponent<DamageField>().MyOwnerTag = "Player";
        go.transform.position = transform.position + transform.right * damageFieldDatas[damageFieldIdx].distance;
        Destroy(go, 3.0f);
    }

    /*IEnumerator FireSkillCoroutin()
    {
        animator.Rebind();
        animator.Play("Attack");

        yield return null;

        var curState = animator.GetCurrentAnimatorStateInfo(0);

        // 애니메이션 진행시간이 1 미만 이고, Attack 애니메이션 일때
        while (1.0 > curState.normalizedTime && curState.IsName("Attack"))
        {
            yield return null;
        }
    }*/
}