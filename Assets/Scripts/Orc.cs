using UnityEngine;

public class Orc : MonoBehaviour {
    public Animator anim;
    public int maxHealth = 100;
    private int currentHealth;
    private OrcBrain brain;

    void Start() {
        currentHealth = maxHealth;
        brain = GetComponent<OrcBrain>();
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
        anim.SetTrigger("Hurt");
        if (brain != null) brain.ApplyReward(-5f);

        if (currentHealth <= 0) Die();
    }

    void Die() {
        anim.SetBool("IsDead", true);
        if (brain != null) brain.ApplyReward(-50f);
        this.enabled = false;
        Destroy(gameObject, 2f);
    }
}