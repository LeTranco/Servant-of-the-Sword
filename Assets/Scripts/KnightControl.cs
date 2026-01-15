using System;
using UnityEngine;

public class Knight : MonoBehaviour
{
    public float speed = 1.0f;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;
    private float jumpForce = 4.0f;
    private bool isGrounded = true;
    KnightCombat knightCombat;
    public AudioSource audioSource;
    public AudioClip swordSound;
    public AudioClip heavySwordSound;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        knightCombat = GetComponent<KnightCombat>();
    }
    
    void Update()
    {
        float X = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(X * speed, rb.linearVelocity.y);
        anim.SetFloat("speed", Mathf.Abs(X));

        if (X > 0.01f)
        {
            sr.flipX = false;
        }
        else if (X < -0.01f)
        {
            sr.flipX = true;
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
            anim.SetBool("isJumping", true);
        }

        CharacterAttack();
        CharacterHeavyAttack();
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            anim.SetBool("isJumping", false);
        }
    }

    void CharacterAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("isAttack");
            knightCombat.DamageEnemy();
            audioSource.PlayOneShot(swordSound);
        }
    }
    
    void CharacterHeavyAttack()
    {
        if (Input.GetMouseButtonDown(1))
        {
            anim.SetTrigger("isHeavyA");
            knightCombat.hDamageEnemy();
            audioSource.PlayOneShot(heavySwordSound);
        }
    }
 }
