using UnityEngine;

public class OrcControıl : MonoBehaviour
{
    public float speed = 0.7f;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;
    private float jumpForce = 3.0f;
    private bool isGrounded = true;
    OrcCombat orcCombat;
    [SerializeField] bool IsAI;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        orcCombat = GetComponent<OrcCombat>();
    }
    
    void Update()
    {
        if (IsAI == true)
        {
            return;
        }
        float X = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(X * speed, rb.linearVelocity.y);
        anim.SetFloat("Speed", Mathf.Abs(X));

        if (X > -0.01f)
        {
            sr.flipX = false;
        }
        else if (X < 0.01f)
        {
            sr.flipX = true;
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
            anim.SetBool("IsJumping", true);
        }

        OrcAttack();
        OrcHeavyAttack();
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            anim.SetBool("IsJumping", false);
        }
    }
    
    void OrcAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("IsAttack");
            orcCombat.DamageKnight();
        }
    }
    
    void OrcHeavyAttack()
    {
        if (Input.GetMouseButtonDown(1))
        {
            anim.SetTrigger("IsHeavyA");
            orcCombat.hDamageKnight();
        }
    }
}
