using UnityEngine;

public class OrcControl : MonoBehaviour {
    public float speed = 1.5f;
    public float decisionDelay = 0.3f;
    [SerializeField] bool IsAI = true;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;
    private OrcBrain brain;
    private float decisionTimer = 0f;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        brain = GetComponent<OrcBrain>();
    }

    void Update() {
        if (IsAI) AIUpdate();
        else ManualUpdate();
    }

    void AIUpdate() {
        decisionTimer += Time.deltaTime;
        if (decisionTimer >= decisionDelay) {
            int action = brain.DecideAction();
            if (action == -1) { rb.linearVelocity = new Vector2(0, rb.linearVelocity.y); return; }

            switch (action) {
                case 0: Move(-1); break;
                case 1: Move(1); break;
                case 2: RequestAttack("IsAttack"); break;
                case 3: RequestAttack("IsHeavyA"); break;
            }
            decisionTimer = 0f;
        }
    }

    void ManualUpdate() {
        Move(Input.GetAxisRaw("Horizontal"));
        if (Input.GetMouseButtonDown(0)) RequestAttack("IsAttack");
        if (Input.GetMouseButtonDown(1)) RequestAttack("IsHeavyA");
    }

    void Move(float dir) {
        rb.linearVelocity = new Vector2(dir * speed, rb.linearVelocity.y);
        anim.SetFloat("Speed", Mathf.Abs(dir));
        if (dir != 0) sr.flipX = dir < 0;
    }

    void RequestAttack(string triggerName) {
        anim.SetTrigger(triggerName);
        GetComponent<OrcCombat>().ResetAttackFlag();
    }
}