using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    Animator anim;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;

    public float speed;
    public int switchCount;
    private int moveCount;

    public float hp;
    public float maxHp;
    public Slider hpSlider;

    public Vector2 direction;

    private void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(direction.x * speed * Time.deltaTime, 0, 0);

        moveCount++;

        if (moveCount >= switchCount)
        {
            direction *= -1;
            spriteRenderer.flipX = direction.x < 0;
            moveCount = 0;
        }
    }

    public void HitDamage(float damage)
    {
        hp -= damage;
        
        if (hp > 0)
        {
            hpSlider.value = hp / maxHp;
        }
        else if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}